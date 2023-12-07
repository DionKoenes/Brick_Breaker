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
        private BrickManager brickManager;

        Sound paddleBoink = LoadSound("resources/audio/paddleboink.wav");
        Sound brickBoink = LoadSound("resources/audio/brickboink.wav");

        public BrickBreakerGame(int screenWidth, int screenHeight)
        {
            ScreenWidth = screenWidth;
            ScreenHeight = screenHeight;

            paddle = new Paddle(screenWidth, screenHeight);
            ball = new Ball(new Vector2(screenWidth / 2, screenHeight - 50));  // Adjust the starting position
            brickManager = new BrickManager();
        }

        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        internal void Update()
        { 
            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                ball.StartGame();
                paddle.StartGame();
                Console.WriteLine("Starting Game");
            }

            ball.Update(); // Mention paddle & bricks list for collision detection
            paddle.Update();

            CheckBallCollisions();
        }

        private void CheckBallCollisions()
        {
            // Check for collision with the paddle
            if (ball.Position.Y + 10 >= paddle.paddlePosition.Y && ball.Position.Y - 10 <= paddle.paddlePosition.Y + 20 &&
                ball.Position.X >= paddle.paddlePosition.X && ball.Position.X <= paddle.paddlePosition.X + 200)
            {
                ball.BounceOffPaddle(paddle);
                PlaySound(paddleBoink);
            }

            // Check for collision with bricks
            foreach (Brick brick in brickManager.Bricks)
            {
                if (brick.IsActive && ball.Position.Y + 10 >= brick.brickPosition.Y && ball.Position.Y - 10 <= brick.brickPosition.Y + 40 &&
                ball.Position.X >= brick.brickPosition.X && ball.Position.X <= brick.brickPosition.X + 100)
                {
                    brick.IsActive = false;  // Deactivate the brick
                    ball.BounceOffBrick(brick);
                    PlaySound(brickBoink);
                }
            }
        }

        internal void Draw()
        {
            paddle.Draw();
            ball.Draw();
            brickManager.Draw();
        }
    }
}