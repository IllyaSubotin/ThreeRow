using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class ThreeRowControler : Controller
{
    private CellComponent[,] cellComponent;
    private int[,] map;
    private bool[,] mark;

    private int Size = 5;

    private int FromX, FromY;
    private bool IsSelected;
    private float MaxHealthPlayer;
    private float CurHealthPlayer;
    private float PlayerDamage;
    private float MaxHealthEnemy;
    private float CurHealthEnemy;
    private float EnemyDamage;
    private int EnemyWaitMax;
    private int EnemyWaitCur;

    private ThreeRowView _view;
    private ThreeRowModel _model;


    private Enemy enemyScripteblObject;
    private Player playerScripteblObject;

    private Image EnemyImage;

    public GameObject componentPrefab;
    public GameObject panel;
    public Image BattleFonsPanel;
    public Scrollbar playerScrollbar;
    public Scrollbar enemyScrollbar;
    public Sprite[] sprites;



    public ThreeRowControler(ThreeRowView view, ThreeRowModel model)
    {
        _model = model;
        _view = view;

        map = _model.map;
        Size = _model.BoardSize;

        componentPrefab = _view.componentPrefab;
        panel = _view.panel;
        sprites = _view.sprites;

        BattleFonsPanel = _view.BattleFonsPanel;

        enemyScrollbar = _view.EnemyHealthBar;
        playerScrollbar = _view.PlayerHealthBar;



        mark = new bool[Size, Size];
        IsSelected = false;
    }
    public override void Init()
    {
        EnemyAndPlayerUpdate(0);
        InitCellComponent();
        SetClickOnButtons();
        SetCurrentSprites();


    }

    public void EnemyAndPlayerUpdate(int LevelCaunt)
    {
        var rand = UnityEngine.Random.Range(0, 3);

        playerScripteblObject = _view.playerScripteblObject;
        enemyScripteblObject = _view.enemyScripteblObjects[rand];//

        MaxHealthPlayer = playerScripteblObject.MaxHealth;
        CurHealthPlayer = playerScripteblObject.CurrentHealth;
        PlayerDamage = playerScripteblObject.Damage;

        CurHealthEnemy = MaxHealthEnemy = enemyScripteblObject.MaxHealth;
        EnemyDamage = enemyScripteblObject.Damage;

        EnemyImage = _view.EnemyImage;

        EnemyImage.sprite = enemyScripteblObject.Image;
        enemyScrollbar.size = CurHealthEnemy / MaxHealthEnemy;

        EnemyWaitCur = EnemyWaitMax = enemyScripteblObject.Wait;


    }

    private void InitCellComponent()
    {
        cellComponent = new CellComponent[Size, Size];

        for (int a = 0; a < cellComponent.Length; a++)
        {
            var cell = GameObject.Instantiate(componentPrefab);
            cell.transform.SetParent(panel.transform, false);
            cellComponent[a % Size, a / Size] = cell.GetComponent<CellComponent>();
            cellComponent[a % Size, a / Size].Index = a;

        }
    }

    private void SetCurrentSprites()
    {
        for (int a = 0; a < cellComponent.Length; a++)
        {
            cellComponent[a % Size, a / Size].Image.sprite = sprites[map[a % Size, a / Size]];
        }

        EnemyImage.sprite = enemyScripteblObject.Image;
        enemyScrollbar.size = CurHealthEnemy / MaxHealthEnemy;
    }

    private void SetClickOnButtons()
    {
        for (int a = 0; a < cellComponent.Length; a++)
        {
            var temp = a;
            cellComponent[a % Size, a / Size].Button.onClick.AddListener(() =>
            {
                Click(temp);
            });
        }
        _view.button.onClick.AddListener(() =>
        {
            CreateNewMap();
        });
    }




    private void Click(int index)
    {
        int x = index % Size;
        int y = index / Size;



        if (IsSelected)
        {
            MoveBoll(x, y);
            IsSelected = false;

        }
        else
        {
            TakeBoll(x, y);
        }

    }

    private void CreateNewMap()
    {
        var hp = CurHealthEnemy;
        EnemyAttack();

        for (int a = 0; a < Size; a++)
        {
            for (int b = 0; b < Size; b++)
            {
                SetImage(a, b, UnityEngine.Random.Range(1, 7));
                mark[a, b] = false;
            }
        }
        if (IsMarking(map))
        {
            Marking();
            FallingBalls();
        }

        CurHealthEnemy = hp;
        enemyScrollbar.size = CurHealthEnemy / MaxHealthEnemy;
        IsEnd();
    }



    private void TakeBoll(int x, int y)
    {
        FromX = x;
        FromY = y;
        IsSelected = true;

    }



    private void MoveBoll(int x, int y)
    {
        if (IsPosibleMove(x, y))
        {
            var copyMap = map;
            var swap1 = copyMap[x, y];
            var swap2 = copyMap[FromX, FromY];

            copyMap[x, y] = swap2;
            copyMap[FromX, FromY] = swap1;

            if (IsMarking(copyMap))
            {
                EnemyAttack();

                SetImage(x, y, swap2);
                SetImage(FromX, FromY, swap1);

                Marking();
                FallingBalls();
                IsEnd();


            }
            else
            {

                SetImage(x, y, swap1);
                SetImage(FromX, FromY, swap2);
            }
        }
        else
        {
            TakeBoll(x, y);
        }


    }

    private void IsEnd()
    {
        if (CurHealthEnemy <= 0)
        {
            _view.manager.ThreeRowViewPanel.gameObject.SetActive(false);

            _view.manager.HeaderMoneyMenuPanel.gameObject.SetActive(true);
            _view.manager.HubMenuPanel.gameObject.SetActive(true);
            _view.manager.ButtlePanel.gameObject.SetActive(true);
        }

        if (CurHealthPlayer <= 0)
        {
            _view.manager.ThreeRowViewPanel.gameObject.SetActive(false);

            _view.manager.HeaderMoneyMenuPanel.gameObject.SetActive(true);
            _view.manager.HubMenuPanel.gameObject.SetActive(true);
            _view.manager.ButtlePanel.gameObject.SetActive(true);
        }
    }

    private void PlayerAttack()
    {
        CurHealthEnemy -= PlayerDamage;
        enemyScrollbar.size = CurHealthEnemy / MaxHealthEnemy;
    }

    private void EnemyAttack()
    {
        if (EnemyWaitCur == EnemyWaitMax)
        {
            CurHealthPlayer -= EnemyDamage;
            playerScrollbar.size = CurHealthPlayer / MaxHealthPlayer;
            EnemyWaitCur = 0;
        }
        else
        {
            EnemyWaitCur++;
        }
    }

    private bool IsPosibleMove(int x, int y)
    {

        if (x == FromX && y == FromY + 1 || x == FromX && y == FromY - 1)
        {
            return true;
        }
        if (x == FromX + 1 && y == FromY || x == FromX - 1 && y == FromY)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsMarking(int[,] copyMap)
    {
        for (int a = 0; a < Size; a++)
        {
            for (int b = 0; b < Size; b++)
            {
                if (copyMap[a, b] == GetCellNum(a, b + 1, copyMap) && copyMap[a, b] == GetCellNum(a, b + 2, copyMap) || copyMap[a, b] == GetCellNum(a + 1, b, copyMap) && copyMap[a, b] == GetCellNum(a + 2, b, copyMap))
                {
                    return true;
                }
            }
        }


        return false;
    }

    private void Marking()
    {

        for (int a = 0; a < Size; a++)
        {
            for (int b = 0; b < Size; b++)
            {
                if (!mark[a, b])
                {
                    if (map[a, b] == GetCellNum(a, b + 1, map) && map[a, b] == GetCellNum(a, b + 2, map) && map[a, b] != 0)
                    {
                        for (var tempX = 0; map[a, b] == GetCellNum(a, b + tempX, map); tempX++)
                        {
                            mark[a, b + tempX] = true;

                            if (map[a, b + tempX] == GetCellNum(a + 1, b + tempX, map) && map[a, b + tempX] == GetCellNum(a + 2, b + tempX, map) && map[a, b + tempX] != 0)
                            {
                                for (var tempY = 0; map[a, b + tempX] == GetCellNum(a + tempY, b + tempX, map); tempY++)
                                {
                                    mark[a + tempY, b + tempX] = true;
                                }
                            }
                        }
                        return;
                    }


                    if (map[a, b] == GetCellNum(a + 1, b, map) && map[a, b] == GetCellNum(a + 2, b, map) && map[a, b] != 0)
                    {
                        for (var tempY = 0; map[a, b] == GetCellNum(a + tempY, b, map); tempY++)
                        {
                            mark[a + tempY, b] = true;

                            if (map[a + tempY, b] == GetCellNum(a + tempY, b + 1, map) && map[a + tempY, b] == GetCellNum(a + tempY, b + 2, map) && map[a + tempY, b] != 0)
                            {
                                for (var tempX = 0; map[a + tempY, b] == GetCellNum(a + tempY, b + tempX, map); tempX++)
                                {
                                    mark[a + tempY, b + tempX] = true;
                                }
                            }

                        }
                        return;
                    }
                }
            }
        }
    }





    private void FallingBalls()
    {
        for (int a = 0; a < Size; a++)
        {
            for (int b = 0; b < Size; b++)
            {
                if (mark[a, b] == true)
                {

                    for (var temp = 1; GetCellNum(a, b - temp, map) != 10; temp++)
                    {
                        SetImage(a, b - temp + 1, map[a, b - temp]);
                    }

                    mark[a, b] = false;
                    SetImage(a, 0, UnityEngine.Random.Range(1, 7));

                }
            }
        }
        PlayerAttack();

        for (; IsMarking(map) == true;)
        {
            Marking();
            FallingBalls();
        }
    }


    private int GetCellNum(int x, int y, int[,] copyMap)
    {
        if (x >= 0 && y >= 0 && x < Size && y < Size)
        {
            return copyMap[x, y];
        }
        else
        {
            return 10;
        }
    }


    private void SetImage(int a, int b, int Image)
    {
        map[a, b] = Image;
        cellComponent[a, b].Image.sprite = sprites[Image];
    }



    public void FonsUpdate()
    {
        var rand = UnityEngine.Random.Range(0, 2);
        BattleFonsPanel.sprite = _view.BattleFonsImage[rand];
    }


}
