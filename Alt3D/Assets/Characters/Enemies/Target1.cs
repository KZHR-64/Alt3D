using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target1 : Enemies
{
    [SerializeField]
    private GameObject nextTarget; //次に呼ぶ的

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Scale = 2.0f; //拡大率を設定
    }

    //継承側の更新
    protected override void SubUpdate()
    {
        //自機から遠いなら
        if(scale < 2.0f && scaleTime <= 0.0f)
        {
            SetScaleDis(2.0f, 1.0f); //近づける
        }
    }

    //やられた時の動作
    protected override void Defeat() {
        Instantiate(nextTarget); //次の的を生成
    }
}
