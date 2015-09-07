/// <summary>
/// PoolExtensions v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Group of extensions that facilitate working with the provided Pool class.
/// </summary>

using UnityEngine;

namespace Unitilities.Pools
{

    public static class PoolExtensions
    {
        /// <summary>
        /// Creates a new pool using this GameObject as its template
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parentForSpawned"></param>
        /// <param name="prefillQuantity"></param>
        public static void CreatePool(this GameObject prefab, Transform parentForSpawned, int prefillQuantity)
        {
            PoolManager.Instance.CreatePool(prefab, parentForSpawned, prefillQuantity);
        }

        /// <summary>
        /// Creates a new pool using this GameObject as its template
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="prefillQuantity"></param>
        public static void CreatePool(this GameObject prefab, int prefillQuantity)
        {
            PoolManager.Instance.CreatePool(prefab, prefillQuantity);
        }

        /// <summary>
        /// Creates a new pool using this GameObject as its template
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="parentForSpawned"></param>
        public static void CreatePool(this GameObject prefab, Transform parentForSpawned)
        {
            PoolManager.Instance.CreatePool(prefab, parentForSpawned);
        }

        /// <summary>
        /// Creates a new pool using this GameObject as its template
        /// </summary>
        /// <param name="prefab"></param>
        public static void CreatePool(this GameObject prefab)
        {
            PoolManager.Instance.CreatePool(prefab);
        }


        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab)
        {
            return PoolManager.Instance.Spawn(prefab);
        }

        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position)
        {
            return PoolManager.Instance.Spawn(prefab, position);
        }

        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Quaternion rotation)
        {
            return PoolManager.Instance.Spawn(prefab, rotation);
        }

        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="customParent"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Transform customParent)
        {
            return PoolManager.Instance.Spawn(prefab, customParent);
        }


        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return PoolManager.Instance.Spawn(prefab, position, rotation);
        }

        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="customParent"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position, Transform customParent)
        {
            return PoolManager.Instance.Spawn(prefab, position, customParent);
        }

        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="rotation"></param>
        /// <param name="customParent"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Quaternion rotation, Transform customParent)
        {
            return PoolManager.Instance.Spawn(prefab, rotation, customParent);
        }

        /// <summary>
        /// Creates a new instance of the Game Object, managed by a pool
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="position"></param>
        /// <param name="rotation"></param>
        /// <param name="customParent"></param>
        /// <returns></returns>
        public static GameObject Spawn(this GameObject prefab, Vector3 position, Quaternion rotation, Transform customParent)
        {
            return PoolManager.Instance.Spawn(prefab, position, rotation, customParent);
        }

        /// <summary>
        /// Removes this gameobject from play
        /// </summary>
        /// <param name="go"></param>
        public static void Recycle(this GameObject go)
        {
            PoolManager.Instance.Recycle(go);
        }

        /// <summary>
        /// [For pooled GameObjects]
        /// Resets the Transform values of the GameObject to the one of its blueprint.
        /// </summary>
        /// <param name="go">GameObject to reset</param>
        public static void ResetInstanceTransform(this GameObject go)
        {
            PoolManager.Instance.ResetInstanceTransform(go);
        }
    }

}