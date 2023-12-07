using Raylib_cs;
using System;
using System.Numerics;
using static Raylib_cs.Raylib;

public class Paddle
{
    public int ScreenWidth { get; }
    public int ScreenHeight { get; }

    public Vector2 paddlePosition;
    private Texture2D paddleTexture;

    private const float paddleSpeed = 5f;  // Value of speed
    private bool gameStarted = false;

    public Paddle(int screenWidth, int screenHeight)
    {
        Image image = LoadImage("resources/paddle.png"); // Folder is located in bin/Debug/net5.0/resources || Resources folder should be with the exe executable
        paddleTexture = LoadTextureFromImage(image); 
        UnloadImage(image);

        ScreenWidth = screenWidth;
        ScreenHeight = screenHeight;

        // Initialize the position for the drawn rectangle
        paddlePosition = new Vector2(screenWidth / 2 - 100, screenHeight - 35);
    }

    internal void StartGame()
    {
        // Set gameStarted to true when the game starts
        gameStarted = true;
    }

    internal void Update()
    {
        // Handle input and update paddle position
        if (IsKeyDown(KeyboardKey.KEY_LEFT) && paddlePosition.X > 0)
        {
            paddlePosition.X -= paddleSpeed;
            Console.WriteLine("Moving Left");
        }
        if (IsKeyDown(KeyboardKey.KEY_RIGHT) && paddlePosition.X < ScreenWidth - 200) // Adjusted based on drawn rectangle width
        {
            paddlePosition.X += paddleSpeed;
            Console.WriteLine("Moving Right");
        }
    }

    internal void Draw()
    {
        // Draw the rectangle at the updated position
        //DrawRectangle((int)paddlePosition.X, (int)paddlePosition.Y, 200, 20, Color.WHITE);

        // Draw texture at the updated position
        DrawTexture(paddleTexture, (int)paddlePosition.X, (int)paddlePosition.Y, Color.WHITE);
    }
}


