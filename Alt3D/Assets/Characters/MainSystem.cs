using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSystem : MonoBehaviour
{
    private GameObject player; //���@
    [SerializeField]
    private Text endMessage = null; //�I�����b�Z�[�W
    [SerializeField]
    private Text endMessage2 = null; //�I�����b�Z�[�W
    [SerializeField]
    private AudioSource bgm = null; //BGM
    [SerializeField]
    private AudioClip gong = null; //�S���O
    [SerializeField]
    private AudioClip clearJingle = null; //�N���A���̃W���O��
    [SerializeField]
    private AudioClip gameoverJingle = null; //�Q�[���I�[�o�[���̃W���O��
    private bool endFlag = true; //�퓬���I��������
    private bool backFlag = false; //�V�[����߂�邩

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //FPS��60��
        player = GameObject.FindGameObjectWithTag("Player"); //���@���擾
        endMessage.enabled = false; //���b�Z�[�W���\����
        endMessage2.enabled = false; //���b�Z�[�W���\����
        StartCoroutine(BattleStandby()); //�퓬��ҋ@
    }

    // Update is called once per frame
    void Update()
    {
        //�O�̃V�[���ɖ߂��Ȃ�
        if(backFlag)
        {
            //�N���b�N������
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                SceneManager.LoadScene(0); //�^�C�g���ɖ߂�
            }
        }
        //�I�����Ă���Ȃ�
        if (endFlag)
        {
            return; //�ȍ~�̏������΂�
        }
        GameObject[] eList = GameObject.FindGameObjectsWithTag("Enemy"); //�G���擾
        //�G���S�ł�����
        if(eList.Length == 0)
        {
            endMessage.text = "CLEAR!"; //���b�Z�[�W��ݒ�
            endFlag = true; //�I���t���O��true��
            StartCoroutine(EndCheck(clearJingle)); //�I���m�F���J�n
        }
        //����Ȃ�
        else
        {
            //�G�̐�����
            foreach (var e in eList)
            {
                Enemies econ = e.GetComponent<Enemies>();
                //���Ȃ�߂Â�����
                if (4.0f <= econ.Scale && !econ.HitFlag)
                {
                    int dam = econ.Damage;
                    player.GetComponent<PMachine>().Damage(dam); //���@�Ƀ_���[�W
                    econ.HitFlag = true; //�ڐG�t���O��true��
                    econ.Hit(); //�����������̓���
                }
                else
                {
                    econ.HitFlag = false; //�ڐG�t���O��false��
                }
            }
        }

        //���@��HP��0�ɂȂ�����
        if(player.GetComponent<PMachine>().Energy <= 0)
        {
            endMessage.text = "GAME OVER"; //���b�Z�[�W��ݒ�
            endFlag = true; //�I���t���O��true��
            StartCoroutine(EndCheck(gameoverJingle)); //�I���m�F���J�n
        }
    }

    //�J�n�O�̏���
    private IEnumerator BattleStandby()
    {
        endMessage.enabled = true; //���b�Z�[�W��\��
        endMessage.text = "READY..."; //���b�Z�[�W��ݒ�
        yield return new WaitForSeconds(1.0f); //1�b�҂�
        endMessage.text = "GO!"; //���b�Z�[�W��ݒ�
        GetComponent<AudioSource>().PlayOneShot(gong); //�S���O���Đ�
        yield return new WaitForSeconds(0.5f); //0.5�b�҂�
        endMessage.enabled = false; //���b�Z�[�W���\����
        bgm.Play(); //BGM���Đ�
        endFlag = false; //�I���t���O��false��
    }

    //�I�����邩����
    private IEnumerator EndCheck(AudioClip playJingle)
    {
        bgm.Stop(); //BGM���~
        //�p�[�e�B�N�����Ȃ��Ȃ�܂őҋ@
        while (GameObject.FindGameObjectWithTag("Particles") != null)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f); //1�b�҂�
        endMessage.enabled = true; //���b�Z�[�W��\��
        endMessage2.enabled = true; //���b�Z�[�W��\��
        GetComponent<AudioSource>().PlayOneShot(playJingle); //�W���O�����Đ�
        backFlag = true; //�V�[����߂�t���O��true��
    }
}
