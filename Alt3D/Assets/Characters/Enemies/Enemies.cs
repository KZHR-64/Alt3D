using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private int hp; //HP
    [SerializeField]
    private GameObject brokenEffect; //破片のエフェクト
    //private GameObject hitEffect; //ヒット時のエフェクト
    private bool moveFlag = true; //動作させるか
    protected bool superArmor = false; //スーパーアーマー
    protected float scale = 1.0f; //拡大率
    protected float scaleDis; //拡大縮小の目標値
    protected float scaleTime = 0.0f; //拡大縮小にかける時間
    protected float scaleStart = 0.0f; //開始時の拡大率
    protected float scaleTimeStart = 0.0f; //開始時の時間
    private float startTime = 0.0f; //出た時の時間
    protected int damage = 50; //接触時のダメージ
    private float damageTime; //ダメージ動作の時間
    protected bool hitFlag; //自機と接触したか
    private readonly float dTime = 2.0f; //ダメージ時間の基準値
    public bool MoveFlag { get { return moveFlag; } set { moveFlag = value;} } //拡大率
    public float Scale { get { return scale; } set { scale = value; scaleDis = scale; } } //拡大率
    protected float ETime { get { return Time.time - startTime; } } //出てから経過した時間
    public int Damage { get { return damage; } } //接触ダメージ
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } } //自機と接触したか

    // Start is called before the first frame update
    protected void Start()
    {
        startTime = Time.time; //出た時の時間を設定
    }

    // Update is called once per frame
    protected void Update()
    {
        if(moveFlag) SubUpdate(); //継承側での更新
        ScaleUpdate(); //拡大率の更新

        //ダメージ中なら
        if(0.0f < damageTime)
        {
            damageTime -= Time.deltaTime; //ダメージ時間を減らす
            if (damageTime <= 0.0f) moveFlag = true; //終了したら動作再開
        }
    }

    //拡大率の更新
    protected void ScaleUpdate()
    {
        //拡縮するなら
        if (0.0f < scaleTime)
        {
            float t = (ETime - scaleTimeStart) / scaleTime; //補完値を設定
            if (1.0f <= t) scaleTime = 0.0f; //拡縮の終了判定
            scale = Mathf.Lerp(scaleStart, scaleDis, t); //目標値に向けて拡縮
        }
        transform.localScale = new Vector3(scale, scale, 1.0f); //拡大率を設定

        //一定以上拡大したら
        if (2.0f <= scale)
        {
            GetComponent<BoxCollider2D>().enabled = true; //当たり判定を発生
            //GetComponent<Renderer>().material.color = Color.white; //本来の色にする
            SetColor(Color.white); //本来の色にする
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false; //当たり判定を無効
            //GetComponent<Renderer>().material.color = new Color(0.74f, 0.96f, 1.0f, 1.0f); //少し青白い色にする
            SetColor(new Color(0.74f, 0.96f, 1.0f, 1.0f)); //少し青白い色にする
        }
    }

    //当たり判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerAttack")) return; //自機の攻撃でないなら終了
        int damage = collision.GetComponent<PlayerAttack>().Damage; //ダメージを取得
        hp -= damage; //HPをダメージ分減らす

        Instantiate(brokenEffect, collision.transform.position, transform.rotation); //パーティクルを発生

        //スーパーアーマーがないなら
        if (!superArmor)
        {
            moveFlag = false; //動作フラグをfalseに
            damageTime = dTime * ((float)damage / 4.0f); //ダメージに応じたダメージ時間を設定
            SetScaleDis(scale - 0.5f * damageTime, 0.5f); //吹っ飛ぶ
        }

        //HPが0になったら
        if (hp <= 0)
        {
            Defeat(); //やられた時の処理
            Destroy(gameObject); //オブジェクトを消す
        }
    }

    //拡大縮小の目標値を設定
    public void SetScaleDis(float sd, float st)
    {
        scaleDis = sd; //目標値を設定
        scaleTime = st; //かける時間を設定
        scaleStart = scale; //開始時の拡大率を設定
        scaleTimeStart = ETime; //開始時の時間を設定
    }

    //色の変更
    private void SetColor(Color col)
    {
        var rend = GetComponent<Renderer>(); //Rendererを取得
        //nullでなければ
        if (rend)
        {
            rend.material.color = col; //色を変更
        }
        //子の色を変更
        foreach(Transform child in transform)
        {
            var cRend = child.gameObject.GetComponent<Renderer>(); //Rendererを取得
            //nullでなければ
            if (cRend)
            {
                cRend.material.color = col; //色を変更
            }
        }
    }

    //継承側の更新
    protected virtual void SubUpdate() { }

    //自機に当たったときの動作
    public virtual void Hit() { }

    //やられた時の動作
    protected virtual void Defeat() { }
}
