using UnityEngine;

public class BoardPlacer : MonoBehaviour
{
    [SerializeField] private int boardSize = 7;

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
}