using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target3 : Enemies
{
    private readonly float period = 3.0f; //����
    private readonly float amp = 10.0f; //�U�ꕝ
    private readonly float scaleAmp = 2.0f; //�U�ꕝ�i�g�嗦�j

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
        float t = 2.0f * amp * ETime / period; //�⊮�l��ݒ�
        float x = Mathf.PingPong(t, 2.0f * amp) - amp; //�ʒu���v�Z
        float st = 4.0f * scaleAmp * ETime / period; //�⊮�l��ݒ�
        transform.position = new Vector3(x, 0.0f, 0.0f); //�ʒu��ݒ�
        Scale = Mathf.PingPong(st, scaleAmp) + 1.0f; //�g�嗦��ݒ�
    }
}
