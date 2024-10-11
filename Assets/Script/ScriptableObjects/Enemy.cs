using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/new Enemy", order = 1)]
public class Enemy : ScriptableObject
{
    public float MaxHealth;
    public float Damage;
    public Sprite Image;
    public int Wait;

}
