using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelInfoObject", menuName = "ScriptableObjects/new LevelInfoObject", order = 1)]

public class LevelInfoObject : ScriptableObject
{
    public Sprite[] EnemySprite;
    public Sprite[] DropSprite;


}
