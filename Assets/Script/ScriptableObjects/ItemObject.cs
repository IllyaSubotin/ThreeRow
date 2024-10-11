using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum ItemType {Defoult, Helmet, Armor, Boots, Weapon, Shield, Ring}

[CreateAssetMenu(fileName = "ItemObject", menuName = "ScriptableObjects/new ItemObject", order = 1)]
public class ItemObject : ScriptableObject
{
    public ItemType Type;
    public string ItemName;
    public int ItemNumber;
    public int Price;
    public Sprite ItemImage;
   
}
