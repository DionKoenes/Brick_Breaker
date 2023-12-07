using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using System;

public class Brick
{
    public Vector2 BrickPosition { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public bool IsActive { get; set; }

    public Brick(Vector2 position, int width, int height)
    {
        BrickPosition = position;
        Width = width;
        Height = height;
        IsActive = true; 
    }

    public void Draw()
    {
        if (IsActive)
        {
            DrawRectangle((int)BrickPosition.X, (int)BrickPosition.Y, Width, Height, Color.WHITE);
        }
    }
}

