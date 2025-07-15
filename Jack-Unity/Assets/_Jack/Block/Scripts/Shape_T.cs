using System.Collections.Generic;
using UnityEngine;

public static class Shape_T
{
    public static List<bool[,]> Generate()
    {
        var shapes = new List<bool[,]>();

        // 3x2 T Œ^
        shapes.Add(new bool[,]
        {
            { true, true, true },
            { false, true, false }
        });

        shapes.Add(new bool[,]
        {
            { false, true, false },
            { true, true, true }
        });

        // 2x3 T Œ^
        shapes.Add(new bool[,]
        {
            { false, true },
            { true, true },
            { false, true }
        });

        shapes.Add(new bool[,]
        {
            { true, false },
            { true, true },
            { true, false }
        });

        return shapes;
    }
}