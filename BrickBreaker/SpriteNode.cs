using Raylib_cs;
using System.Collections.Generic;
using System.Numerics;
using static Raylib_cs.Raylib;

public class SpriteNode
{
    private static List<Texture2D> loadedTextures = new List<Texture2D>();

    private Texture2D texture;

    public SpriteNode(string imagePath)
    {
        LoadTexture(imagePath);
        loadedTextures.Add(texture);
    }

    private void LoadTexture(string imagePath)
    {
        Image image = LoadImage(imagePath);
        texture = LoadTextureFromImage(image);
        UnloadImage(image);
    }

    public void Draw(Vector2 position, Color color)
    {
        DrawTexture(texture, (int)position.X, (int)position.Y, color);
    }

    public static void UnloadAllTextures()
    {
        foreach (Texture2D texture in loadedTextures)
        {
            UnloadTexture(texture);
        }
        loadedTextures.Clear();
    }
}


