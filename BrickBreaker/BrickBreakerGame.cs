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
        private ScoreManager scoreManager;

        private int lives = 3;
        private bool gameOver = false;
        private bool gameStarted = false;

        private GameState gameState = GameState.Home;

        Sound paddleBoink = LoadSound("resources/audio/paddleboink.wav");
        Sound brickBoink = LoadSound("resources/audio/brickboink.wav");

        public BrickBreakerGame(int screenWidth, int screenHeight)
        {
            paddle = new Paddle();
            ball = new Ball(new Vector2(GetScreenWidth() / 2, GetScreenHeight() - 50));
            brickManager = new BrickManager();
            background = new Background();
            scoreManager = new ScoreManager();
        }

        internal void Update()
        {
            switch (gameState)
            {
                case GameState.Home:
                    UpdateHome();
                    break;
                case GameState.Playing:
                    UpdatePlaying();
                    break;
                case GameState.GameOver:
                    UpdateGameOver();
                    break;
            }
        }

        private void UpdateHome()
        {
            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                gameState = GameState.Playing;
            }
        }

        private void UpdatePlaying()
        {
            if (!gameStarted && gameOver == false && IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                ball.StartGame();
                Console.WriteLine("Starting Game");
                gameStarted = true;
            }

            ball.Update(paddle);
            paddle.Update();
            CheckBallCollisions();

            if (IsKeyPressed(KeyboardKey.KEY_Q))
            {
                gameState = GameState.Home;
            }
        }

        private void UpdateGameOver()
        {
            if (IsKeyPressed(KeyboardKey.KEY_R))
            {
                Restarted();
                gameState = GameState.Playing;
            }
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
                    ball.Position.Y + ball.Radius >= brick.BrickPosition.Y &&
                    ball.Position.Y - ball.Radius <= brick.BrickPosition.Y + brick.Height &&
                    ball.Position.X + ball.Radius >= brick.BrickPosition.X &&
                    ball.Position.X - ball.Radius <= brick.BrickPosition.X + brick.Width)
                {
                    PlaySound(brickBoink);
                    brick.IsEnabled = false;
                    ball.BounceOffBrick(brick);

                    // Increase the score based on the type of brick
                    if (brick.Type == Brick.BrickType.Normal)
                    {
                        scoreManager.IncreaseScore(1);
                    }
                    else if (brick.Type == Brick.BrickType.Special)
                    {
                        scoreManager.IncreaseScore(2);
                    }
                }
            }

            // Check if ball is below the screen
            if (ball.Position.Y > GetScreenHeight())
            {
                lives--;
                ball.LostLife();
                gameStarted = false;

                // Check if game over
                if (lives == 0)
                {
                    gameOver = true;
                }
            }
        }

        private void Restarted()
        {
            lives = 3;
            gameOver = false;
            brickManager = new BrickManager();
            scoreManager.ResetScore();
        }

        internal void Draw()
        {
            switch (gameState)
            {
                case GameState.Home:
                    DrawText("Press SPACE to start!", GetScreenWidth() / 2 - 160, GetScreenHeight() / 2 - 75, 30, Color.WHITE);
                    DrawText("Use SPACE to launch the ball and press A & D keys to move the paddle!", GetScreenWidth() / 2 - 550, GetScreenHeight() / 2 - 25, 30, Color.GOLD);
                    DrawText("Press Q for Title Screen!", GetScreenWidth() / 2 - 180, GetScreenHeight() / 2 + 25, 30, Color.RED);
                    DrawText("Press ESQ to quit application", GetScreenWidth() / 2 - 200, GetScreenHeight() / 2 + 75, 30, Color.LIME);
                    break;
                case GameState.Playing:
                    DrawPlaying();
                    break;
                case GameState.GameOver:
                    DrawGameOver();
                    break;
            }
        }

        private void DrawPlaying()
        {
            background.Draw();
            paddle.Draw();
            ball.Draw();
            brickManager.Draw();

            DrawText($"Lives: {lives}", GetScreenWidth() - 120, 10, 30, Color.RED);
            DrawText($"Score: {scoreManager.Score}", 10, 10, 30, Color.GOLD);

            if (gameOver)
            {
                DrawGameOver();
                gameState = GameState.GameOver;
            }
        }

        private void DrawGameOver()
        {
            DrawText("GAME OVER", GetScreenWidth() / 2 - 150, GetScreenHeight() / 2, 50, Color.RED);
            DrawText("Press R to restart!", GetScreenWidth() / 2 - 150, GetScreenHeight() / 2 + 50, 30, Color.WHITE);
        }

        internal void Unload()
        {
            UnloadSound(paddleBoink);
            UnloadSound(brickBoink);
            SpriteNode.UnloadAllTextures();
        }
    }

    internal enum GameState
    {
        Home,
        Playing,
        GameOver
    }
}
