using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PMachine : MonoBehaviour
{
    [SerializeField]
    private Text gauge = null; //EN
    [SerializeField]
    private GameObject cursor = null; //�Ə�
    [SerializeField]
    private GameObject lArm = null; //���r
    [SerializeField]
    private GameObject rArm = null; //�E�r
    [SerializeField]
    private AudioClip damSound = null; //�_���[�W���̌��ʉ�
    private bool moveFlag; //����ł��邩
    private int energy; //�c��G�l���M�[
    private bool attackFlag = false; //�U������
    public bool MoveFlag { get { return moveFlag; } set { moveFlag = value; } } //����ł��邩
    public int Energy { get { return energy; } } //�c��G�l���M�[

    // Start is called before the first frame update
    void Start()
    {
        energy = 500; //�G�l���M�[��������
    }

    // Update is called once per frame
    void Update()
    {
        if (energy <= 0) return;

        //�J�[�\���ړ�
        Vector3 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); //�}�E�X�J�[�\���̈ʒu���擾
        cursorPos.x = Mathf.Clamp(cursorPos.x, -7.5f, 7.5f); //x�����̈ړ�����
        cursorPos.y = Mathf.Clamp(cursorPos.y, -3.5f, 3.5f); //y�����̈ړ�����
        cursorPos.z = 0.0f; //z���W�̎w��
        cursor.transform.position = cursorPos; //�J�[�\���ʒu�̐ݒ�

        //�U�����łȂ��A���N���b�N���ꂽ��
        if (Input.GetMouseButtonDown(0) && !attackFlag)
        {
            lArm.GetComponent<ArmAction>().SetAttack(cursor.transform.position); //�p���`������
            attackFlag = true; //�U���t���O��true��
        }
        //�U�����łȂ��A�E�N���b�N���ꂽ��
        if (Input.GetMouseButtonDown(1) && !attackFlag)
        {
            rArm.GetComponent<ArmAction>().SetAttack(cursor.transform.position); //�p���`������
            attackFlag = true; //�U���t���O��true��
        }

        //�U�����I�������
        if(lArm.GetComponent<ArmAction>().EndFlag && rArm.GetComponent<ArmAction>().EndFlag)
        {
            attackFlag = false; //�U���t���O��false��
        }

        //�G�l���M�[�\��
        gauge.text = "EN:" + energy.ToString();
    }

    //�U������
    private void Attack()
    {

    }

    //�_���[�W����
    public void Damage(int dam)
    {
        GetComponent<AudioSource>().PlayOneShot(damSound); //�_���[�W���̌��ʉ���炷
        energy -= dam; //�_���[�W���G�l���M�[�����炷
        energy = (int)Mathf.Clamp((float)energy, 0.0f, 500.0f);
    }
}
