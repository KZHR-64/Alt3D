using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2 : Enemies
{
    [SerializeField]
    private GameObject nextTarget; //次に呼ぶ的
    private float period = 3.0f; //周期
    private float amp = 14.0f; //振れ幅

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        superArmor = true; //スーパーアーマーを設定
        Scale = 2.0f; //拡大率を設定
    }

    //継承側の更新
    protected override void SubUpdate()
    {
        float t = 4.0f * amp * ETime / period; //補完値を設定
        float x = Mathf.PingPong(t, 2.0f * amp) - amp; //位置を計算
        transform.position = new Vector3(x, 0.0f, 0.0f); //位置を設定
    }

    //やられた時の動作
    protected override void Defeat()
    {
        Instantiate(nextTarget); //次の的を生成
    }
}
