using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Reflection.Metadata;
using static Raylib_cs.Raylib;

public class Ball
{
    public Vector2 Position;
    private Vector2 Velocity;

    public int Radius { get; }

    private int paddleBallOffset;
    private int BallSpeed;
    private bool ballLaunched = false;

    public Ball(Vector2 initialPosition)
    {
        Position = initialPosition;
        Velocity = new Vector2(0, -BallSpeed);  // Initial velocity, moving upward
        Radius = 10; // Size of ball in pixels
        BallSpeed = 10; // Speed of the ball
        paddleBallOffset = 5; // Amount of pixels between ball and paddle on before game starts
    }

    internal void StartGame()
    {
        // Set the initial velocity to move upward when the game starts
        Velocity = new Vector2(0, -BallSpeed);
        ballLaunched = true;
    }

    internal void Update(Paddle paddle)
    {
        if (!ballLaunched)
        {
            // If the game hasn't started, move the ball with the paddle
            Position.X = paddle.paddlePosition.X + paddle.paddleWidth / 2; // Position horizontally on the paddle when gaame starts
            Position.Y = paddle.paddlePosition.Y - paddleBallOffset; // Position vertically above the paddle when game starts
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
    }

    internal void BounceOffPaddle(Paddle paddle)
    {
        // Handle paddle collision and bounce logic
        float hitPositionOnPaddle = Position.X - paddle.paddlePosition.X;
        float normalizedHitPosition = hitPositionOnPaddle / 200f; // Normalize hit position between 0 and 1
        float maxReflectionAngle = MathF.PI / 6;  // Equivalent to 30 degrees in radians
        float reflectionAngle = (2 * normalizedHitPosition - 1) * maxReflectionAngle;

        // Calculate new velocity based on reflection angle
        Velocity = new Vector2(BallSpeed * MathF.Sin(reflectionAngle), -BallSpeed * MathF.Cos(reflectionAngle));
    }

    internal void BounceOffBrick(Brick brick)
    {
        Velocity = new Vector2(Velocity.X, -Velocity.Y);

        // Move the ball slightly away from the brick to prevent sticking
        float moveAwayDistance = 2.0f;
        Position += Velocity * moveAwayDistance;
    }

    internal void Draw()
    {
        // Draw the ball at the current position
        DrawCircle((int)Position.X, (int)Position.Y, Radius, Color.WHITE);
    }
}


