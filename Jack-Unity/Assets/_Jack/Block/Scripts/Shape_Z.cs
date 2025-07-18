using UnityEngine;
using System.Collections.Generic;

public static class Shape_Z
{
    public static List<bool[,]> Generate()
    {
        var shapes = new List<bool[,]>();

        shapes.Add(new bool[,]
        {
            {true, false },
            {true, true },
            {false, true }
        });

        shapes.Add(new bool[,]
        {
            {false, true },
            {true, true },
            {true, false}
        });

        shapes.Add(new bool[,]
        {
            {true, true, false },
            {false, true, true }
        });

        shapes.Add(new bool[,]
        {
            {false, true, true },
            {true, true, false }
        });

        return shapes;
    }
}