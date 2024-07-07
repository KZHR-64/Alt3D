using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target3 : Enemies
{
    private readonly float period = 3.0f; //周期
    private readonly float amp = 10.0f; //振れ幅
    private readonly float scaleAmp = 2.0f; //振れ幅（拡大率）

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
        float t = 2.0f * amp * ETime / period; //補完値を設定
        float x = Mathf.PingPong(t, 2.0f * amp) - amp; //位置を計算
        float st = 4.0f * scaleAmp * ETime / period; //補完値を設定
        transform.position = new Vector3(x, 0.0f, 0.0f); //位置を設定
        Scale = Mathf.PingPong(st, scaleAmp) + 1.0f; //拡大率を設定
    }
}
