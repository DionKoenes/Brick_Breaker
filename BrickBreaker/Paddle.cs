using Raylib_cs;
using System;
using System.Numerics;
using static Raylib_cs.Raylib;

public class Paddle : SpriteNode
{
    public Vector2 paddlePosition;

    public int paddleWidth { get; }
    public int paddleHeight { get; }

    private const float paddleSpeed = 5f;  // Value of speed

    public Paddle()
        : base("resources/paddle.png")
    {
        paddleWidth = 200;
        paddleHeight = 20;

        // Initialize the position for the paddle
        paddlePosition = new Vector2(GetScreenWidth() / 2 - 100, GetScreenHeight() - 35);
    }

    internal void Update()
    {
        // Handle input and update paddle position
        if (IsKeyDown(KeyboardKey.KEY_LEFT) && paddlePosition.X > 0)
        {
            paddlePosition.X -= paddleSpeed;
            Console.WriteLine("Moving Left");
        }
        if (IsKeyDown(KeyboardKey.KEY_RIGHT) && paddlePosition.X < GetScreenWidth() - paddleWidth) // Adjusted based on drawn rectangle width
        {
            paddlePosition.X += paddleSpeed;
            Console.WriteLine("Moving Right");
        }
    }

    internal void Draw()
    {
        Draw(paddlePosition, Color.WHITE);
    }
}


