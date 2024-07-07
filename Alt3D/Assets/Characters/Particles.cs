using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField]
    private float delTime = 0.0f; //Á‚¦‚é‚Ü‚Å‚ÌŠÔ
    [SerializeField]
    private AudioClip sound; //”­¶‚ÌŒø‰Ê‰¹

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(sound); //Œø‰Ê‰¹‚ğ–Â‚ç‚·
        Destroy(gameObject, delTime); //ˆê’èŠÔŒã‚ÉÁ‚·
    }
}
