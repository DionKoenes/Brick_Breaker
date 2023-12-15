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
        private Background background;

        public bool gameStarted = false;

        Sound paddleBoink = LoadSound("resources/audio/paddleboink.wav");
        Sound brickBoink = LoadSound("resources/audio/brickboink.wav");

        public BrickBreakerGame(int screenWidth, int screenHeight)
        {
            paddle = new Paddle();
            ball = new Ball(new Vector2(GetScreenWidth() / 2, GetScreenHeight() - 50));  // Adjust the starting position
            brickManager = new BrickManager();
            background = new Background();
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
            if (ball.Position.Y + ball.Radius >= paddle.paddlePosition.Y && 
                ball.Position.Y - ball.Radius <= paddle.paddlePosition.Y + paddle.paddleHeight &&
                ball.Position.X >= paddle.paddlePosition.X && 
                ball.Position.X <= paddle.paddlePosition.X + paddle.paddleWidth)
            {
                PlaySound(paddleBoink);
                ball.BounceOffPaddle(paddle);
            }

            // Check for collision with bricks
            foreach (Brick brick in brickManager.Bricks)
            {
                if (brick.IsEnabled && 
                    ball.Position.Y + ball.Radius >= brick.BrickPosition.Y && //Check if bottom side of ball is below or same as top edge of brick.
                    ball.Position.Y - ball.Radius <= brick.BrickPosition.Y + brick.Height && //Checks if top side of ball is above or same level as bottom of brick.
                    ball.Position.X + ball.Radius >= brick.BrickPosition.X && //Checks if right side of ball is right of or same level as left of brick.
                    ball.Position.X - ball.Radius <= brick.BrickPosition.X + brick.Width) //Checks if left side of ball is left of or same as right edge of brick.
                {
                    PlaySound(brickBoink);
                    brick.IsEnabled = false;  // Deactivate the brick
                    ball.BounceOffBrick(brick);
                }
            }
        }

        internal void Draw()
        {
            background.Draw();
            paddle.Draw();
            ball.Draw();
            brickManager.Draw();
        }

        internal void Unload()
        {
            UnloadSound(paddleBoink);
            UnloadSound(brickBoink);

            SpriteNode.UnloadAllTextures();
        }
    }
}