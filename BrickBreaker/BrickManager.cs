using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Collections.Generic;
using System.Numerics;
using System;

public class BrickManager
{
    private List<Brick> bricks;

    public BrickManager()
    {
        bricks = new List<Brick>();

        int brickWidth = 100; // Adjust this value to set the width of the bricks
        int brickHeight = 40; // Adjust this value to set the height of the bricks
        int horizontalGap = 20; // Adjust this value to set the gap between bricks horizontally
        int verticalGap = 20;   // Adjust this value to set the gap between bricks vertically

        // Calculate the total width of the bricks in the row
        int totalRowWidth = 7 * (brickWidth + horizontalGap) - horizontalGap;

        // Calculate the starting X coordinate to center the bricks
        int startX = (GetScreenWidth() - totalRowWidth) / 2;

        int startY = 50; // Adjust this value to set the initial vertical position

        // Add bricks to the list with adjusted positions || h =  horizontal, v = vertical
        for (int h = 0; h < 7; h++)
        {
            for (int v = 0; v < 4; v++)
            {
                Brick brick = new Brick(new Vector2(startX + h * (brickWidth + horizontalGap), startY + v * (brickHeight + verticalGap)), brickWidth, brickHeight);
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
