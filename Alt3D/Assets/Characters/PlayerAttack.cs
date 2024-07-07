using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private int damage = 0; //ダメージ
    public int Damage { get { return damage; } } //ダメージ
}
