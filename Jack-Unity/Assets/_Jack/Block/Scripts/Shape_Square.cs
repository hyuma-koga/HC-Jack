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

        //L字パターン
        List<bool[,]> IShapes = new List<bool[,]>
        {
            new bool[,]
            {
                {true, false, false },
                {true, false, false },
                {true, true, true }
            },
            new bool[,]
            {
                { false, false, true },
                { false, false, true },
                { true, true, true }
            },
            new bool[,]
            {
                { true, true, true },
                { true, false, false },
                { true, false, false }
            },
            new bool[,]
            {
                { true, true, true },
                { false, false, true },
                { false, false, true }
            }
        };

        shapes.AddRange(IShapes);

        return shapes;
    }
}