using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditorInternal.ReorderableList;

public class InventoryController : Controller
{

    private ItemComponent[] itemComponent;
    private AchievementComponent[] achievementComponent;
    private int Size;
    private int ItemCount;


    private InventoryView _view;
    private InventoryModel _model;

    public GameObject itemCellPrefab;
    public GameObject inventoryPanel;

    public GameObject achievementPrefab;
    public GameObject achievementPanel;

    public GameObject ItemInfoPanel;
    public Image ItemInfoImage;
    public Button ItemButton;
    public InventoryController(InventoryView view, InventoryModel model)
    {
        _view = view;
        _model = model;

        inventoryPanel = _view.inventoryPanel;
        itemCellPrefab = _view.itemCellPrefab;
        achievementPrefab = _view.achievementPrefab;
        achievementPanel = _view.achievementPanel;
        ItemInfoPanel = _view.ItemInfoPanel;
        ItemInfoImage = _view.ItemInfoImage;
        ItemButton = _view.ItemButton;

        Size = _model.InventorySlotNnumber;

    }   

    public override void Init() 
    {
        CreateAchievementSlots();
        CreateInventorySlots();
        SetClickOnButtons();
        CreateInventoryBackImage();
    }

    private void CreateAchievementSlots()
    {
        achievementComponent = new AchievementComponent[5];

        for (int a = 0; a < achievementComponent.Length; a++)
        {
            var cell = GameObject.Instantiate(achievementPrefab);
            cell.transform.SetParent(achievementPanel.transform, false);
            achievementComponent[a] = cell.GetComponent<AchievementComponent>();
            achievementComponent[a].Index = a;
        }
    }

    private void CreateInventorySlots()
    {
       
        itemComponent = new ItemComponent[Size];

        for (int a = 0; a < itemComponent.Length; a++)
        {
            var cell = GameObject.Instantiate(itemCellPrefab);
            cell.transform.SetParent(inventoryPanel.transform, false);
            itemComponent[a] = cell.GetComponent<ItemComponent>();
            itemComponent[a].Index = a;
        }
    }

    private void SetClickOnButtons()
    {
        for (int a = 0; a < Size; a++)
        {
            var temp = a;
            itemComponent[a].Button.onClick.AddListener(() =>
            {
                Click(temp);
            });
        }
    }

    

    private void CreateInventoryBackImage()
    {
        for (int a = 0; a < itemComponent.Length; a++)
        {            
            itemComponent[a].BackImage.sprite = _view.BackSprite;
        }
    }

    public void UpdateInventory(ItemType type)
    {
        ItemCount = 0;
        for (int a = 0; a < itemComponent.Length; a++)
        {
            if (_view.ItemArr.Length > a && _view.ItemArr[a].Type == type)
            {
                itemComponent[a].ItemImage.gameObject.SetActive(true);
                itemComponent[a].ItemImage.sprite = _view.ItemArr[a].ItemImage;
                ItemCount++;
            }
            else 
            {
                itemComponent[a].ItemImage.gameObject.SetActive(false);
            } 
        }
    }

    public void AllItemInventory(bool IsAll)
    {
        ItemCount = 0;
        var Default = ItemType.Defoult; 

        if (IsAll) 
        {
            for (int a = 0; a < itemComponent.Length; a++)
            {
                if (_view.ItemArr.Length > a)
                {
                    itemComponent[a].ItemImage.gameObject.SetActive(true);
                    itemComponent[a].ItemImage.sprite = _view.ItemArr[a].ItemImage;
                    ItemCount++;
                }
                else
                {
                    itemComponent[a].ItemImage.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int a = 0; a < itemComponent.Length; a++ )
            {
                if (_view.ItemArr.Length > a && _view.ItemArr[a].Type != Default)
                {
                    itemComponent[a].ItemImage.gameObject.SetActive(true);
                    itemComponent[a].ItemImage.sprite = _view.ItemArr[a].ItemImage;
                    ItemCount++;
                }
                else
                {
                    itemComponent[a].ItemImage.gameObject.SetActive(false);
                }
            }

        }
    
    }

    private void Click(int temp)
    {
        if (ItemCount - 1 <= temp)
        {
            ItemButton.gameObject.SetActive(true);
            ItemInfoPanel.gameObject.SetActive(true);
            ItemInfoImage.sprite = itemComponent[temp].ItemImage.sprite;

        }
            
    }
}
