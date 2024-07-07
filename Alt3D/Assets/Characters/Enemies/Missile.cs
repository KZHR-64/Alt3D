using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemies
{
    [SerializeField]
    private AudioClip sound; //発射時の効果音
    private float period = 5.0f; //周期

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        superArmor = true; //スーパーアーマーを設定
        period = 5.0f * (scale / 4.0f); //着弾までの時間を設定

        GetComponent<AudioSource>().PlayOneShot(sound); //発射時の効果音を鳴らす
        SetScaleDis(4.0f, period); //自機に向けて拡大
    }

    //継承側の更新
    protected override void SubUpdate()
    {
    }

    //自機に当たったときの動作
    public override void Hit() {
        Destroy(gameObject); //オブジェクトを消す
    }
}
