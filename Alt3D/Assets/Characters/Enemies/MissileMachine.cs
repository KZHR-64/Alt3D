using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMachine : Enemies
{
    [SerializeField]
    private GameObject missile; //ミサイル
    [SerializeField]
    private Transform lArm; //左腕
    [SerializeField]
    private Transform rArm; //右腕
    private int pattern; //行動パターン
    private float attackStart; //攻撃開始時間
    private bool aStartFlag = true; //攻撃開始直後か
    private bool rFlag = false; //右の攻撃（移動）をしたか
    private float xDis; //移動先
    private readonly int idleAnim = Animator.StringToHash("IsIdle"); //停止状態のハッシュ
    private readonly int launchAnim = Animator.StringToHash("IsLaunch"); //発射状態のハッシュ
    private readonly int lMoveAnim = Animator.StringToHash("IsLMove"); //左移動状態のハッシュ
    private readonly int rMoveAnim = Animator.StringToHash("IsRMove"); //右移動状態のハッシュ
    private readonly int tackleAnim = Animator.StringToHash("IsTackle"); //突進状態のハッシュ

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        pattern = 0; //パターンを設定
        damage = 100; //接触ダメージを設定
    }

    //継承側の更新
    protected override void SubUpdate()
    {
        switch(pattern)
        {
            case 0:
                Pattern0(); //準備
                break;
            case 1:
                Pattern1(); //ミサイル
                break;
            case 2:
                Pattern2(); //手前横切り
                break;
            case 3:
                Pattern3(); //突進
                break;
            default:
                pattern = 0;
                break;
        }
    }

    //準備
    private void Pattern0()
    {
        //開始直後なら
        if (aStartFlag)
        {
            xDis = (Random.value * 10.0f - 5.0f); //移動先を決定
            aStartFlag = false; //開始フラグをfalseに
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(xDis, -1.5f, 0.0f), 12.0f * Time.deltaTime); //中央に移動
        //自機から近いなら
        if (1.0f < scale && scaleTime <= 0.0f)
        {
            SetScaleDis(1.0f, 1.0f); //特定の位置に移動
        }
        if (scale <= 1.0f && 3.0f < ETime - attackStart)
        {
            aStartFlag = true; //開始フラグをtrueに
            attackStart = ETime; //攻撃開始時間を設定
            pattern = (int)(Random.value * 3.0f + 1.0f); //パターンを設定
        }
    }

    //ミサイル
    private void Pattern1()
    {
        //開始直後なら
        if (aStartFlag)
        {
            GetComponent<Animator>().SetBool(launchAnim, true); //アニメーションをセット
            aStartFlag = false; //開始フラグをfalseに
            rFlag = true; //フラグをtrueに
        }
        //一定時間経過したら
        if (0.5f <= ETime - attackStart && ETime - attackStart < 1.0f && rFlag)
        {
            GameObject mis = Instantiate(missile, lArm.position, transform.rotation); //ミサイルを生成
            mis.GetComponent<Enemies>().Scale = scale; //拡大率を設定
            rFlag = false; //フラグをfalseに
        }
        if (1.0f <= ETime - attackStart && ETime - attackStart < 1.5f && !rFlag)
        {
            GameObject mis = Instantiate(missile, rArm.position, transform.rotation); //ミサイルを生成
            mis.GetComponent<Enemies>().Scale = scale; //拡大率を設定
            rFlag = true; //フラグをtrueに
        }
        if (2.0f <= ETime - attackStart)
        {
            GetComponent<Animator>().SetBool(launchAnim, false); //アニメーションをセット
            pattern = 0; //パターンを戻す
            aStartFlag = true; //開始フラグをtrueに
            attackStart = ETime; //攻撃開始時間を設定
        }
    }

    //手前横切り
    private void Pattern2()
    {
        //開始直後なら
        if (aStartFlag)
        {
            GetComponent<Animator>().SetBool(rMoveAnim, true); //アニメーションをセット
            aStartFlag = false; //開始フラグをfalseに
            rFlag = true; //フラグをtrueに
        }
        //画面外に移動
        if (rFlag)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(16.0f, -1.5f, 0.0f), 24.0f * Time.deltaTime); //画面外に移動
            //画面外に出たら拡大
            if (16.0f <= transform.position.x)
            {
                GetComponent<Animator>().SetBool(rMoveAnim, false); //アニメーションをセット
                GetComponent<Animator>().SetBool(lMoveAnim, true); //アニメーションをセット
                rFlag = false; //フラグをfalseに
                Scale = 2.5f;
            }
        }
        //自機の前を横切る
        if (1.0f <= ETime - attackStart && !rFlag)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-16.0f, -1.5f, 0.0f), 24.0f * Time.deltaTime); //画面外に移動
            //画面外に出たら戻る
            if (transform.position.x <= -16.0f)
            {
                GetComponent<Animator>().SetBool(lMoveAnim, false); //アニメーションをセット
                pattern = 0; //パターンを戻す
                aStartFlag = true; //開始フラグをtrueに
                attackStart = ETime; //攻撃開始時間を設定
            }
        }
    }

    //突進
    private void Pattern3()
    {
        //開始直後なら
        if (aStartFlag)
        {
            GetComponent<Animator>().SetBool(tackleAnim, true); //アニメーションをセット
            aStartFlag = false; //開始フラグをfalseに
        }
        //一旦距離をとる
        if (ETime - attackStart < 0.5f && scaleTime <= 0.0f)
        {
            SetScaleDis(0.8f, 0.5f);
        }

        //距離をとったら突進
        if (scale <= 0.8f && scaleTime <= 0.0f)
        {
            float period = 4.0f * (scale / 4.0f); //着弾までの時間を設定
            SetScaleDis(4.0f, period); //自機に向けて拡大
        }

        //一定時間経過したら
        if(3.0f <= ETime - attackStart)
        {
            GetComponent<Animator>().SetBool(tackleAnim, false); //アニメーションをセット
            pattern = 0; //パターンを戻す
            aStartFlag = true; //開始フラグをtrueに
            attackStart = ETime; //攻撃開始時間を設定
        }
    }

    //自機に当たったときの動作
    public override void Hit()
    {
        SetScaleDis(2.0f, 0.3f); //距離をとる
    }
}
