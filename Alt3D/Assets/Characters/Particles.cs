using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particles : MonoBehaviour
{
    [SerializeField]
    private float delTime = 0.0f; //������܂ł̎���
    [SerializeField]
    private AudioClip sound; //�������̌��ʉ�

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().PlayOneShot(sound); //���ʉ���炷
        Destroy(gameObject, delTime); //��莞�Ԍ�ɏ���
    }
}
