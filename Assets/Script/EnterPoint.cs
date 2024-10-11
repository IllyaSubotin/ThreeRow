using System;
using UnityEngine;
using UnityEngine.UI;

public class EnterPoint : MonoBehaviour
{
    [SerializeField]
    private ThreeRowView ThreeObjectsView;

    [SerializeField]
    private InventoryView InventoryObjectsView;

    [SerializeField]
    private MarketView MarketObjectsView;

    [SerializeField]
    private ThreeRowObject[] ThreeObjects;

    [SerializeField]
    private Button[] LevelButton;

    public InventoryController controller;

    public UiManager uiManager;

    
 
    void Start()
    {
        BattleStart();
        MarketStart();


        var controler = InventoryStart();

        uiManager.ItemButton.onClick.AddListener(() =>
        {
            controler.AllItemInventory(false);
        });

        uiManager.AllItemButton.onClick.AddListener(() =>
        {
            controler.AllItemInventory(true);
        });

        for (int a = 0; a < uiManager.SlotButton.Length; a++)
        {
            var temp = a;
            uiManager.SlotButton[temp].onClick.AddListener(() =>
            {
                controler.UpdateInventory(uiManager.types[temp]);
            });
        }

        for (int a = 0; a < uiManager.SortButton.Length; a++)
        {
            var temp = a;
            uiManager.SortButton[temp].onClick.AddListener(() =>
            {
                controler.UpdateInventory(uiManager.types[temp]);
            });
        }

    }

   
    public InventoryController InventoryStart()
    {        
        var model = new InventoryModel();
        controller = new InventoryController(InventoryObjectsView, model);        
        controller.Init();  
        return controller;    
    }
    private void MarketStart()
    {
        var model = new MarketModel();
        var controller = new MarketController(MarketObjectsView, model);
        controller.Init();
    }

    public void BattleStart()
    {
        var model = new ThreeRowModel(ThreeObjects[0]);
        var controller = new ThreeRowControler(ThreeObjectsView, model);
        controller.Init();

        for (int c = 0; c < LevelButton.Length; c++) 
        {
            LevelButton[c].onClick.AddListener(() =>
            {
                controller.EnemyAndPlayerUpdate(c);
                controller.FonsUpdate();
            });
        }
       
    }

}
