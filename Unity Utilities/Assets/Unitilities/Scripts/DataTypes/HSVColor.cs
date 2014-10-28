using UnityEngine;

/// <summary>
/// Tuple class of 3 int
/// </summary>
/// <typeparam name="First">First float</typeparam>
/// <typeparam name="Second">Second float</typeparam>
/// <typeparam name="Third">Third int</typeparam>
[System.Serializable]
public class HSVColor : Tuple4<float, float, float, float>
{
    public float Hue
    {
        set
        {
            this.first = value;
        }
        get
        {
            return this.first;
        }
    }

    public float Saturation
    {
        set
        {
            this.second = value;
        }
        get
        {
            return this.second;
        }
    }

    public float Value
    {
        set
        {
            this.third = value;
        }
        get
        {
            return this.third;
        }
    }

    public float Alpha
    {
        set
        {
            this.fourth = value;
        }
        get
        {
            return this.fourth;
        }
    }

    public Color RGB
    {
        get
        {
            return ColorHelper.HSVToRGB(this.first, this.second, this.third, 1f);
        }
    }

    public Color RGBA
    {
        get
        {
            return ColorHelper.HSVToRGB(this.first, this.second, this.third, this.fourth);
        }
    }


    public static HSVColor white
    {
        get { return new HSVColor(0, 0, 1f, 1f); }
    }

    public static HSVColor black
    {
        get { return new HSVColor(0, 0, 0f, 1f); }
    }
    

    public void CopyValuesFrom(Color c)
    {
        HSVColor hsv = c.HSV();
        this.first = hsv.Hue;
        this.second = hsv.Saturation;
        this.third = hsv.Value;
        this.fourth = hsv.Alpha;
    }

    public HSVColor(float h, float s, float v, float a = 1f)
        : base(h, s, v, a)
    {
    }
}