using UnityEngine;

public class TextureHelper
{

    public static Texture2D CreateBackgroundTexture(Color c)
    {
        Texture2D t = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        t.SetPixel(0, 0, c);
        t.Apply();

        return t;
    }

}
