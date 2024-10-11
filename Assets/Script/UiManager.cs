using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField]
    private GameObject FonsPanel;

    [SerializeField]
    private GameObject InventoryViewPanel;

    [SerializeField]
    public GameObject ThreeRowViewPanel;//

    [SerializeField]
    private GameObject MarketViewPanel;

    [SerializeField]
    private GameObject MarketCellPanel;

    [SerializeField]
    private GameObject MarketBuyPanel;

    [SerializeField]
    private GameObject InventoryHomePanel;

    [SerializeField]
    private GameObject AchievementPanel;

    [SerializeField]
    public GameObject HeaderMoneyMenuPanel;//

    [SerializeField]
    private GameObject HeaderPlayerMenuPanel;

    [SerializeField]
    public GameObject HubMenuPanel;//

    [SerializeField]
    private GameObject HubHomeMenuPanel;

    [SerializeField]
    public GameObject ButtlePanel; //

    [SerializeField]
    private GameObject ItemInfoPanel;

    [SerializeField]
    private GameObject BattleInfoPanel;

    [SerializeField]
    private Button HomeButton;

    [SerializeField]
    private Button ForgeButton;

    [SerializeField]
    private Button QuestButton;

    [SerializeField]
    private Button MarketButton;

    [SerializeField]
    private Button MapButton;

    [SerializeField]
    public Button ItemButton;

    [SerializeField]
    public Button AllItemButton;

    [SerializeField]
    private Button AchievementsButton;

    [SerializeField]
    private Button MarketCellBuyButton;

    [SerializeField]
    private Button ItemInfoReturnButton;


    [SerializeField]
    private Button BattleInfoReturnButton;


    [SerializeField]
    public Button[] SlotButton;

    [SerializeField]
    public Button[] SortButton;

    [SerializeField]
    private Button[] LevelButton;

    [SerializeField]
    private Button BattleStartButton;

    public ItemType[] types;




    void Start()
    {
        var IsBuy = true;

        types = new ItemType[7];

        types[0] = ItemType.Helmet;
        types[1] = ItemType.Armor;
        types[2] = ItemType.Boots;
        types[3] = ItemType.Weapon;
        types[4] = ItemType.Shield;
        types[5] = ItemType.Ring;
        types[6] = ItemType.Defoult;



        for (int a = 0; a < SlotButton.Length; a++) 
        {
            SlotButton[a].onClick.AddListener(() =>
            {
                InventoryItemOpen();
            });
        }

        for (int a = 0; a < SortButton.Length; a++)
        {
            SortButton[a].onClick.AddListener(() =>
            {
                InventoryItemOpen();

            });
        }

        for (int c = 0; c < LevelButton.Length; c++)
        {
            LevelButton[c].onClick.AddListener(() =>
            {
                BattleInfoPanel.gameObject.SetActive(true);
                BattleInfoReturnButton.gameObject.SetActive(true);
            });
        }

        BattleStartButton.onClick.AddListener(() =>
        {
            HubMenuPanel.gameObject.SetActive(false);
            BattleInfoPanel.gameObject.SetActive(false);
            BattleInfoReturnButton.gameObject.SetActive(false);
            MapClose();

            BattleOpen();
        });

        ItemInfoReturnButton.onClick.AddListener(() =>
        {
            ItemInfoPanel.gameObject.SetActive(false);
            ItemInfoReturnButton.gameObject.SetActive(false);
        });

        BattleInfoReturnButton.onClick.AddListener(() =>
        {
            BattleInfoPanel.gameObject.SetActive(false);
            BattleInfoReturnButton.gameObject.SetActive(false);
        });

        ItemButton.onClick.AddListener(() =>
        {
            ItemInfoPanel.gameObject.SetActive(false);
            AchievementPanel.gameObject.SetActive(false);
            InventoryItemOpen();

        });

        AllItemButton.onClick.AddListener(() =>
        {
            ItemInfoPanel.gameObject.SetActive(false);
            AchievementPanel.gameObject.SetActive(false);
            InventoryItemOpen();
        });

        AchievementsButton.onClick.AddListener(() =>
        {
            InventoryClose();
            InventoryHomePanel.gameObject.SetActive(false);

            AchievementPanel.gameObject.SetActive(true);
        });

        HomeButton.onClick.AddListener(() =>
        {   
            InventoryClose();
            MarketClose();
            MapClose();

            HomeOpen();  
        });

        MarketButton.onClick.AddListener(() =>
        {
            HomeClose();
            InventoryClose();
            MapClose();

            MarketOpen();
        });

        MapButton.onClick.AddListener(() =>
        {
            InventoryClose();
            MarketClose();
            HomeClose();

            MapOpen();
        });

       

        MarketCellBuyButton.onClick.AddListener(() =>
        {
            if (IsBuy)
            {
                IsBuy = !IsBuy;
                MarketBuyPanel.gameObject.SetActive(false);

                MarketCellPanel.gameObject.SetActive(true);
            }
            else
            {
                IsBuy = !IsBuy;
                MarketCellPanel.gameObject.SetActive(false);

                MarketBuyPanel.gameObject.SetActive(true);               

            }
        });


    }


    private void InventoryClose()
    {
        InventoryViewPanel.gameObject.SetActive(false);
        ItemInfoPanel.gameObject.SetActive(false);

    }

    


    private void HomeClose()
    {
        AchievementPanel.gameObject.SetActive(false);
        HeaderPlayerMenuPanel.gameObject.SetActive(false);
        InventoryHomePanel.gameObject.SetActive(false);
        HubHomeMenuPanel.gameObject.SetActive(false);
        FonsPanel.gameObject.SetActive(false);

    }


    private void HomeOpen()
    {
        HeaderPlayerMenuPanel.gameObject.SetActive(true);
        InventoryHomePanel.gameObject.SetActive(true);
        HubHomeMenuPanel.gameObject.SetActive(true);
        FonsPanel.gameObject.SetActive(true);
        
    }    

    private void MapClose()
    {
        ButtlePanel.gameObject.SetActive(false);

    }

    private void MarketClose()
    {
        HeaderPlayerMenuPanel.gameObject.SetActive(false);
        MarketViewPanel.gameObject.SetActive(false);
    }

    private void MapOpen()
    {
        ButtlePanel.gameObject.SetActive(true);

    }

    private void InventoryItemOpen()
    {
        InventoryHomePanel.gameObject.SetActive(false);

        InventoryViewPanel.gameObject.SetActive(true);
    }

    private void BattleOpen()
    {
        HeaderMoneyMenuPanel.gameObject.SetActive(false);
        ThreeRowViewPanel.gameObject.SetActive(true);
    }

    private void MarketOpen()
    {    
        HeaderPlayerMenuPanel.gameObject.SetActive(true);
        MarketViewPanel.gameObject.SetActive(true);
    }
}
