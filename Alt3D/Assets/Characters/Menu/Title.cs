using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private int sceneNum = 1; //���[�h����V�[��


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //FPS��60��
    }

    //���̃V�[�������[�h
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneNum);
    }

    //�Q�[�����I��
    public void GameEnd()
    {
        Application.Quit();
    }
}
