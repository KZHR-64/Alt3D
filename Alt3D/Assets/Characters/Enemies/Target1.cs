using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target1 : Enemies
{
    [SerializeField]
    private GameObject nextTarget; //���ɌĂԓI

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        Scale = 2.0f; //�g�嗦��ݒ�
    }

    //�p�����̍X�V
    protected override void SubUpdate()
    {
        //���@���牓���Ȃ�
        if(scale < 2.0f && scaleTime <= 0.0f)
        {
            SetScaleDis(2.0f, 1.0f); //�߂Â���
        }
    }

    //���ꂽ���̓���
    protected override void Defeat() {
        Instantiate(nextTarget); //���̓I�𐶐�
    }
}
