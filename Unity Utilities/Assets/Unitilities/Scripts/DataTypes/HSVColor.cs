using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public struct HSVColor // : Tuple4<float, float, float, float>
{
    private float hue ;
    private float saturation;
    private float value;
    private float alpha;

    private Color rgb;
    private bool wasModified;

    public Color RGB
    {
        get
        {
            if (wasModified)
            {
                rgb = this.ToRGB();
                wasModified = false;
                return rgb;
            }
            else
            {
                return rgb;
            }
        }
    }

    /// <summary>
    /// [0, 360] degrees
    /// </summary>
    public float Hue
    {
        set
        {
            hue = value % 360f;
            wasModified = true;
        }
        get
        {
            return hue;
        }
    }

    public float Saturation
    {
        set
        {
            this.saturation = Mathf.Clamp01(value);
            wasModified = true;
        }
        get
        {
            return this.saturation;
        }
    }

    public float Value
    {
        set
        {
            this.value = Mathf.Clamp01(value);
            wasModified = true;
        }
        get
        {
            return this.value;
        }
    }

    public float Alpha
    {
        set
        {
            this.alpha = Mathf.Clamp01(value);
            wasModified = true;
        }
        get
        {
            return this.alpha;
        }
    }
    

    public static HSVColor white
    {
        get { return new HSVColor(0f, 0f, 1f, 1f); }
    }

    public static HSVColor black
    {
        get { return new HSVColor(0f, 0f, 0f, 1f); }
    }
    
    public HSVColor(Color c)
        : this(c.ToHSV())
    {
    }

    public HSVColor(HSVColor original)
        : this(original.hue, original.saturation, original.value, original.alpha)
    {
    }

    public HSVColor(float h, float s, float v, float a = 1f)
    {
        hue = h % 360f;
        saturation = Mathf.Clamp01(s);
        value = Mathf.Clamp01(v);
        alpha = Mathf.Clamp01(a);
        wasModified = true;
        rgb = Color.black;

        /*Hue = h;
        Saturation = s;
        Value = v;
        Alpha = a;*/
        
        //this.wasModified = true;
    }

    public void CopyFrom(Color c)
    {
        HSVColor hsv = c.ToHSV();
        this.hue = hsv.Hue;
        this.saturation= hsv.Saturation;
        this.value = hsv.Value;
        this.alpha = hsv.Alpha;
    }




    public static HSVColor Lerp(HSVColor start, HSVColor end, float amount)
    {
        if (amount >= 1f)
            return new HSVColor(end);
        else if (amount <= 0f)
            return new HSVColor(start);

        return new HSVColor(start.hue + (end.hue - start.hue) * amount,
                            start.saturation + (end.saturation - start.saturation) * amount,
                            start.value + (end.value - start.value) * amount,
                            start.alpha + (end.alpha - start.alpha) * amount
                            );
    }


    public Vector3 ToVector3()
    {
        return new Vector3(this.hue, this.saturation, this.value);
    }

    public Vector4 ToVector4()
    {
        return new Vector4(this.hue, this.saturation, this.value, this.alpha);
    }

    public override string ToString()
    {
        return string.Format("HSV({0}, {1}, {2}, {3})", this.hue, this.saturation, this.value, this.alpha);
    }




    public static bool operator ==(HSVColor a, HSVColor b)
    {
        /*bool isNullA = IsNull(a);
        bool isNullB = IsNull(b);

        if (isNullA && !isNullB)
            return false;

        if (!isNullA && isNullB)
            return false;

        if (isNullA && isNullB)
            return true;*/

        return
            Mathf.Approximately(a.hue, b.hue) &&
            Mathf.Approximately(a.saturation, b.saturation) &&
            Mathf.Approximately(a.value, b.value) &&
            Mathf.Approximately(a.alpha, b.alpha);
            /*a.first.Equals(b.first) &&
            a.second.Equals(b.second) &&
            a.third.Equals(b.third) &&
            a.fourth.Equals(b.fourth);*/
    }

    public static bool operator !=(HSVColor a, HSVColor b)
    {
        return !(a == b);
    }

    public static HSVColor operator +(HSVColor a, HSVColor b)
    {
        /*if (IsNull(a) || IsNull(b))
        {
            throw new System.ArgumentException("Fatal error: Cannot sum null HSVColors"); 
        }*/

        return new HSVColor(a.hue + b.hue, a.saturation + b.saturation, a.value + b.value, a.alpha + b.alpha);
    }

    public static HSVColor operator -(HSVColor a, HSVColor b)
    {
        /*if (IsNull(a) || IsNull(b))
        {
            throw new System.ArgumentException("Fatal error: Cannot sum null HSVColors");
        }*/

        return new HSVColor(a.hue - b.hue, a.saturation - b.saturation, a.value - b.value, a.alpha - b.alpha);
    }

    public static HSVColor operator *(float factor, HSVColor a)
    {
        return a * factor;
    }

    public static HSVColor operator *(HSVColor a, float factor)
    {
        /*if (IsNull(a))
        {
            throw new System.ArgumentException("Fatal error: Cannot multiply a null HSVColors");
        }*/

        return new HSVColor(a.hue * factor, a.saturation * factor, a.value * factor, a.alpha * factor);
    }

    public static HSVColor operator /(HSVColor a, float factor)
    {
        /*if (IsNull(a))
        {
            throw new System.ArgumentException("Fatal error: Cannot divide a null HSVColor");
        }*/

        if (Mathf.Approximately(factor, 0f))
            throw new System.ArgumentException("Fatal error: Cannot divide by 0");
        
        return new HSVColor(a.hue / factor, a.saturation / factor, a.value / factor, a.alpha / factor);
    }

    /*private static bool IsNull(object obj)
    {
        return object.ReferenceEquals(obj, null);
    }*/

    public override bool Equals(object obj)
    {
        var other = (HSVColor) obj;
        if (object.ReferenceEquals(other, null))
            return false;
        else
            return this == other;
    }

    public override int GetHashCode()
    {
        int hash = 17;
        hash = hash * 23 + hue.GetHashCode();
        hash = hash * 23 + saturation.GetHashCode();
        hash = hash * 23 + value.GetHashCode();
        hash = hash * 23 + alpha.GetHashCode();
        return hash;
    }

}

