/// <summary>
/// HSVColor v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Struct to represent colors in the HSV (hue, saturation, value) system.
/// Includes:
///     * Functions to convert from/to RGB and to HEX.
///     * Basic operations: ==, !=, +, -, *, /
///     * Shortcuts to create: black and white instances.
/// </summary>

using UnityEngine;

namespace Unitilities.Colors
{

    [System.Serializable]
    public struct HSVColor
    {
        #region Variables

        [SerializeField] private float hue;
        [SerializeField] private float saturation;
        [SerializeField] private float value;
        [SerializeField] private float alpha;

        #endregion

        #region Constructors

        public HSVColor(float h, float s, float v, float a = 1f)
        {
            hue = h % 360f;
            saturation = Mathf.Clamp01(s);
            value = Mathf.Clamp01(v);
            alpha = Mathf.Clamp01(a);

            /*Hue = h;
            Saturation = s;
            Value = v;
            Alpha = a;*/

            //this.wasModified = true;
        }

        public HSVColor(HSVColor original)
            : this(original.hue, original.saturation, original.value, original.alpha)
        {
        }


        public HSVColor(Color c)
            : this(HSVColor.FromRGB(c))
        {
        }

        #endregion

        #region Accessors

        /// <summary>
        /// [0, 360] degrees
        /// </summary>
        public float Hue
        {
            set
            {
                hue = value % 360f;
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
                saturation = Mathf.Clamp01(value);
            }
            get
            {
                return saturation;
            }
        }

        public float Value
        {
            set
            {
                this.value = Mathf.Clamp01(value);
            }
            get
            {
                return value;
            }
        }

        public float Alpha
        {
            set
            {
                alpha = Mathf.Clamp01(value);
            }
            get
            {
                return alpha;
            }
        }

        #endregion

        #region Public Functions

        //Should not be necessary as HSVColors are now structs and not classes
        [System.Obsolete("CopyFrom() is deprecated, please use a direct assignment instead.")]
        public void CopyFrom(Color c)
        {
            HSVColor hsv = HSVColor.FromRGB(c);
            hue = hsv.Hue;
            saturation = hsv.Saturation;
            value = hsv.Value;
            alpha = hsv.Alpha;
        }

        public Color ToRGB()
        {
            float r = 0f, g = 0f, b = 0f;

            float h = Hue % 360f;
            float s = Mathf.Clamp01(Saturation);
            float v = Mathf.Clamp01(Value);

            float c = v * s;
            float x = c * (1f - Mathf.Abs(((h / 60f) % 2) - 1f));
            float m = v - c;

            if (h < 60f)
            {
                r = c;
                g = x;
            }
            else if (h < 120f)
            {
                r = x;
                g = c;
            }
            else if (h < 180f)
            {
                g = c;
                b = x;
            }
            else if (h < 240f)
            {
                g = x;
                b = c;
            }
            else if (h < 300f)
            {
                r = x;
                b = c;
            }
            else if (h <= 360f)
            {
                r = c;
                b = x;
            }
            else
            {
                r = 0f;
                g = 0f;
                b = 0f;
            }

            return new Color(r + m, g + m, b + m, alpha);
        }

        public string ToHex(bool includeAlpha = false)
        {
            return ToRGB().ToHex(includeAlpha);
        }

        /// <summary>
        /// Copy color values from an HEX color.
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="customAlpha">Use non-negative values to override the alpha that could come from the hex</param>
        public void FromHex(string hex, float customAlpha = -1)
        {
            this = ColorHelper.FromHex(hex, customAlpha);
        }
        
        public Vector3 ToVector3()
        {
            return new Vector3(hue, saturation, value);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(hue, saturation, value, alpha);
        }

        public override string ToString()
        {
            return string.Format("HSV({0}, {1}, {2}, {3})", hue, saturation, value, alpha);
        }

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
        
        #endregion

        #region Static Accessors

        public static HSVColor white
        {
            get { return new HSVColor(0f, 0f, 1f, 1f); }
        }

        public static HSVColor black
        {
            get { return new HSVColor(0f, 0f, 0f, 1f); }
        }

        public static HSVColor blue
        {
            get { return new HSVColor(240f, 1f, 1f, 1f); }
        }

        public static HSVColor clear
        {
            get { return new HSVColor(0f, 0f, 0f, 0f); }
        }

        public static HSVColor cyan
        {
            get { return new HSVColor(180f, 1f, 1f, 1f); }
        }

        public static HSVColor gray
        {
            get { return new HSVColor(0f, 0f, 50f, 1f); }
        }

        public static HSVColor green
        {
            get { return new HSVColor(120f, 1f, 1f, 1f); }
        }

        public static HSVColor grey
        {
            get { return HSVColor.gray; }
        }

        public static HSVColor magenta
        {
            get { return new HSVColor(300f, 1f, 1f, 1f); }
        }

        public static HSVColor red
        {
            get { return new HSVColor(0f, 1f, 1f, 1f); }
        }

        public static HSVColor yellow
        {
            get { return new HSVColor(60f, 1f, 1f, 1f); }
        }

        #endregion

        #region Static Functions

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

        /// <summary>
        /// Returns the ToHSV values of a RGB hsvColor
        /// </summary>
        /// <param name="r">'Red' component</param>
        /// <param name="g">'Green' component</param>
        /// <param name="b">'Blue' component</param>
        /// <returns></returns>
        public static HSVColor FromRGB(float r, float g, float b, float a = 1f)
        {
            float h, s, v;

            float max = Mathf.Max(Mathf.Max(r, g), b);
            float min = Mathf.Min(Mathf.Min(r, g), b);

            float delta = max - min;

            // value is our max hsvColor
            v = max;


            if (Mathf.Approximately(max, 0f) || Mathf.Approximately(delta, 0f))
            {
                // all colors are white, no saturation and hue is undefined
                s = 0f;
                h = 0f;
                return new HSVColor(h, s, v, a);
            }
            else // saturation is percent of max
                s = delta / max;

            // grayscale image if min and max are the same
            /*if (Mathf.Approximately(min, max))
            {
                v = max;
                s = 0;
                hue = -1;
                return new HSVColor(hue, s, v, a);
            }*/

            //Debug.Log("max: " + max + ". r: " + r + ". g: " + g + ". b: " + b);


            // hue depends which hsvColor is max (this creates a rainbow effect)
            if (Mathf.Approximately(r, max)) // r == max)
                h = ((g - b) / delta) % 6f;            // between yellow & magenta
            else if (Mathf.Approximately(g, max)) //(g == max)
            {
                h = 2f + (b - r) / delta;                // between cyan & yellow
            }
            else
                h = 4f + (r - g) / delta;                // between magenta & cyan


            // turn hue into 0-360 degrees
            h *= 60f;
            if (h < 0f)
                h += 360f;

            /*s *= 255f;
            v *= 255f;*/

            //hue: [0, 360]
            //s, v: [0, 1]
            return new HSVColor(h, s, v, a);
        }
        
        public static HSVColor FromRGB(Color rgbColor)
        {
            return HSVColor.FromRGB(rgbColor.r, rgbColor.g, rgbColor.b, rgbColor.a);
        }

        //http://www.rapidtables.com/convert/hsvColor/rgb-to-hsv.htm
        /// <summary>
        /// Generates a RGB hsvColor using specified hue, saturation, value (brightness) and  hue and customAlpha
        /// </summary>
        /// <param name="hue">[0f, 360f] degrees</param>
        /// <param name="saturation">[0f, 1f]</param>
        /// <param name="value">[0f, 1f]</param>
        /// <param name="customAlpha">[0f, 1f]</param>
        /// <returns>RGB hsvColor</returns>
        public static Color ToRGB(float hue, float saturation, float value, float alpha = 1.0f) //value = brightness
        {
            HSVColor hsvColor = new HSVColor(hue, saturation, value, alpha);
            return hsvColor.ToRGB();
        }
        
        public static Color ToRGB(HSVColor hsvColor)
        {
            return hsvColor.ToRGB();
        }
        
        #endregion

        #region Internal Operators

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
        
        public static implicit operator HSVColor(Color rgb)
        {
            return HSVColor.FromRGB(rgb);
        }

        public static implicit operator Color(HSVColor hsv)
        {
            return HSVColor.ToRGB(hsv);
        }

        public static implicit operator Vector3(HSVColor hsv)
        {
            return hsv.ToVector3();
        }

        public static implicit operator Vector4(HSVColor hsv)
        {
            return hsv.ToVector4();
        }

        /*private static bool IsNull(object obj)
        {
            return object.ReferenceEquals(obj, null);
        }*/


        #endregion

    }

}