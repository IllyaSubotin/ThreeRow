using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryView : View
{
    public GameObject itemCellPrefab;
    public GameObject inventoryPanel;
    public GameObject achievementPrefab;
    public GameObject achievementPanel;
    public GridLayoutGroup layoutGroup;

    public Sprite BackSprite;
    public ItemObject[] ItemArr;

    public GameObject ItemInfoPanel;
    public Image ItemInfoImage;
    public Button ItemButton;

}
