using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ImageUtil : MonoBehaviour
{
    public static Sprite GetSpriteByPath(string path)
    {
        Texture2D tmp = LoadFromFile(path);
        Sprite sprite = Sprite.Create(tmp, new Rect(0, 0, tmp.width, tmp.height), new Vector2(10, 10));
        return sprite;
    }

    private static Texture2D LoadFromFile(string path)
    {
        Texture2D tmp = new Texture2D(100, 100);
        tmp.LoadImage(ReadPNG(path));
        return tmp;
    }
    private static byte[] ReadPNG(string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
        fileStream.Seek(0, SeekOrigin.Begin);
        byte[] binary = new byte[fileStream.Length];
        fileStream.Read(binary, 0, (int)fileStream.Length);
        fileStream.Close();
        fileStream.Dispose();
        fileStream = null;
        return binary;
    }
}
