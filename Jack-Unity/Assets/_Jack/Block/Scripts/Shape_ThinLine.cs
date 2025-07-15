using UnityEngine;
using System.Collections.Generic;

public static class Shape_ThinLine
{
    public static List<bool[,]> Generate()
    {
        var shapes = new List<bool[,]>();

        //â°ÇPÉ}ÉX & èc 2Å`5É}ÉX
        for (int h = 2; h <= 5; h++)
        {
            bool[,] shape = new bool[1, h];

            for (int y = 0; y < h; y++)
            {
                shape[0, y] = true;
            }

            shapes.Add(shape);
        }

        //â°2Å`5É}ÉX & èc1É}ÉX
        for ( int w = 2; w <= 5; w++)
        {
            bool[,] shape = new bool[w, 1];

            for (int x = 0; x < w; x++)
            {
                shape[x, 0] = true;
            }
                
            shapes.Add(shape);
        }

        return shapes;
    }
}
