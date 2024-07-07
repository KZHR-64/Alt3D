using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmAction : MonoBehaviour
{
    [SerializeField]
    private float colTime = 0.0f; //�p���`�̎�������
    [SerializeField]
    private AudioClip sound = null; //�J�n���̌��ʉ�

    private readonly float punchTime = 0.05f; //�p���`�����܂ł̎���
    private Vector3 startPos; //�J�n�ʒu
    private Vector3 punchPos; //�p���`�����ʒu
    private bool punched = false; //�p���`����������
    private bool moveFlag = false; //�r�𓮂�����
    private bool endFlag = true; //�p���`�������I������
    private float startTime; //�p���`�������n�߂�����
    public bool EndFlag { get { return endFlag; } } //�I���t���O

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //�J�n�ʒu��ݒ�
        startTime = Time.time; //�J�n���Ԃ�ݒ�
        GetComponent<CircleCollider2D>().enabled = false; //�����蔻�������
    }

    // Update is called once per frame
    void Update()
    {
        //�r�𓮂����Ȃ�
        if (moveFlag)
        {
            float t = (Time.time - startTime) / punchTime; //�⊮�l��ݒ�
            transform.position = Vector3.Lerp(startPos, punchPos, t); //�r���ړ�������

            //�p���`�ʒu�ɒB�����ꍇ
            if (1.0f <= t)
            {
                //�p���`�ς݂Ȃ�
                if (punched)
                {
                    endFlag = true; //�I���t���O��true��
                    gameObject.SetActive(false); //��A�N�e�B�u�ɂ���
                }
                else
                {
                    punchPos = startPos; //�߂��ʒu��ݒ�
                    startPos = transform.position; //�J�n�ʒu��ݒ�
                    moveFlag = false; //�r�̈ړ��t���O��false��
                    startTime = Time.time; //�J�n���Ԃ�ݒ�
                    GetComponent<CircleCollider2D>().enabled = true; //�����蔻��𔭐�
                    punched = true; //�p���`�σt���O��true��
                }
            }
        }
        //�������Ȃ��Ȃ�
        else
        {
            //�p���`�ς݂Ȃ�
            if (punched)
            {
                //�������Ԃ��߂�����
                if (colTime <= Time.time - startTime)
                {
                    moveFlag = true; //�r�̈ړ��t���O��true��
                    startTime = Time.time; //�J�n���Ԃ�ݒ�
                    GetComponent<CircleCollider2D>().enabled = false; //�����蔻�������
                }
            }
        }
    }

    //�U���J�n
    public void SetAttack(Vector3 target)
    {
        gameObject.SetActive(true); //�A�N�e�B�u�ɂ���
        startPos = transform.position; //�J�n�ʒu��ݒ�
        punchPos = target; //�p���`�ʒu��ݒ�
        moveFlag = true; //�r�̈ړ��t���O��true��
        endFlag = false; //�I���t���O��false��
        punched = false; //�p���`�σt���O��false��
        startTime = Time.time; //�J�n���Ԃ�ݒ�
        GetComponent<CircleCollider2D>().enabled = false; //�����蔻�������
        GetComponent<AudioSource>().PlayOneShot(sound); //���ʉ���炷
    }
}
