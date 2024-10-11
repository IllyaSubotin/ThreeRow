using UnityEngine;
using UnityEngine.UI;


public class ThreeRowView : View
{
    public GameObject componentPrefab;
    public GameObject panel;
    public Button button;
    public GridLayoutGroup layoutGroup;
    public Sprite[] sprites;


    public Player playerScripteblObject;
    public Scrollbar PlayerHealthBar;


    public Image EnemyImage;
    public Enemy[] enemyScripteblObjects;
    public Scrollbar EnemyHealthBar;

    public UiManager manager;

    public Image BattleFonsPanel;
    public Sprite[] BattleFonsImage;

}
