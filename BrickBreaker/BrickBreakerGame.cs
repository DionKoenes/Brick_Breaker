using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace BrickBreaker
{
    internal class BrickBreakerGame
    {
        private Paddle paddle;
        private Ball ball;

        public BrickBreakerGame(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            paddle = new Paddle(screenWidth, screenHeight);
            ball = new Ball(new Vector2(screenWidth / 2, screenHeight - 50));  // Adjust the starting position
        }

        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        internal void Update()
        { 
            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                ball.StartGame();
                paddle.StartGame();
            }

            ball.Update(paddle);
            paddle.Update();
        }

        internal void Draw()
        {
            paddle.Draw();
            ball.Draw();
        }
    }
}