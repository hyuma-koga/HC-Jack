using UnityEngine;

[CreateAssetMenu(fileName = "BlockData", menuName = "Block/BlockData")]
public class BlockData : ScriptableObject
{
    public Vector2Int size;
    public bool[,]    shape;  　　　　 //ブロック形状を表す２次元配列
    public Sprite[]   blockSprites;

    public void SetShape(bool[,] newShape)
    {
        int width = newShape.GetLength(0);
        int height = newShape.GetLength(1);

        size = new Vector2Int(width, height);
        shape = new bool[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                shape[x, y] = newShape[x, y];
            }
        }
    }
}