using Raylib_cs;
using static Raylib_cs.Raylib;
using static Raylib_cs.ConfigFlags;

namespace BrickBreaker
{
    public class Program
    {
        public static void Main()
        {
            const int screenWidth = 1280;
            const int screenHeight = 720;

            InitWindow(screenWidth, screenHeight, "Brick Breaker");

            InitAudioDevice();

            BrickBreakerGame game = new(screenWidth, screenHeight);

            SetTargetFPS(60);

            while (!WindowShouldClose())
            {
                game.Update();

                BeginDrawing();

                ClearBackground(Color.BLACK);

                game.Draw();

                EndDrawing();
            }

            // DE-INITIALIZATION
            CloseAudioDevice();
            game.Unload();
            CloseWindow();

        }
    }
}

