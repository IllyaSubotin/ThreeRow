using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/new Player", order = 1)]
public class Player : ScriptableObject
{
    public float MaxHealth;
    public float CurrentHealth;
    public float MaxMana;
    public float CurrentMana;
    public float Damage;



}
