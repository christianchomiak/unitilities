using UnityEngine;
using System.Collections;

public static class VectorExtensions
{
    #region Vector2

    /// <summary>
    /// Returns a copy of a vector with a new X field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="x">X field of the new vector</param>
    /// <returns></returns>
    public static Vector2 WithX(this Vector2 v, float x)
    {
        return new Vector2(x, v.y);
    }

    /// <summary>
    /// Returns a copy of a vector with a new Y field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="y">Y field of the new vector</param>
    public static Vector2 WithY(this Vector2 v, float y)
    {
        return new Vector2(v.x, y);
    }

    #endregion


    #region Vector3

    /// <summary>
    /// Returns a copy of a vector with a new X field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="x">X field of the new vector</param>
    public static Vector3 WithX(this Vector3 v, float x)
    {
        return new Vector3(x, v.y, v.z);
    }

    public static Vector3 WithY(this Vector3 v, float y)
    {
        return new Vector3(v.x, y, v.z);
    }

    /// <summary>
    /// Returns a copy of a vector with a new Z field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="z">Z field of the new vector</param>
    public static Vector3 WithZ(this Vector3 v, float z)
    {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    #endregion


    #region Vector4

    /// <summary>
    /// Returns a copy of a vector with a new X field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="x">X field of the new vector</param>
    public static Vector4 WithX(this Vector4 v, float x)
    {
        return new Vector4(x, v.y, v.z);
    }

    /// <summary>
    /// Returns a copy of a vector with a new Y field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="y">Y field of the new vector</param>
    public static Vector4 WithY(this Vector4 v, float y)
    {
        return new Vector4(v.x, y, v.z);
    }

    /// <summary>
    /// Returns a copy of a vector with a new Z field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="z">Z field of the new vector</param>
    public static Vector4 WithZ(this Vector4 v, float z)
    {
        return new Vector4(v.x, v.y, z);
    }

    /// <summary>
    /// Returns a copy of a vector with a new W field
    /// </summary>
    /// <param name="v">Original vector</param>
    /// <param name="w">W field of the new vector</param>
    public static Vector4 WithW(this Vector4 v, float w)
    {
        return new Vector4(v.x, v.y, v.z, w);
    }

    public static Vector3 ToVector3(this Vector4 v)
    {
        return new Vector3(v.x, v.y, v.z);
    }

    #endregion
}
