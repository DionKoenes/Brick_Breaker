using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

public class Brick
{
    public Vector2 brickPosition { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public bool IsActive { get; set; }

    public Brick(Vector2 position, int width, int height)
    {
        brickPosition = position;
        Width = width;
        Height = height;
        IsActive = true; 
    }

    public void Draw()
    {
        if (IsActive)
        {
            DrawRectangle((int)brickPosition.X, (int)brickPosition.Y, Width, Height, Color.WHITE);
        }
    }
}
