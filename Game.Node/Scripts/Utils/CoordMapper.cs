using Godot;

public static class CoordMapper
{
    public static Vector2I ToVector2I(Point point) => new(point.X, point.Y);

    public static Point ToPoint(Vector2I vector2I) => new(vector2I.X, vector2I.Y);
}
