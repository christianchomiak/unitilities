/// <summary>
/// RandomHelper v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Useful functions for random labors.
/// </summary>

using UnityEngine;

namespace Unitilities
{
    public static class RandomHelper
    {
        #region int
        /// <summary>
        /// Returns 1 or -1 at random.
        /// </summary>
        /// <returns>1 or -1</returns>
        public static int RandomSign()
        {
            int sign = UnityEngine.Random.value > 0.5f ? 1 : -1;
            return sign;
        }
        #endregion

        #region bool

        /// <summary>
        /// 
        /// </summary>
        /// <param name="probabilityOfFalse"></param>
        /// <returns></returns>
        public static bool RandomBool(float probabilityOfFalse = 0.5f)
        {
            bool b = UnityEngine.Random.value >= probabilityOfFalse ? true : false;
            return b;
        }

        #endregion

        #region Vectors

        /// <summary>
        /// Get a random and normalized Vector2
        /// </summary>
        /// <returns></returns>
        public static Vector2 RndNormV2()
        {
            //Rnd.value: random number between 0.0 [inclusive] and 1.0 [inclusive] 
            int signX = UnityEngine.Random.value > 0.5f ? 1 : -1;
            int signY = UnityEngine.Random.value > 0.5f ? 1 : -1;
            Vector2 randomVector = new Vector2(signX * UnityEngine.Random.value,
                                                signY * UnityEngine.Random.value);

            return randomVector.normalized;
        }

        /// <summary>
        /// Get a random and normalized Vector3
        /// </summary>
        /// <returns></returns>
        public static Vector3 RndNormV3()
        {
            //Rnd.value: random number between 0.0 [inclusive] and 1.0 [inclusive] 
            int signX = UnityEngine.Random.value > 0.5f ? 1 : -1;
            int signY = UnityEngine.Random.value > 0.5f ? 1 : -1;
            int signZ = UnityEngine.Random.value > 0.5f ? 1 : -1;
            Vector3 randomVector = new Vector3(signX * UnityEngine.Random.value,
                                                signY * UnityEngine.Random.value,
                                                signZ * UnityEngine.Random.value);
            
            return randomVector.normalized;
        }

        #endregion

    }

}