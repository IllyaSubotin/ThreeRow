using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class MarketController : Controller
{
    private ItemCellBuyComponent[] buyItemComponent;
    private ItemCellBuyComponent[] cellItemComponent;
    private int Size;


    private MarketView _view;
    private MarketModel _model;

    public GameObject CellPrefab;
    public GameObject CellPanel;
    public GameObject BuyPrefab;
    public GameObject BuyPanel;


    public MarketController(MarketView view, MarketModel model)
    {
        _view = view;
        _model = model;

        CellPanel = _view.CellPanel;
        CellPrefab = _view.CellPrefab;
        BuyPanel = _view.BuyPanel;
        BuyPrefab = _view.BuyPrefab;

        Size = _model.Size;

    }

    public override void Init()
    {
        CreateInventorySlot();
        CreateInventorySlot2();
        SetClickOnButtons();

    }

    private void CreateInventorySlot2()
    {
        buyItemComponent = new ItemCellBuyComponent[Size];

        for (int b = 0; b < buyItemComponent.Length; b++)
        {
            var temp = b;
            var cell = GameObject.Instantiate(BuyPrefab);
            cell.transform.SetParent(BuyPanel.transform, false);
            buyItemComponent[temp] = cell.GetComponent<ItemCellBuyComponent>();
            buyItemComponent[temp].Index = temp;
        }
    }

    private void CreateInventorySlot()
    {

        cellItemComponent = new ItemCellBuyComponent[Size];

        for (int a = 0; a < cellItemComponent.Length; a++)
        {
            var temp = a;
            var cell = GameObject.Instantiate(CellPrefab);
            cell.transform.SetParent(CellPanel.transform, false);
            cellItemComponent[temp] = cell.GetComponent<ItemCellBuyComponent>();
            cellItemComponent[temp].Index = temp;
        }

      
    }

    private void SetClickOnButtons()
    {
        for (int a = 0; a < cellItemComponent.Length; a++)
        {
            var temp = a;
            cellItemComponent[temp].Button.onClick.AddListener(() =>
            {
                Click(temp);
            });
        }

        for (int a = 0; a < buyItemComponent.Length; a++)
        {
            var temp = a;
            buyItemComponent[temp].Button.onClick.AddListener(() =>
            {
                Click(temp);
            });
        }
    }

    private void Click(int temp)
    {

    }
}
