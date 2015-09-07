/// <summary>
/// ColorHelper v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Set of useful mathematical functions.
/// </summary>

using UnityEngine;

namespace Unitilities
{

    public static class MathHelper
    {

        #region Trigonometry

        /// <summary>
        /// Calculates a position over a sphere
        /// </summary>
        /// <param name="center">Position of the center of the sphere</param>
        /// <param name="_radius">Radius of the sphere</param>
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

        public static Vector2 FindMirror(Vector2 _origin, Vector2 _direction, Vector2 _pointA)
        {
            float s = 0f; //Scalar projection

            /*float angle = Mathf.Atan2(_pointA.y, _pointA.x) - Mathf.Atan2(_direction.y, _direction.x);
            float lengthA = _pointA.magnitude;
            s = lengthA * Mathf.Cos(angle);*/
            //We use the vector projection formula

            //An alternate way is by using the dot product, in which case
            //  the following code of line substitutes the last three.
            s = Vector3.Dot(_pointA, _direction);

            //The projection of point A over _direction
            Vector2 projA = _origin + _direction * s;

            //The rejection of point A from _direction
            Vector2 rejA = _pointA - projA;

            //We want the opposite, as it's the rejection of point B from "_direction"
            rejA *= -1;

            //We return the symmetric point of point A over _direction
            return projA + rejA;
        }

        public static Vector2[] GenerateMirrorPair(Vector2 _origin, Vector2 _direction, float _maxDistanceInLine, float _maxDistanceFromLine)
        {
            return MathHelper.GenerateMirrorPair(_origin, _direction, UnityEngine.Random.Range(0f, _maxDistanceInLine), UnityEngine.Random.Range(0f, _maxDistanceFromLine), UnityEngine.Random.Range(0f, 1f) <= 0.5f, UnityEngine.Random.Range(0f, 1f) <= 0.5f);
        }

        public static Vector2[] GenerateMirrorPair(Vector2 _origin, Vector2 _direction, float _distanceInLine, float _distanceFromLine, bool _goAgainstDirection, bool _swapHemispheres)
        {
            Vector2[] points = new Vector2[2];

            //This sets the max distance along the line where both points can be generated
            //float maxDistance = maxD; //[This is just an example]

            //Length of the projection vector
            float distanceInLine = _distanceInLine; // Rnd.Range(0f, maxDistance);

            //Length of the rejection vector
            float distanceFromLine = _distanceFromLine; // Rnd.Range(0f, maxDistance);

            Vector2 projection = _direction;

            //The projection can go with or against the _direction
            if (_goAgainstDirection) // Rnd.Range(0f, 1f) <= 0.5f)
            {
                projection *= -1;
            }

            //Given the projection vector, we calculate the future rejection vector
            Vector2 rejection = projection.PerpendicularRight(); // Perpendicular(projection);

            rejection *= distanceFromLine;

            if (_goAgainstDirection) // !_swapHemispheres && // Rnd.Range(0f, 1f) <= 0.5f)
            {
                rejection *= -1f;
            }

            projection *= distanceInLine;
            projection += _origin;

            if (_swapHemispheres)
            {
                points[0] = projection - rejection;
                points[1] = projection + rejection;
            }
            else
            {
                points[0] = projection + rejection;
                points[1] = projection - rejection;
            }

            return points;
        }

        public static Vector2[] GenerateMirrorPairRadially(Vector2 _origin, Vector2 _direction, float _maxRadius)
        {
            return MathHelper.GenerateMirrorPairRadially(_origin, _direction, UnityEngine.Random.Range(1f, _maxRadius), UnityEngine.Random.Range(0f, 180f));
        }

        public static Vector2[] GenerateMirrorPairRadially(Vector2 _origin, Vector2 _direction, float _radius, float _deltaAngle)
        {
            Vector2[] points = new Vector2[2];

            float x, y;

            //Distance away from the _origin (i.e. the _radius of the circle)
            float distance = _radius; //Rnd.Range(0f, float.MaxValue);

            //Angle of the line used as a mirror
            float lineAngle = Mathf.Atan2(_direction.y, _direction.x);

            //Variation to be applied to the line angle
            //  Max value is 180 degrees because the point over the 
            //  other hemisphere will be calculated using this one
            float deltaAngle = _deltaAngle; // Rnd.Range(0f, 180f);

            //The deltaAngle in radians
            float deltaAngleR;

            //We generate the first point
            deltaAngleR = lineAngle + (deltaAngle * Mathf.Deg2Rad);

            //Basic position over a circle formula
            x = _origin.x + Mathf.Cos(deltaAngleR) * distance;
            y = _origin.y + Mathf.Sin(deltaAngleR) * distance;

            points[0] = new Vector2(x, y);

            //The mirror point's angular distance from the 360 degrees
            //is equal to the distance between the original point and 0 degrees
            deltaAngleR = lineAngle + ((360f - deltaAngle) * Mathf.Deg2Rad);

            x = _origin.x + Mathf.Cos(deltaAngleR) * distance;
            y = _origin.y + Mathf.Sin(deltaAngleR) * distance;

            points[1] = new Vector2(x, y);

            return points;
        }

        #endregion


        #region Indexes

        //Left-to-right
        public static int LinearIndex(int column, int row, int width)
        {
            return column + (row * width);
        }

        //Top-to-bottom
        public static int LinearIndexAlt(int column, int row, int height)
        {
            return row + (column * height);
        }

        public static Unitilities.Tuples.TupleI GridIndexes(int index, int width)
        {
            return new Unitilities.Tuples.TupleI(index % width, index / width);
        }

        public static Unitilities.Tuples.TupleI GridIndexesAlt(int index, int height)
        {
            return new Unitilities.Tuples.TupleI(index / height, index % height);
        }

        #endregion

    }

}