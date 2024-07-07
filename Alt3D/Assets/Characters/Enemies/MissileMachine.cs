using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileMachine : Enemies
{
    [SerializeField]
    private GameObject missile; //�~�T�C��
    [SerializeField]
    private Transform lArm; //���r
    [SerializeField]
    private Transform rArm; //�E�r
    private int pattern; //�s���p�^�[��
    private float attackStart; //�U���J�n����
    private bool aStartFlag = true; //�U���J�n���ォ
    private bool rFlag = false; //�E�̍U���i�ړ��j��������
    private float xDis; //�ړ���
    private readonly int idleAnim = Animator.StringToHash("IsIdle"); //��~��Ԃ̃n�b�V��
    private readonly int launchAnim = Animator.StringToHash("IsLaunch"); //���ˏ�Ԃ̃n�b�V��
    private readonly int lMoveAnim = Animator.StringToHash("IsLMove"); //���ړ���Ԃ̃n�b�V��
    private readonly int rMoveAnim = Animator.StringToHash("IsRMove"); //�E�ړ���Ԃ̃n�b�V��
    private readonly int tackleAnim = Animator.StringToHash("IsTackle"); //�ːi��Ԃ̃n�b�V��

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        pattern = 0; //�p�^�[����ݒ�
        damage = 100; //�ڐG�_���[�W��ݒ�
    }

    //�p�����̍X�V
    protected override void SubUpdate()
    {
        switch(pattern)
        {
            case 0:
                Pattern0(); //����
                break;
            case 1:
                Pattern1(); //�~�T�C��
                break;
            case 2:
                Pattern2(); //��O���؂�
                break;
            case 3:
                Pattern3(); //�ːi
                break;
            default:
                pattern = 0;
                break;
        }
    }

    //����
    private void Pattern0()
    {
        //�J�n����Ȃ�
        if (aStartFlag)
        {
            xDis = (Random.value * 10.0f - 5.0f); //�ړ��������
            aStartFlag = false; //�J�n�t���O��false��
        }
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(xDis, -1.5f, 0.0f), 12.0f * Time.deltaTime); //�����Ɉړ�
        //���@����߂��Ȃ�
        if (1.0f < scale && scaleTime <= 0.0f)
        {
            SetScaleDis(1.0f, 1.0f); //����̈ʒu�Ɉړ�
        }
        if (scale <= 1.0f && 3.0f < ETime - attackStart)
        {
            aStartFlag = true; //�J�n�t���O��true��
            attackStart = ETime; //�U���J�n���Ԃ�ݒ�
            pattern = (int)(Random.value * 3.0f + 1.0f); //�p�^�[����ݒ�
        }
    }

    //�~�T�C��
    private void Pattern1()
    {
        //�J�n����Ȃ�
        if (aStartFlag)
        {
            GetComponent<Animator>().SetBool(launchAnim, true); //�A�j���[�V�������Z�b�g
            aStartFlag = false; //�J�n�t���O��false��
            rFlag = true; //�t���O��true��
        }
        //��莞�Ԍo�߂�����
        if (0.5f <= ETime - attackStart && ETime - attackStart < 1.0f && rFlag)
        {
            GameObject mis = Instantiate(missile, lArm.position, transform.rotation); //�~�T�C���𐶐�
            mis.GetComponent<Enemies>().Scale = scale; //�g�嗦��ݒ�
            rFlag = false; //�t���O��false��
        }
        if (1.0f <= ETime - attackStart && ETime - attackStart < 1.5f && !rFlag)
        {
            GameObject mis = Instantiate(missile, rArm.position, transform.rotation); //�~�T�C���𐶐�
            mis.GetComponent<Enemies>().Scale = scale; //�g�嗦��ݒ�
            rFlag = true; //�t���O��true��
        }
        if (2.0f <= ETime - attackStart)
        {
            GetComponent<Animator>().SetBool(launchAnim, false); //�A�j���[�V�������Z�b�g
            pattern = 0; //�p�^�[����߂�
            aStartFlag = true; //�J�n�t���O��true��
            attackStart = ETime; //�U���J�n���Ԃ�ݒ�
        }
    }

    //��O���؂�
    private void Pattern2()
    {
        //�J�n����Ȃ�
        if (aStartFlag)
        {
            GetComponent<Animator>().SetBool(rMoveAnim, true); //�A�j���[�V�������Z�b�g
            aStartFlag = false; //�J�n�t���O��false��
            rFlag = true; //�t���O��true��
        }
        //��ʊO�Ɉړ�
        if (rFlag)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(16.0f, -1.5f, 0.0f), 24.0f * Time.deltaTime); //��ʊO�Ɉړ�
            //��ʊO�ɏo����g��
            if (16.0f <= transform.position.x)
            {
                GetComponent<Animator>().SetBool(rMoveAnim, false); //�A�j���[�V�������Z�b�g
                GetComponent<Animator>().SetBool(lMoveAnim, true); //�A�j���[�V�������Z�b�g
                rFlag = false; //�t���O��false��
                Scale = 2.5f;
            }
        }
        //���@�̑O�����؂�
        if (1.0f <= ETime - attackStart && !rFlag)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-16.0f, -1.5f, 0.0f), 24.0f * Time.deltaTime); //��ʊO�Ɉړ�
            //��ʊO�ɏo����߂�
            if (transform.position.x <= -16.0f)
            {
                GetComponent<Animator>().SetBool(lMoveAnim, false); //�A�j���[�V�������Z�b�g
                pattern = 0; //�p�^�[����߂�
                aStartFlag = true; //�J�n�t���O��true��
                attackStart = ETime; //�U���J�n���Ԃ�ݒ�
            }
        }
    }

    //�ːi
    private void Pattern3()
    {
        //�J�n����Ȃ�
        if (aStartFlag)
        {
            GetComponent<Animator>().SetBool(tackleAnim, true); //�A�j���[�V�������Z�b�g
            aStartFlag = false; //�J�n�t���O��false��
        }
        //��U�������Ƃ�
        if (ETime - attackStart < 0.5f && scaleTime <= 0.0f)
        {
            SetScaleDis(0.8f, 0.5f);
        }

        //�������Ƃ�����ːi
        if (scale <= 0.8f && scaleTime <= 0.0f)
        {
            float period = 4.0f * (scale / 4.0f); //���e�܂ł̎��Ԃ�ݒ�
            SetScaleDis(4.0f, period); //���@�Ɍ����Ċg��
        }

        //��莞�Ԍo�߂�����
        if(3.0f <= ETime - attackStart)
        {
            GetComponent<Animator>().SetBool(tackleAnim, false); //�A�j���[�V�������Z�b�g
            pattern = 0; //�p�^�[����߂�
            aStartFlag = true; //�J�n�t���O��true��
            attackStart = ETime; //�U���J�n���Ԃ�ݒ�
        }
    }

    //���@�ɓ��������Ƃ��̓���
    public override void Hit()
    {
        SetScaleDis(2.0f, 0.3f); //�������Ƃ�
    }
}
