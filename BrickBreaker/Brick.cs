using Raylib_cs;
using System.Numerics;
using System;

public class Brick : SpriteNode
{
    public enum BrickType
    {
        Normal,
        Special,
    }

    public BrickType Type { get; private set; }
    public Vector2 BrickPosition { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool IsEnabled { get; set; }

    public Brick(Vector2 position, int width, int height, BrickType type)
        : base(GetTexturePath(type))
    {
        BrickPosition = position;
        Width = width;
        Height = height;
        Type = type;
        IsEnabled = true; 
    }

    private static string GetTexturePath(BrickType type)
    {
        switch (type)
        {
            case BrickType.Normal:
                return "resources/brick_normal.png";
            case BrickType.Special:
                return "resources/brick_special.png";
            default:
                throw new ArgumentOutOfRangeException(nameof(type), type, null);
        }
    }

    internal void Draw()
    {
        if (IsEnabled)
        {
            Draw(BrickPosition, Color.WHITE);
        }
    }
}

