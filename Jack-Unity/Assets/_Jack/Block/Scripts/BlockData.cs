using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Block/BlockData")]
public class BlockData : ScriptableObject
{
    public Vector2Int size;
    public bool[,]    shape;
    public Sprite[]   blockSprites;

    public void SetShape(bool[,] newShape)
    {
        int width = newShape.GetLength(0);
        int height = newShape.GetLength(1);

        int minX = width, minY = height;
        int maxX = -1, maxY = -1;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (newShape[x, y])
                {
                    if (x < minX) minX = x;
                    if (y < minY) minY = y;
                    if (x > maxX) maxX = x;
                    if (y > maxY) maxY = y;
                }
            }
        }

        int newWidth = maxX - minX + 1;
        int newHeight = maxY - minY + 1;

        size = new Vector2Int(newWidth, newHeight);
        shape = new bool[newWidth, newHeight];

        for (int y = 0; y < newHeight; y++)
        {
            for (int x = 0; x < newWidth; x++)
            {
                shape[x, y] = newShape[minX + x, minY + y];
            }
        }
    }
}