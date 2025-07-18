using UnityEngine;
using System.Collections.Generic;
using static UnityEngine.Rendering.ProbeAdjustmentVolume;

public static class Shape_Square
{
    public static List<bool[,]> Generate()
    {
        var shapes = new List<bool[,]>();

        //3マス×3マス
        bool[,] full = new bool[3, 3];
        
        for (int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                full[x, y] = true;
            }
        }

        shapes.Add(full);
        return shapes;
    }
}