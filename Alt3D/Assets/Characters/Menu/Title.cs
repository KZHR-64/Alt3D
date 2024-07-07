using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [SerializeField]
    private int sceneNum = 1; //ロードするシーン


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //FPSを60に
    }

    //次のシーンをロード
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneNum);
    }

    //ゲームを終了
    public void GameEnd()
    {
        Application.Quit();
    }
}
