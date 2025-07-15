using System.Collections.Generic;
using UnityEngine;

public class BoardPlacer : MonoBehaviour
{
    [SerializeField] private int boardSize = 8;

    private bool[,]              occupied;

    private void Awake()
    {
        occupied = new bool[boardSize, boardSize];
    }

    public bool CanPlaceBlock(bool[,] shape, int startX, int startY)
    {
        if (occupied == null)
        {
            return false;
        }

        int shapeWidth = shape.GetLength(0);
        int shapeHeight = shape.GetLength(1);

        // 範囲外チェック
        if (startX < 0 || startY < 0 || startX + shapeWidth > boardSize || startY + shapeHeight > boardSize) 
        {
            return false;
        } 

        // 他のブロックと重ならないか確認
        for (int y = 0; y < shapeHeight; y++)
        {
            for (int x = 0; x < shapeWidth; x++)
            {
                if (shape[x, y] && occupied[startX + x, startY + y])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public void PlaceBlock(bool[,] shape, int startX, int startY, GameObject blockObj)
    {
        int shapeWidth = shape.GetLength(0);
        int shapeHeight = shape.GetLength(1);

        // occupied を更新
        for (int y = 0; y < shapeHeight; y++)
        {
            for (int x = 0; x < shapeWidth; x++)
            {
                if (shape[x, y])
                {
                    occupied[startX + x, startY + y] = true;
                }
            }
        }
    }

    public List<int> GetFullRows()
    {
        var fullRows = new List<int>();

        for (int y = 0; y < boardSize; y++)
        {
            bool isFull = true;

            for (int x = 0; x < boardSize; x++)
            {
                if (!occupied[x, y])
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                fullRows.Add(y);
            }
        }

        return fullRows;
    }

    public List<int> GetFullColumns()
    {
        var fullCols = new List<int>();

        for (int x = 0; x < boardSize; x++)
        {
            bool isFull = true;

            for (int y = 0; y < boardSize; y++)
            {
                if (!occupied[x, y])
                {
                    isFull = false;
                    break;
                }
            }

            if (isFull)
            {
                fullCols.Add(x);
            }
        }

        return fullCols;
    }

    public void ClearRows(List<int> rows)
    {
        foreach (var y in rows)
        {
            for (int x = 0; x < boardSize; x++)
            {
                occupied[x, y] = false;
            }
        }
    }

    public void ClearColumns(List<int> columns)
    {
        foreach (var x in columns)
        {
            for (int y = 0; y < boardSize; y++)
            {
                occupied[x, y] = false;
            }
        }
    }

    public void SetOccupied(int x, int y, bool value)
    {
        if (occupied == null)
        {
            return;
        }

        if (x < 0 || x >= boardSize || y < 0 || y >= boardSize)
        {
            return;
        }

        occupied[x, y] = value;
    }
}