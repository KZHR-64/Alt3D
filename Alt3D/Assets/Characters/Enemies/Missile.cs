using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Enemies
{
    [SerializeField]
    private AudioClip sound; //���ˎ��̌��ʉ�
    private float period = 5.0f; //����

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        superArmor = true; //�X�[�p�[�A�[�}�[��ݒ�
        period = 5.0f * (scale / 4.0f); //���e�܂ł̎��Ԃ�ݒ�

        GetComponent<AudioSource>().PlayOneShot(sound); //���ˎ��̌��ʉ���炷
        SetScaleDis(4.0f, period); //���@�Ɍ����Ċg��
    }

    //�p�����̍X�V
    protected override void SubUpdate()
    {
    }

    //���@�ɓ��������Ƃ��̓���
    public override void Hit() {
        Destroy(gameObject); //�I�u�W�F�N�g������
    }
}
