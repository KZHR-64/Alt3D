using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target2 : Enemies
{
    [SerializeField]
    private GameObject nextTarget; //���ɌĂԓI
    private float period = 3.0f; //����
    private float amp = 14.0f; //�U�ꕝ

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        superArmor = true; //�X�[�p�[�A�[�}�[��ݒ�
        Scale = 2.0f; //�g�嗦��ݒ�
    }

    //�p�����̍X�V
    protected override void SubUpdate()
    {
        float t = 4.0f * amp * ETime / period; //�⊮�l��ݒ�
        float x = Mathf.PingPong(t, 2.0f * amp) - amp; //�ʒu���v�Z
        transform.position = new Vector3(x, 0.0f, 0.0f); //�ʒu��ݒ�
    }

    //���ꂽ���̓���
    protected override void Defeat()
    {
        Instantiate(nextTarget); //���̓I�𐶐�
    }
}
