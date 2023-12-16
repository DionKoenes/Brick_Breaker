using Raylib_cs;
using System.Numerics;

public class Background : SpriteNode
{
    public Vector2 backgroundPosition;

    public Background()
        : base("resources/background.png")
    {
        backgroundPosition = new Vector2(0,0);
    }

    internal void Draw()
    {
        Draw(backgroundPosition, Color.WHITE);
    }
}



