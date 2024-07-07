using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSystem : MonoBehaviour
{
    private GameObject player; //自機
    [SerializeField]
    private Text endMessage = null; //終了メッセージ
    [SerializeField]
    private Text endMessage2 = null; //終了メッセージ
    [SerializeField]
    private AudioSource bgm = null; //BGM
    [SerializeField]
    private AudioClip gong = null; //ゴング
    [SerializeField]
    private AudioClip clearJingle = null; //クリア時のジングル
    [SerializeField]
    private AudioClip gameoverJingle = null; //ゲームオーバー時のジングル
    private bool endFlag = true; //戦闘が終了したか
    private bool backFlag = false; //シーンを戻れるか

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60; //FPSを60に
        player = GameObject.FindGameObjectWithTag("Player"); //自機を取得
        endMessage.enabled = false; //メッセージを非表示に
        endMessage2.enabled = false; //メッセージを非表示に
        StartCoroutine(BattleStandby()); //戦闘を待機
    }

    // Update is called once per frame
    void Update()
    {
        //前のシーンに戻れるなら
        if(backFlag)
        {
            //クリックしたら
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                SceneManager.LoadScene(0); //タイトルに戻る
            }
        }
        //終了しているなら
        if (endFlag)
        {
            return; //以降の処理を飛ばす
        }
        GameObject[] eList = GameObject.FindGameObjectsWithTag("Enemy"); //敵を取得
        //敵が全滅したら
        if(eList.Length == 0)
        {
            endMessage.text = "CLEAR!"; //メッセージを設定
            endFlag = true; //終了フラグをtrueに
            StartCoroutine(EndCheck(clearJingle)); //終了確認を開始
        }
        //いるなら
        else
        {
            //敵の数処理
            foreach (var e in eList)
            {
                Enemies econ = e.GetComponent<Enemies>();
                //かなり近づいたら
                if (4.0f <= econ.Scale && !econ.HitFlag)
                {
                    int dam = econ.Damage;
                    player.GetComponent<PMachine>().Damage(dam); //自機にダメージ
                    econ.HitFlag = true; //接触フラグをtrueに
                    econ.Hit(); //当たった時の動作
                }
                else
                {
                    econ.HitFlag = false; //接触フラグをfalseに
                }
            }
        }

        //自機のHPが0になったら
        if(player.GetComponent<PMachine>().Energy <= 0)
        {
            endMessage.text = "GAME OVER"; //メッセージを設定
            endFlag = true; //終了フラグをtrueに
            StartCoroutine(EndCheck(gameoverJingle)); //終了確認を開始
        }
    }

    //開始前の準備
    private IEnumerator BattleStandby()
    {
        endMessage.enabled = true; //メッセージを表示
        endMessage.text = "READY..."; //メッセージを設定
        yield return new WaitForSeconds(1.0f); //1秒待つ
        endMessage.text = "GO!"; //メッセージを設定
        GetComponent<AudioSource>().PlayOneShot(gong); //ゴングを再生
        yield return new WaitForSeconds(0.5f); //0.5秒待つ
        endMessage.enabled = false; //メッセージを非表示に
        bgm.Play(); //BGMを再生
        endFlag = false; //終了フラグをfalseに
    }

    //終了するか判定
    private IEnumerator EndCheck(AudioClip playJingle)
    {
        bgm.Stop(); //BGMを停止
        //パーティクルがなくなるまで待機
        while (GameObject.FindGameObjectWithTag("Particles") != null)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1.0f); //1秒待つ
        endMessage.enabled = true; //メッセージを表示
        endMessage2.enabled = true; //メッセージを表示
        GetComponent<AudioSource>().PlayOneShot(playJingle); //ジングルを再生
        backFlag = true; //シーンを戻るフラグをtrueに
    }
}
