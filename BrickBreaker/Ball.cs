using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata;
using static Raylib_cs.Raylib;

public class Ball
{
    public Vector2 Position { get; private set; }
    private Vector2 Velocity;
    public const float BallSpeed = 10f;  // Adjust the speed as needed
    private bool ballLaunched = false;

    public Ball(Vector2 initialPosition)
    {
        Position = initialPosition;
        Velocity = new Vector2(0, -BallSpeed);  // Initial velocity, moving upward
    }

    internal void StartGame()
    {
        // Set the initial velocity to move upward when the game starts
        Velocity = new Vector2(0, -BallSpeed);
        ballLaunched = true;
    }

    internal void Update(Paddle paddle, List<Brick> bricks)
    {
        if (!ballLaunched)
        {
            // If the game hasn't started, keep the ball in place
            Velocity = new Vector2(0, 0);
        }

        // Update ball position based on velocity
        Position += Velocity;

        // Bounce off the top border
        if (Position.Y <= 0)
        {
            Velocity.Y = -Velocity.Y;  // Reverse the Y velocity to bounce
        }

        // Bounce off the sides of the screen
        if (Position.X <= 0 || Position.X >= GetScreenWidth())
        {
            Velocity.X = -Velocity.X;  // Reverse the X velocity to bounce
        }

        // Check for collision with the paddle
        if (Position.Y + 10 >= paddle.paddlePosition.Y && Position.Y - 10 <= paddle.paddlePosition.Y + 14 &&
            Position.X >= paddle.paddlePosition.X && Position.X <= paddle.paddlePosition.X + 200)
        {
            BounceOffPaddle(paddle);
        }

        // Check for collision with bricks
        foreach (Brick brick in bricks)
        {
            if (brick.IsActive && Position.Y + 10 >= brick.brickPosition.Y && Position.Y - 10 <= brick.brickPosition.Y + 40 &&
            Position.X >= brick.brickPosition.X && Position.X <= brick.brickPosition.X + 100)
            {
                brick.IsActive = false;  // Deactivate the brick
                BounceOffBrick(brick);
            }
        }
    }

    private void BounceOffPaddle(Paddle paddle)
    {
        // Handle paddle collision and bounce logic
        float hitPositionOnPaddle = Position.X - paddle.paddlePosition.X;
        float normalizedHitPosition = hitPositionOnPaddle / 200f; // Normalize hit position between 0 and 1
        float maxReflectionAngle = MathF.PI / 6;  // Equivalent to 30 degrees in radians
        float reflectionAngle = (2 * normalizedHitPosition - 1) * maxReflectionAngle;

        // Calculate new velocity based on reflection angle
        Velocity = new Vector2(BallSpeed * MathF.Sin(reflectionAngle), -BallSpeed * MathF.Cos(reflectionAngle));
    }

    private void BounceOffBrick(Brick brick)
    {
        Velocity = new Vector2(Velocity.X, -Velocity.Y);

        // Move the ball slightly away from the brick to prevent sticking
        float moveAwayDistance = 2.0f;
        Position += Velocity * moveAwayDistance;
    }




    internal void Draw()
    {
        // Draw the ball at the current position
        DrawCircle((int)Position.X, (int)Position.Y, 10, Color.WHITE);
    }
}


