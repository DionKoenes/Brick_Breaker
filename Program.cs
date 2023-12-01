using Raylib_cs;
using static Raylib_cs.Raylib;

namespace BrickBreaker
{
    public class Program
    {
        public static void Main()
        {
            const int screenWidth = 1280;
            const int screenHeight = 720;

            InitWindow(screenWidth, screenHeight, "Brick Breaker");

            BrickBreakerGame game = new(screenWidth, screenHeight);

            SetTargetFPS(60);

            while (!WindowShouldClose())
            {
                game.Update();

                BeginDrawing();

                ClearBackground(Color.BLACK);  // Clear the background

                game.Draw();

                EndDrawing();
            }

            //    UnloadTexture(paddleTexture); DE-INITIALIZATION

            CloseWindow();

        }
    }
}

