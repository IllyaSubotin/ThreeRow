using System.Drawing;
using UnityEngine;


public class ThreeRowModel: Model
{
    public int BoardSize;
    public int[,] map;
    private bool IsMapCreated;

    public ThreeRowModel(ThreeRowObject threeRowObject) 
    {
        IsMapCreated = threeRowObject.IsMapCreated;

        if (IsMapCreated)
        {
            map = threeRowObject.Map;
            BoardSize = map.Length; //тут щас неправильно, треба передивитись по довжині одного массива
        }
        else
        {
            BoardSize = threeRowObject.BoardSize;
            map = CreateMap(BoardSize);
        }
    }


    private int[,] CreateMap(int size)
    {
        var CopyMap = new int[size, size];

        CreateRandomBolls(CopyMap, size);
        
        for (; IsMarking(CopyMap) == true;)
        {
            CreateRandomBolls(CopyMap, size);
        }

            return CopyMap;
    }

    private void CreateRandomBolls(int[,] copyMap, int size)
    {
        for (int a = 0; a < size; a++)
        {
            for (int b = 0; b < size; b++)
            {
                copyMap[a, b] = Random.Range(1, 7);
            }
        }
    }

    private bool IsMarking(int[,] copyMap)
    {
        for (int a = 0; a < BoardSize; a++)
        {
            for (int b = 0; b < BoardSize; b++)
            {
                if (copyMap[a, b] == GetCellNum(a, b + 1, copyMap) && copyMap[a, b] == GetCellNum(a, b + 2, copyMap) || copyMap[a, b] == GetCellNum(a + 1, b, copyMap) && copyMap[a, b] == GetCellNum(a + 2, b, copyMap))
                {
                    return true;
                }
            }
        }


        return false;
    }

    private int GetCellNum(int x, int y, int[,] copyMap)
    {
        if (x >= 0 && y >= 0 && x < BoardSize && y < BoardSize)
        {
            return copyMap[x, y];
        }
        else
        {
            return 10;
        }
    }
}
