using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField]
    private float delTime = 0.0f; //消えるまでの時間
    [SerializeField]
    private AudioClip sound; //発生時の効果音

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(sound); //効果音を鳴らす
        Destroy(gameObject, delTime); //一定時間後に消す
    }
}
