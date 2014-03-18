using UnityEngine;
using System.Collections;

public class MathHelper
{

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
        return new TupleI(index / height, index % height);
    }

    #endregion

}
