/// <summary>
/// ColorHelper v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Functions that facilitate working with Unity Textures.
/// </summary>

using UnityEngine;

namespace Unitilities
{
    public static class TextureHelper
    {

        public static Texture2D CreateBackgroundTexture(Color c)
        {
            Texture2D t = new Texture2D(1, 1, TextureFormat.ARGB32, false);
            t.SetPixel(0, 0, c);
            t.Apply();

            return t;
        }
    }
}
