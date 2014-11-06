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

    /*public Color RGB
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
    }*/


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
        HSVColor hsv = c.ToHSV();
        this.first = hsv.Hue;
        this.second = hsv.Saturation;
        this.third = hsv.Value;
        this.fourth = hsv.Alpha;
    }

    public HSVColor(float h, float s, float v, float a = 1f)
        : base(h, s, v, a)
    {
    }

    public HSVColor(HSVColor original)
        : base(original.first, original.second, original.third, original.fourth)
    {

    }

    public static HSVColor Lerp(HSVColor start, HSVColor end, float amount)
    {
        if (amount >= 1f)
            return new HSVColor(end);
        else if (amount <= 0f)
            return new HSVColor(start);

        return new HSVColor(start.first + (end.first - start.first) * amount,
                            start.second + (end.second - start.second) * amount,
                            start.third + (end.third - start.third) * amount,
                            start.fourth + (end.fourth - start.fourth) * amount
                            );
    }


    public Vector3 ToVector3()
    {
        return new Vector3(this.first, this.second, this.third);
    }

    public Vector4 ToVector4()
    {
        return new Vector4(this.first, this.second, this.third, this.fourth);
    }

    public override string ToString()
    {
        return string.Format("HSV({0}, {1}, {2}, {3})", first, second, third, fourth);
    }
}
