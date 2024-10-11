using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private Button[] LevelButton;

    [SerializeField]
    private GameObject EnemyInfoPanel;

    [SerializeField]
    private GameObject DropInfoPanel;

    [SerializeField]
    private LevelInfoObject[] LevelInfoObject;

    [SerializeField]
    private GameObject ImagePrefab;

    private int LevelCount = 0;

    void Start()
    {


        for (int c = 0; c < LevelButton.Length; c++)
        {
            LevelButton[c].onClick.AddListener(() =>
            {
                if(LevelCount == 0)
                {
                    LevelCount = 1;
                    for (int c = 0; c < LevelInfoObject[0].EnemySprite.Length; c++) 
                    {
                        var cell = GameObject.Instantiate(ImagePrefab);
                        cell.transform.SetParent(EnemyInfoPanel.transform, false);
                        var Image = cell.GetComponent<Image>();
                        Image.sprite = LevelInfoObject[0].EnemySprite[c];
                    }

                    for (int c = 0; c < LevelInfoObject[0].DropSprite.Length; c++)
                    {
                        var cell = GameObject.Instantiate(ImagePrefab);
                        cell.transform.SetParent(DropInfoPanel.transform, false);
                        var Image = cell.GetComponent<Image>();
                        Image.sprite = LevelInfoObject[0].DropSprite[c];
                    }
                }

            });
        }
    }
}
