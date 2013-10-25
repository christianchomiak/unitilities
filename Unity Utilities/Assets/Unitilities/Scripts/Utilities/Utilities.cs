using UnityEngine;
using System.Collections;

public class Utilities
{
    #region Platforms

    public static bool PlatformIsDesktop()
    {
        return  Utilities.PlatformIsEditor()            ||
                Utilities.PlatformIsDesktopStandalone() ||
                Utilities.PlatformIsWeb();
    }

    public static bool PlatformIsDesktopStandalone()
    {
        return Application.platform == RuntimePlatform.WindowsPlayer    ||
               Application.platform == RuntimePlatform.LinuxPlayer      ||
               Application.platform == RuntimePlatform.OSXPlayer        ||
               Application.platform == RuntimePlatform.MetroPlayerX86   ||
               Application.platform == RuntimePlatform.MetroPlayerX64;  
    }

    public static bool PlatformIsEditor()
    {
        return Application.platform == RuntimePlatform.WindowsEditor ||
               Application.platform == RuntimePlatform.OSXEditor;
    }

    public static bool PlatformIsWeb()
    {
        return Application.platform == RuntimePlatform.WindowsWebPlayer ||
               Application.platform == RuntimePlatform.OSXWebPlayer;
    }

    public static bool PlatformIsMobile()
    {
        return Application.platform == RuntimePlatform.Android      ||
               Application.platform == RuntimePlatform.IPhonePlayer ||
               Application.platform == RuntimePlatform.BB10Player   ||
               Application.platform == RuntimePlatform.WP8Player    ||
               Application.platform == RuntimePlatform.MetroPlayerARM;
    }



    #endregion


    #region EnumsFunctions

    public static T GetRandomEnum<T>(int limit)
    {
        System.Array A = System.Enum.GetValues(typeof(T));

        if (limit < 0 || limit >= A.Length)
        {
            return GetRandomEnum<T>();
        }

        T V = (T)A.GetValue(UnityEngine.Random.Range(0, limit));
        return V;
    }

    public static T GetRandomEnum<T>()
    {
        System.Array A = System.Enum.GetValues(typeof(T));
        T V = (T)A.GetValue(UnityEngine.Random.Range(0, A.Length));
        return V;
    }

    #endregion


    #region Randoms

    /// <summary>
    /// Returns 1 or -1 at random.
    /// </summary>
    /// <returns>1 or -1</returns>
    public static int RandomSign()
    {
        int sign = UnityEngine.Random.value > 0.5f ? 1 : -1;
        return sign;
    }

    public static Vector2 RandomNormalizedVector2()
    {
        //Random.value: random number between 0.0 [inclusive] and 1.0 [inclusive] 
        int signX = RandomSign();
        int signY = RandomSign();
        Vector2 randomVector = new Vector2(signX * UnityEngine.Random.value,
                                            signY * UnityEngine.Random.value);
        return randomVector.normalized;
    }

    public static Vector3 RandomNormalizedVector3()
    {
        //Random.value: random number between 0.0 [inclusive] and 1.0 [inclusive] 
        int signX = RandomSign();
        int signY = RandomSign();
        int signZ = RandomSign();
        Vector3 randomVector = new Vector3(signX * UnityEngine.Random.value,
                                            signY * UnityEngine.Random.value,
                                            signZ * UnityEngine.Random.value);
        return randomVector.normalized;
    }

    #endregion


    #region Conversions

    public static bool IntToBool(int i)
    {
        if (i <= 0) return false;

        return true;
    }

    public static int BoolToInt(bool b)
    {
        if (b) return 1;

        return 0;
    }

    #endregion


    #region Strings

    public static string FillNumberWithLeftZeros(int number, int maxDigits)
    {
        string filledNumber = number.ToString();
        int zerosToAdd = maxDigits - filledNumber.Length;
        for (int i = 0; i < zerosToAdd; i++)
        {
            filledNumber = "0" + filledNumber;
        }

        return filledNumber;
    }

    #endregion


    #region ColorGeneration

    /// <summary>
    /// Generates a RGB color using random hue, saturation, value (brightness)
    /// </summary>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    public static Color HSV_RandomColor(bool useGoldenRation = false) // 0.5, 0.95
    {
        return HSV_RandomColorWithSV(Random.value, Random.value, useGoldenRation);
    }

    /// <summary>
    /// Generates a RGB color using random hue and saturation, and specified value (brightness)
    /// </summary>
    /// <param name="value">Brightness of the color</param>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    public static Color HSV_RandomColorWithValue(float value, bool useGoldenRation = false) // 0.5, 0.95
    {
        return HSV_RandomColorWithSV(Random.value, value, useGoldenRation);
    }

    /// <summary>
    /// Generates a RGB color using random hue and value (brightness), and specified saturation
    /// </summary>
    /// <param name="saturation">Saturation of the color</param>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    public static Color HSV_RandomColorWithSaturation(float saturation, bool useGoldenRation = false) // 0.5, 0.95
    {
        return HSV_RandomColorWithSV(saturation, Random.value, useGoldenRation);
    }

    /// <summary>
    /// Generates a RGB color using random hue and specified saturation and value (brightness)
    /// </summary>
    /// <param name="saturation">Saturation of the color</param>
    /// <param name="value">Brightness of the color</param>
    /// <param name="useGoldenRation">'True': the golden ration will be used to better randomize the hue</param>
    /// <returns>RGB color</returns>
    public static Color HSV_RandomColorWithSV(float saturation, float value, bool useGoldenRation = false)
    {
        //Rangos: 0 - 0.16667 - 0.33337 - 0.5 - 0.66667 - 0.83337 - 1
        float hueValue = Random.value;

        if (useGoldenRation)
        {
            float goldenRadioConj = 0.618033988749895f;
            hueValue += goldenRadioConj;
            hueValue %= 1;
        }

        return Utilities.HSVToRGB(hueValue, saturation, value);
    }

    /// <summary>
    /// Generates a RGB color using random saturation and value (brightness) and specified hue and alpha
    /// </summary>
    /// <param name="hue">Hue of the color</param>
    /// <param name="alpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
    /// <returns>RGB color</returns>
    public static Color HSV_RandomColorWithHue(float hue, float alpha = 1f)
    {
        return HSVToRGB(hue, Random.value, Random.value, alpha);
    }

    /// <summary>
    /// Generates a RGB color using random saturation and value (brightness) and specified hue and alpha
    /// </summary>
    /// <param name="hue">Hue of the color</param>
    /// <param name="saturation">Saturation of the color</param>
    /// <param name="alpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
    /// <returns>RGB color</returns>
    public static Color HSV_RandomColorWithHS(float hue, float saturation, float alpha = 1)
    {
        return HSVToRGB(hue, saturation, Random.value, alpha);
    }

    /// <summary>
    /// Generates a RGB color using random saturation and value (brightness) and specified hue and alpha
    /// </summary>
    /// <param name="hue">Hue of the color</param>
    /// <param name="value">Brightness of the color</param>
    /// <param name="alpha">Transparency: 0 = 100% transparent, 1 = 0% transparent</param>
    /// <returns>RGB color</returns>
    public static Color HSV_RandomColorWithHV(float hue, float value, float alpha = 1)
    {
        return HSVToRGB(hue, Random.value, value, alpha);
    }


    public static Color HSVToRGB(Vector4 hsv)
    {
        return HSVToRGB(hsv.x, hsv.y, hsv.z, hsv.w);
    }

    //Based on http://wiki.unity3d.com/index.php?title=HSBColor
    /// <summary>
    /// Generates a RGB color using specified hue, saturation, value (brightness) and  hue and alpha
    /// </summary>
    /// <param name="hue"></param>
    /// <param name="saturation"></param>
    /// <param name="value"></param>
    /// <param name="alpha"></param>
    /// <returns>RGB color</returns>
    public static Color HSVToRGB(float hue, float saturation, float value, float alpha = 1.0f) //value = brightness
    {
        float r = value;
        float g = value;
        float b = value;
        if (saturation != 0)
        {
            float max = value;
            float dif = value * saturation;
            float min = value - dif;

            float h = hue * 360f;

            if (h < 60f)
            {
                r = max;
                g = h * dif / 60f + min;
                b = min;
            }
            else if (h < 120f)
            {
                r = -(h - 120f) * dif / 60f + min;
                g = max;
                b = min;
            }
            else if (h < 180f)
            {
                r = min;
                g = max;
                b = (h - 120f) * dif / 60f + min;
            }
            else if (h < 240f)
            {
                r = min;
                g = -(h - 240f) * dif / 60f + min;
                b = max;
            }
            else if (h < 300f)
            {
                r = (h - 240f) * dif / 60f + min;
                g = min;
                b = max;
            }
            else if (h <= 360f)
            {
                r = max;
                g = min;
                b = -(h - 360f) * dif / 60 + min;
            }
            else
            {
                r = 0;
                g = 0;
                b = 0;
            }
        }

        return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), alpha);
    }

    /// <summary>
    /// Returns the HSV values of a RGB color
    /// </summary>
    /// <param name="r">'Red' component</param>
    /// <param name="g">'Green' component</param>
    /// <param name="b">'Blue' component</param>
    /// <returns></returns>
    public static Vector3 RGBToHSV(float r, float g, float b)
    {
        return (new Color(r, g, b)).GetHSV();
        //return RGBToHSV(new Color(r, g, b));
    }

    /// <summary>
    /// Returns the HSV values of a RGB color
    /// </summary>
    /// <param name="color">RGB color</param>
    /// <returns>Vector3 containing the HSV representation of the RGB color</returns>
    public static Vector3 RGBToHSV(Color color) //, out float h, out float s, out float v)
    {
        return color.GetHSV();

        /*float h, s, v;

        float min = Mathf.Min(Mathf.Min(color.r, color.g), color.b);
        float max = Mathf.Max(Mathf.Max(color.r, color.g), color.b);
        float delta = max - min;

        // value is our max color
        v = max;

        // saturation is percent of max
        if (!Mathf.Approximately(max, 0))
            s = delta / max;
        else
        {
            // all colors are zero, no saturation and hue is undefined
            s = 0;
            h = -1;
            return new Vector3(h, s, v);
        }

        // grayscale image if min and max are the same
        if (Mathf.Approximately(min, max))
        {
            v = max;
            s = 0;
            h = -1;
            return new Vector3(h, s, v);
        }

        // hue depends which color is max (this creates a rainbow effect)
        if (color.r == max)
            h = (color.g - color.b) / delta;            // between yellow & magenta
        else if (color.g == max)
            h = 2 + (color.b - color.r) / delta;                // between cyan & yellow
        else
            h = 4 + (color.r - color.g) / delta;                // between magenta & cyan

        // turn hue into 0-360 degrees
        h *= 60;
        if (h < 0)
            h += 360;

        return new Vector3(h, s, v);*/
    }

    /// <summary>
    /// Generates a random RGB color
    /// </summary>
    /// <returns>RGB color</returns>
    public static Color GenerateColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    public static Color GenerateRandomPastelColor()
    {
        return Utilities.HSV_RandomColorWithSV(Random.Range(0.45f, 0.75f), Random.Range(0.5f, 0.95f), false);
    }

    #endregion

    /*
    #region ColorGeneration

    public static Color GenerateColorWithHue(float _hue, float alpha  = 1.0f) // 0.5, 0.95
    {
        return HSVToRGB(_hue, Random.Range(0.45f, 0.75f), Random.Range(0.5f, 0.95f), alpha);
    }

    public static Color GenerateColorWithHSV(float value, bool useGoldenRation = false) // 0.5, 0.95
    {
        return GenerateColorWithHSV(Random.value, value, useGoldenRation);
    }

    public static Color GenerateColorWithHSV(bool useGoldenRation = false) // 0.5, 0.95
    {
        return GenerateColorWithHSV(Random.value, Random.value, useGoldenRation);
    }

    public static Color GenerateColorWithHSV(float _saturation, float _value, bool useGoldenRation = false)
    {
        //Rangos: 0 - 0.16667 - 0.33337 - 0.5 - 0.66667 - 0.83337 - 1
        float hueValue = Random.value; // Random.Range(0.0f, 1.0f); 

        if (useGoldenRation)
        {
            float goldenRadioConj = 0.618033988749895f;
            hueValue += goldenRadioConj;
            hueValue %= 1;
        }

        return Utilities.HSVToRGB(hueValue, _saturation, _value);
    }

    public static Color GenerateRandomPastelColor()
    {
        return Utilities.GenerateColorWithHSV(Random.Range(0.45f, 0.75f), Random.Range(0.5f, 0.95f), false);
    }

    //Based on http://wiki.unity3d.com/index.php?title=HSBColor
    public static Color HSVToRGB(float hue, float saturation, float value, float alpha = 1.0f) //value = brightness
    { 
        float r = value;
        float g = value;
        float b = value;
        if (saturation != 0)
        {
            float max = value;
            float dif = value * saturation;
            float min = value - dif;

            float h = hue * 360f;

            if (h < 60f)
            {
                r = max;
                g = h * dif / 60f + min;
                b = min;
            }
            else if (h < 120f)
            {
                r = -(h - 120f) * dif / 60f + min;
                g = max;
                b = min;
            }
            else if (h < 180f)
            {
                r = min;
                g = max;
                b = (h - 120f) * dif / 60f + min;
            }
            else if (h < 240f)
            {
                r = min;
                g = -(h - 240f) * dif / 60f + min;
                b = max;
            }
            else if (h < 300f)
            {
                r = (h - 240f) * dif / 60f + min;
                g = min;
                b = max;
            }
            else if (h <= 360f)
            {
                r = max;
                g = min;
                b = -(h - 360f) * dif / 60 + min;
            }
            else
            {
                r = 0;
                g = 0;
                b = 0;
            }
        }

        return new Color(Mathf.Clamp01(r), Mathf.Clamp01(g), Mathf.Clamp01(b), alpha);
    }

    public static Color GenerateColor()
    {
        return new Color(Random.value, Random.value, Random.value); //(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }

    #endregion
    */



    #region Trigonometry

    /// <summary>
    /// Calculates a position over a sphere
    /// </summary>
    /// <param name="center">Position of the center of the sphere</param>
    /// <param name="radius">Radius of the sphere</param>
    /// <param name="angleInAxisX">Angle of Axis X</param>
    /// <param name="angleInAxisY">Angle of Axis Y</param>
    /// <param name="anglesInRadians">True: the angles are in radians. False: the angles are in degrees</param>
    /// <returns>A position on the sphere</returns>
    public static Vector3 PointOverSphere(Vector3 center, float radius, float angleInAxisX, float angleInAxisY, bool anglesInRadians = false)
    {
        float x, y, z;
        float angleX = 0f, angleY = 0f;

        if (!anglesInRadians)
        {
            angleX = angleInAxisX * Mathf.Deg2Rad;
            angleY = angleInAxisY * Mathf.Deg2Rad;
        }
        else
        {
            angleX = angleInAxisX;
            angleY = angleInAxisY;
        }

        x = center.x + Mathf.Cos(angleY) * radius * Mathf.Cos(angleX);
        y = center.y + Mathf.Sin(angleX) * radius;
        z = center.z + Mathf.Sin(angleY) * radius * Mathf.Cos(angleX);

        return new Vector3(x, y, z);
    }

    #endregion

    #region Textures

    public static Texture2D CreateBackgroundTexture(Color c)
    {
        Texture2D t = new Texture2D(1, 1, TextureFormat.ARGB32, false);
        t.SetPixel(0, 0, c);
        t.Apply();

        return t;
    }

    #endregion


    #region Instances

    public static GameObject CreateInstance(GameObject _original)
    {
        if (_original == null)
        {
            Debug.LogError("Original Game Object is null and cannot be cloned.");
            return null;
        }

        return CreateInstance(_original, null, Vector3.zero, _original.name);
    }

    public static GameObject CreateInstance(GameObject _original, Vector3 _position)
    {
        if (_original == null)
        {
            Debug.LogError("Original Game Object is null and cannot be cloned.");
            return null;
        }

        return CreateInstance(_original, null, _position, _original.name);
    }

    public static GameObject CreateInstance(GameObject _original, Transform _parent)
    {
        if (_original == null)
        {
            Debug.LogError("Original Game Object is null and cannot be cloned.");
            return null;
        }

        return CreateInstance(_original, _parent, Vector3.zero, _original.name);
    }

    public static GameObject CreateInstance(GameObject _original, Transform _parent, Vector3 _position)
    {
        if (_original == null)
        {
            Debug.LogError("Original Game Object is null and cannot be cloned.");
            return null;
        }

        return CreateInstance(_original, _parent, _position, _original.name);
    }

    public static GameObject CreateInstance(GameObject _original, string _name)
    {
        return CreateInstance(_original, null, Vector3.zero, _name);
    }

    public static GameObject CreateInstance(GameObject _original, Vector3 _position, string _name)
    {
        return CreateInstance(_original, null, _position, _name);
    }

    public static GameObject CreateInstance(GameObject _original, Transform _parent, string _name)
    {
        return CreateInstance(_original, _parent, Vector3.zero, _name);
    }


    //leParent == null to put the object at the root
    public static GameObject CreateInstance(GameObject _original, Transform _parent, Vector3 _position, string _name) //, Quaternion rotation)
    {
        if (_original == null)
        {
            Debug.LogError("Game Object \'" + _original.name + "\' is null and cannot be cloned.");
            return null;
        }

        GameObject newGameObject = GameObject.Instantiate(_original) as GameObject;

        newGameObject.name = _name; //_original.name;
        newGameObject.transform.parent = _parent;
        newGameObject.transform.position = _position;

        return newGameObject;
    }

    #endregion

    #region Indexes

    //Left-to-right
    public static int IndexFrom2DTo1D(int x, int y, int width)
    {
        return x + (y * width);
    }

    //Top-to-bottom
    public static int IndexFrom2DTo1D_Alt(int x, int y, int height)
    {
        return y + (x * height);
    }

    public static TupleI IndexesFrom1DTo2D_W(int index, int width)
    {
        return new TupleI(index % width, index / width);
    }

    public static TupleI IndexesFrom1DTo2D_H(int index, int height)
    {
        return new TupleI(index / height, index / height);
    }

    #endregion


    public static int CalculateFontSize(int _desiredSize) //35
    {
        int size = (Screen.width * _desiredSize / 854);// _desiredSize / (854 / Screen.width);

        return size;
    }

    public static int CalculateFontSize() //35
    {
        return CalculateFontSize(35);
    }
    
}
