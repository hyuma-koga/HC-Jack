using System.Collections.Generic;

public static class ShapeGenerator
{
    public static List<bool[,]> GenerateAllShapes()
    {
        var shapes = new List<bool[,]>();
        shapes.AddRange(Shape_ThinLine.Generate());
        shapes.AddRange(Shape_Rect.Generate());
        shapes.AddRange(Shape_Square.Generate());
        return shapes;
    }
}