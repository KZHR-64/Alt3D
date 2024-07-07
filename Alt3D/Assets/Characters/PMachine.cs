using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PMachine : MonoBehaviour
{
    [SerializeField]
    private Text gauge = null; //EN
    [SerializeField]
    private GameObject cursor = null; //照準
    [SerializeField]
    private GameObject lArm = null; //左腕
    [SerializeField]
    private GameObject rArm = null; //右腕
    [SerializeField]
    private AudioClip damSound = null; //ダメージ時の効果音
    private bool moveFlag; //操作できるか
    private int energy; //残りエネルギー
    private bool attackFlag = false; //攻撃中か
    public bool MoveFlag { get { return moveFlag; } set { moveFlag = value; } } //操作できるか
    public int Energy { get { return energy; } } //残りエネルギー

    // Start is called before the first frame update
    void Start()
    {
        energy = 500; //エネルギーを初期化
    }

    // Update is called once per frame
    void Update()
    {
        if (energy <= 0) return;

        //カーソル移動
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //マウスカーソルの位置を取得
        cursorPos.x = Mathf.Clamp(cursorPos.x, -7.5f, 7.5f); //x方向の移動制限
        cursorPos.y = Mathf.Clamp(cursorPos.y, -3.5f, 3.5f); //y方向の移動制限
        cursorPos.z = 0.0f; //z座標の指定
        cursor.transform.position = cursorPos; //カーソル位置の設定

        //攻撃中でなく、左クリックされたら
        if (Input.GetMouseButtonDown(0) && !attackFlag)
        {
            lArm.GetComponent<ArmAction>().SetAttack(cursor.transform.position); //パンチを撃つ
            attackFlag = true; //攻撃フラグをtrueに
        }
        //攻撃中でなく、右クリックされたら
        if (Input.GetMouseButtonDown(1) && !attackFlag)
        {
            rArm.GetComponent<ArmAction>().SetAttack(cursor.transform.position); //パンチを撃つ
            attackFlag = true; //攻撃フラグをtrueに
        }

        //攻撃が終わったら
        if(lArm.GetComponent<ArmAction>().EndFlag && rArm.GetComponent<ArmAction>().EndFlag)
        {
            attackFlag = false; //攻撃フラグをfalseに
        }

        //エネルギー表示
        gauge.text = "EN:" + energy.ToString();
    }

    //攻撃判定
    private void Attack()
    {

    }

    //ダメージ操作
    public void Damage(int dam)
    {
        GetComponent<AudioSource>().PlayOneShot(damSound); //ダメージ時の効果音を鳴らす
        energy -= dam; //ダメージ分エネルギーを減らす
        energy = (int)Mathf.Clamp((float)energy, 0.0f, 500.0f);
    }
}
