using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField]
    private int hp; //HP
    [SerializeField]
    private GameObject brokenEffect; //�j�Ђ̃G�t�F�N�g
    //private GameObject hitEffect; //�q�b�g���̃G�t�F�N�g
    private bool moveFlag = true; //���삳���邩
    protected bool superArmor = false; //�X�[�p�[�A�[�}�[
    protected float scale = 1.0f; //�g�嗦
    protected float scaleDis; //�g��k���̖ڕW�l
    protected float scaleTime = 0.0f; //�g��k���ɂ����鎞��
    protected float scaleStart = 0.0f; //�J�n���̊g�嗦
    protected float scaleTimeStart = 0.0f; //�J�n���̎���
    private float startTime = 0.0f; //�o�����̎���
    protected int damage = 50; //�ڐG���̃_���[�W
    private float damageTime; //�_���[�W����̎���
    protected bool hitFlag; //���@�ƐڐG������
    private readonly float dTime = 2.0f; //�_���[�W���Ԃ̊�l
    public bool MoveFlag { get { return moveFlag; } set { moveFlag = value;} } //�g�嗦
    public float Scale { get { return scale; } set { scale = value; scaleDis = scale; } } //�g�嗦
    protected float ETime { get { return Time.time - startTime; } } //�o�Ă���o�߂�������
    public int Damage { get { return damage; } } //�ڐG�_���[�W
    public bool HitFlag { get { return hitFlag; } set { hitFlag = value; } } //���@�ƐڐG������

    // Start is called before the first frame update
    protected void Start()
    {
        startTime = Time.time; //�o�����̎��Ԃ�ݒ�
    }

    // Update is called once per frame
    protected void Update()
    {
        if(moveFlag) SubUpdate(); //�p�����ł̍X�V
        ScaleUpdate(); //�g�嗦�̍X�V

        //�_���[�W���Ȃ�
        if(0.0f < damageTime)
        {
            damageTime -= Time.deltaTime; //�_���[�W���Ԃ����炷
            if (damageTime <= 0.0f) moveFlag = true; //�I�������瓮��ĊJ
        }
    }

    //�g�嗦�̍X�V
    protected void ScaleUpdate()
    {
        //�g�k����Ȃ�
        if (0.0f < scaleTime)
        {
            float t = (ETime - scaleTimeStart) / scaleTime; //�⊮�l��ݒ�
            if (1.0f <= t) scaleTime = 0.0f; //�g�k�̏I������
            scale = Mathf.Lerp(scaleStart, scaleDis, t); //�ڕW�l�Ɍ����Ċg�k
        }
        transform.localScale = new Vector3(scale, scale, 1.0f); //�g�嗦��ݒ�

        //���ȏ�g�債����
        if (2.0f <= scale)
        {
            GetComponent<BoxCollider2D>().enabled = true; //�����蔻��𔭐�
            //GetComponent<Renderer>().material.color = Color.white; //�{���̐F�ɂ���
            SetColor(Color.white); //�{���̐F�ɂ���
        }
        else
        {
            GetComponent<BoxCollider2D>().enabled = false; //�����蔻��𖳌�
            //GetComponent<Renderer>().material.color = new Color(0.74f, 0.96f, 1.0f, 1.0f); //���������F�ɂ���
            SetColor(new Color(0.74f, 0.96f, 1.0f, 1.0f)); //���������F�ɂ���
        }
    }

    //�����蔻��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("PlayerAttack")) return; //���@�̍U���łȂ��Ȃ�I��
        int damage = collision.GetComponent<PlayerAttack>().Damage; //�_���[�W���擾
        hp -= damage; //HP���_���[�W�����炷

        Instantiate(brokenEffect, collision.transform.position, transform.rotation); //�p�[�e�B�N���𔭐�

        //�X�[�p�[�A�[�}�[���Ȃ��Ȃ�
        if (!superArmor)
        {
            moveFlag = false; //����t���O��false��
            damageTime = dTime * ((float)damage / 4.0f); //�_���[�W�ɉ������_���[�W���Ԃ�ݒ�
            SetScaleDis(scale - 0.5f * damageTime, 0.5f); //�������
        }

        //HP��0�ɂȂ�����
        if (hp <= 0)
        {
            Defeat(); //���ꂽ���̏���
            Destroy(gameObject); //�I�u�W�F�N�g������
        }
    }

    //�g��k���̖ڕW�l��ݒ�
    public void SetScaleDis(float sd, float st)
    {
        scaleDis = sd; //�ڕW�l��ݒ�
        scaleTime = st; //�����鎞�Ԃ�ݒ�
        scaleStart = scale; //�J�n���̊g�嗦��ݒ�
        scaleTimeStart = ETime; //�J�n���̎��Ԃ�ݒ�
    }

    //�F�̕ύX
    private void SetColor(Color col)
    {
        var rend = GetComponent<Renderer>(); //Renderer���擾
        //null�łȂ����
        if (rend)
        {
            rend.material.color = col; //�F��ύX
        }
        //�q�̐F��ύX
        foreach(Transform child in transform)
        {
            var cRend = child.gameObject.GetComponent<Renderer>(); //Renderer���擾
            //null�łȂ����
            if (cRend)
            {
                cRend.material.color = col; //�F��ύX
            }
        }
    }

    //�p�����̍X�V
    protected virtual void SubUpdate() { }

    //���@�ɓ��������Ƃ��̓���
    public virtual void Hit() { }

    //���ꂽ���̓���
    protected virtual void Defeat() { }
}
