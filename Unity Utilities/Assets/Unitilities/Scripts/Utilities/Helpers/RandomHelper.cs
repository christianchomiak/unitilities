using UnityEngine;
using System.Collections;

public class RandomHelper
{
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

    /// <summary>
    /// Generates a random RGB color
    /// </summary>
    /// <returns>RGB color</returns>
    public static Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }
}
