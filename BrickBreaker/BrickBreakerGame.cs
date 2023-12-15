using System;
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace BrickBreaker
{
    internal class BrickBreakerGame
    {
        public int ScreenWidth { get; }
        public int ScreenHeight { get; }

        private Paddle paddle;
        private Ball ball;
        private BrickManager brickManager;

        public bool gameStarted = false;

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

        internal void Update()
        { 
            if (!gameStarted && IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                ball.StartGame();
                Console.WriteLine("Starting Game");
                gameStarted = true;
            }


            ball.Update(paddle); // Mention paddle for movement before start of the game
            paddle.Update();

            CheckBallCollisions();

        }

        private void CheckBallCollisions()
        {
            // Check for collision with the paddle
            if (ball.Position.Y + 10 >= paddle.paddlePosition.Y && ball.Position.Y - 10 <= paddle.paddlePosition.Y + 20 &&
                ball.Position.X >= paddle.paddlePosition.X && ball.Position.X <= paddle.paddlePosition.X + 200)
            {
                PlaySound(paddleBoink);
                ball.BounceOffPaddle(paddle);
            }

            // Check for collision with bricks
            foreach (Brick brick in brickManager.Bricks)
            {
                if (brick.IsActive && 
                    ball.Position.Y + ball.Radius >= brick.BrickPosition.Y && //Check if bottom side of ball is below or same as top edge of brick.
                    ball.Position.Y - ball.Radius <= brick.BrickPosition.Y + brick.Height && //Checks if top side of ball is above or same level as bottom of brick.
                    ball.Position.X + ball.Radius >= brick.BrickPosition.X && //Checks if right side of ball is right of or same level as left of brick.
                    ball.Position.X - ball.Radius <= brick.BrickPosition.X + brick.Width) //Checks if left side of ball is left of or same as right edge of brick.
                {
                    PlaySound(brickBoink);
                    brick.IsActive = false;  // Deactivate the brick
                    ball.BounceOffBrick(brick);
                }
            }
        }

        internal void Draw()
        {
            paddle.Draw();
            ball.Draw();
            brickManager.Draw();
        }

        internal void Unload()
        {
            UnloadSound(paddleBoink);
            UnloadSound(brickBoink);
        }
    }
}