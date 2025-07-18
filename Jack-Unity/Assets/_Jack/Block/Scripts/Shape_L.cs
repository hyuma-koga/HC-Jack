using System.Collections.Generic;
using UnityEngine;

public static class Shape_L
{
    public static List<bool[,]> Generate()
    {
        var shapes = new List<bool[,]>();

        shapes.Add(new bool[,]
        {
            { true, true, true },
            { false, false, true }
        });

        shapes.Add(new bool[,]
        {
            { true, true, true },
            { true, false, false }
        });

        shapes.Add(new bool[,]
       {
            { true, false, false },
            { true, true, true }
       });

        shapes.Add(new bool[,]
       {
            { false, false, true },
            { true, true, true }
       });


        shapes.Add(new bool[,]
        {
            { true, true },
            { false, true },
            { false, true }
        });

        shapes.Add(new bool[,]
        {
            { false, true },
            { false, true },
            { true, true }
        });

        shapes.Add(new bool[,]
        {
            { true, true },
            { true, false },
            { true, false }
        });

        shapes.Add(new bool[,]
        {
            { true, false },
            { true, false },
            { true, true }
        });

        return shapes;
    }
}