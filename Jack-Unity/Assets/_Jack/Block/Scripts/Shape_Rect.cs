using UnityEngine;
using System.Collections.Generic;

public static class Shape_Rect
{
    public static List<bool[,]> Generate()
    {
        var shapes = new List<bool[,]>();

        //��2�}�X & �c1�`3�}�X
        for (int h = 1; h < 3; h++)
        {
            bool[,] shape = new bool[2, h];

            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < h; y++)
                {
                    shape[x, y] = true;
                }
            } 
            shapes.Add(shape);
        }

        //��3�}�X & �c1�`3�}�X
        for (int h = 1; h <= 3; h++)
        {
            bool[,] shape = new bool[3, h];

            for(int x = 0; x < 3; x++)
            {
                for(int y = 0; y < h; y++)
                {
                    shape[x, y] = true;
                }
            }

            shapes.Add(shape);
        }

        return shapes;
    }
}
