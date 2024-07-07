using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAction : MonoBehaviour
{
    [SerializeField]
    private float colTime = 0.0f; //パンチの持続時間
    [SerializeField]
    private AudioClip sound = null; //開始時の効果音

    private readonly float punchTime = 0.05f; //パンチ完了までの時間
    private Vector3 startPos; //開始位置
    private Vector3 punchPos; //パンチを撃つ位置
    private bool punched = false; //パンチを撃ったか
    private bool moveFlag = false; //腕を動かすか
    private bool endFlag = true; //パンチを撃ち終えたか
    private float startTime; //パンチを撃ち始めた時間
    public bool EndFlag { get { return endFlag; } } //終了フラグ

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //開始位置を設定
        startTime = Time.time; //開始時間を設定
        GetComponent<CircleCollider2D>().enabled = false; //当たり判定を消す
    }

    // Update is called once per frame
    void Update()
    {
        //腕を動かすなら
        if (moveFlag)
        {
            float t = (Time.time - startTime) / punchTime; //補完値を設定
            transform.position = Vector3.Lerp(startPos, punchPos, t); //腕を移動させる

            //パンチ位置に達した場合
            if (1.0f <= t)
            {
                //パンチ済みなら
                if (punched)
                {
                    endFlag = true; //終了フラグをtrueに
                    gameObject.SetActive(false); //非アクティブにする
                }
                else
                {
                    punchPos = startPos; //戻す位置を設定
                    startPos = transform.position; //開始位置を設定
                    moveFlag = false; //腕の移動フラグをfalseに
                    startTime = Time.time; //開始時間を設定
                    GetComponent<CircleCollider2D>().enabled = true; //当たり判定を発生
                    punched = true; //パンチ済フラグをtrueに
                }
            }
        }
        //動かさないなら
        else
        {
            //パンチ済みなら
            if (punched)
            {
                //持続時間を過ぎたら
                if (colTime <= Time.time - startTime)
                {
                    moveFlag = true; //腕の移動フラグをtrueに
                    startTime = Time.time; //開始時間を設定
                    GetComponent<CircleCollider2D>().enabled = false; //当たり判定を消す
                }
            }
        }
    }

    //攻撃開始
    public void SetAttack(Vector3 target)
    {
        gameObject.SetActive(true); //アクティブにする
        startPos = transform.position; //開始位置を設定
        punchPos = target; //パンチ位置を設定
        moveFlag = true; //腕の移動フラグをtrueに
        endFlag = false; //終了フラグをfalseに
        punched = false; //パンチ済フラグをfalseに
        startTime = Time.time; //開始時間を設定
        GetComponent<CircleCollider2D>().enabled = false; //当たり判定を消す
        GetComponent<AudioSource>().PlayOneShot(sound); //効果音を鳴らす
    }
}
