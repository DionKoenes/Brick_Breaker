using static Raylib_cs.Raylib;
using System.Collections.Generic;
using System.Numerics;
using System;

public class BrickManager
{
    private List<Brick> bricks;

    public BrickManager()
    {
        Random random = new Random();
        bricks = new List<Brick>();

        int brickWidth = 104; // Adjust this value to set the width of the bricks
        int brickHeight = 42; // Adjust this value to set the height of the bricks
        int horizontalGap = 20; // Adjust this value to set the gap between bricks horizontally
        int verticalGap = 20;   // Adjust this value to set the gap between bricks vertically
        int hrows = 7; // Adjust this value to set the number of horizontal rows
        int vrows = 4; // Adjust this value to set the number of vertical rows

        // Calculate the total width of the bricks in the row
        int totalRowWidth = hrows * (brickWidth + horizontalGap) - horizontalGap;

        // Calculate the starting X coordinate to center the bricks
        int startX = (GetScreenWidth() - totalRowWidth) / 2;

        int startY = 50; // Adjust this value to set the initial vertical position

        // Add bricks to the list with adjusted positions || h =  horizontal, v = vertical
        for (int h = 0; h < hrows; h++)
        {
            for (int v = 0; v < vrows; v++)
            {
                // Randomly choose the type of brick (Normal or Special)
                Brick.BrickType brickType = (random.Next(2) == 0) ? Brick.BrickType.Normal : Brick.BrickType.Special;

                Brick brick = new Brick(
                    new Vector2(startX + h * (brickWidth + horizontalGap), startY + v * (brickHeight + verticalGap)),
                    brickWidth,
                    brickHeight,
                    brickType
                );

                bricks.Add(brick);
            }
        }
    }

    public List<Brick> Bricks
    {
        get { return bricks; }
    }

    public void Draw()
    {
        foreach (Brick brick in bricks)
        {
            brick.Draw();
        }
    }
}
