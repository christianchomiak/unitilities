using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager> 
{
    /// <summary>
    /// If true, when recycling an object also remove its association with the customParent pool.
    /// </summary>
    [SerializeField]
    bool forceRecycleCleanup = false;

    /// <summary>
    /// If true, creates a new empty child for each pool
    /// </summary>
    [SerializeField]
    bool createChildForPools = true;

    /// <summary>
    /// Default amount of clones to pre-store when a new pool is created
    /// </summary>
    [SerializeField]
    int defaultPrefillQuantity = 0;

    [SerializeField]
    List<Pool> pools;
    
    /// <summary>
    /// Each spawned GameObject and the pool that created it
    /// </summary>
    Dictionary<GameObject, Pool> relObjectPool;

    /// <summary>
    /// Each GameObject template and the pool that creates clones from it
    /// </summary>
    Dictionary<GameObject, Pool> relTemplatePool;
    

    protected override void Awake()
    {
        base.Awake();

        if (pools == null)
            pools = new List<Pool>();
        
        relTemplatePool = new Dictionary<GameObject, Pool>();
        relObjectPool = new Dictionary<GameObject, Pool>();

        InitializePools();
    }

    private void InitializePools()
    {
        pools.RemoveAll(p => p != null && p.Prefab == null);

        for (int i = 0; i < pools.Count; i++)
        {
            pools[i].Init();
            if (pools[i].parentForPooled == null)
            {
                if (createChildForPools)
                {
                    GameObject o = new GameObject("[Pool: " + pools[i].Prefab.name + "]");
                    o.transform.parent = this.transform;
                    pools[i].parentForPooled = o.transform;
                }
                else
                    pools[i].parentForPooled = this.transform;
            }

            pools[i].PreFill();
        }

        MaintainPools();
    }

    private void MaintainPools()
    {
        relTemplatePool.Clear();

        pools.RemoveAll(p => p == null);

        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i] != null)
            {
                relTemplatePool.Add(pools[i].Prefab, pools[i]);
            }
        }
    }

    #if UNITY_EDITOR

    void LateUpdate()
    {
        if (pools.Count != relTemplatePool.Count)
        {
            MaintainPools();
        }
    }

    #endif

    
    public void ClearPools()
    {
        pools.Clear();
        relTemplatePool.Clear();
        relObjectPool.Clear();
    }


    /// <summary>
    /// Creates a new clone of the prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab)
    {
        return Spawn(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero), null);
    }


    /// <summary>
    /// Creates a new clone of the prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Quaternion rotation)
    {
        return Spawn(prefab, Vector3.zero, rotation, null);
    }

    /// <summary>
    /// Creates a new clone of the prefab 
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Vector3 position)
    {
        return Spawn(prefab, position, Quaternion.Euler(Vector3.zero), null);
    }

    /// <summary>
    /// Creates a new clone of the prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="customParent"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Transform customParent)
    {
        return Spawn(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero), customParent);
    }


    /// <summary>
    /// Creates a new clone of the prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        return Spawn(prefab, position, rotation, null);
    }

    /// <summary>
    /// Creates a new clone of the prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="customParent"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Vector3 position, Transform customParent)
    {
        return Spawn(prefab, position, Quaternion.Euler(Vector3.zero), customParent);
    }

    /// <summary>
    /// Creates a new clone of the prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="rotation"></param>
    /// <param name="customParent"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Quaternion rotation, Transform customParent)
    {
        return Spawn(prefab, Vector3.zero, rotation, customParent);
    }


    /// <summary>
    /// Creates a new clone of the prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="position"></param>
    /// <param name="rotation"></param>
    /// <param name="customParent"></param>
    /// <returns></returns>
    public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform customParent)
    {
        if (prefab == null)
            return null;


        Pool pool = null;
        GameObject newObject = null;


        //Check if the prefab has already been registered
        /*if (relTemplatePool.TryGetValue(prefab, out pool))
        {
            if (pool != null) //The prefab existed but its pool doesn't
            {
                pool = new Pool(prefab);
                pool.PreFill();

                //Register the new pool
                pools.Add(pool);

                //Update the association of the existing prefab with its new pool
                relTemplatePool[prefab] = pool;
            }
        }
        else //There's no record of that prefab
        {
            pool = new Pool(prefab);
            pool.PreFill();

            //Register the new pool
            pools.Add(pool);

            //Create the association of the existing prefab with its pool
            relTemplatePool.Add(prefab, pool);
        }*/

        pool = GetOrCreatePool(prefab, customParent, defaultPrefillQuantity);

        newObject = pool.Spawn(position, rotation, customParent);
        relObjectPool.Add(newObject, pool);

        return newObject;
    }
    
    /// <summary>
    /// Removes a GameObject from play and stores it in its corresponding pool (if it applies)
    /// </summary>
    /// <param name="go"></param>
    public void Recycle(GameObject go)
    {
        Pool parentPool = null;

        if (relObjectPool.TryGetValue(go, out parentPool))
        {
            if (parentPool != null)
            {
                go.transform.parent = this.transform;

                if (forceRecycleCleanup)
                    relObjectPool.Remove(go);

                parentPool.Recycle(go);

                return;
            }
            else
            {
                relObjectPool.Remove(go);
            }
        }

        Destroy(go);
    }
    
    /// <summary>
    /// Given a prefab, returns all spawned clones
    /// </summary>
    /// <param name="prefab"></param>
    /// <returns></returns>
    public List<GameObject> GetAllSpawnedFrom(GameObject prefab)
    {
        List<GameObject> allSpawned = new List<GameObject>();

        Pool parentPool = null;
        if (relObjectPool.TryGetValue(prefab, out parentPool))
        {
            if (parentPool != null)
            {
                return parentPool.Spawned;
            }
        }

        return allSpawned;
    }

    /// <summary>
    /// Returns the pool associated with a prefab.
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parentForSpawned"></param>
    /// <param name="prefillQuantity"></param>
    /// <returns></returns>
    private Pool GetOrCreatePool(GameObject prefab, Transform parentForSpawned, int prefillQuantity)
    {
        Pool pool = null;

        bool prefabExistsInRecords = relTemplatePool.ContainsKey(prefab);

        if (prefabExistsInRecords && relTemplatePool[prefab] != null)
            return relTemplatePool[prefab];


        Transform parentForPooled = null;
        if (createChildForPools)
        {
            GameObject o = new GameObject("[Pool: " + prefab.name + "]");
            o.transform.parent = this.transform;
            parentForPooled = o.transform;
        }
        else
            parentForPooled = this.transform;

        pool = new Pool(prefab, parentForPooled, parentForSpawned, prefillQuantity);

        //pool.PreFill();
        pools.Add(pool);

        if (!prefabExistsInRecords)
        {
            relTemplatePool.Add(prefab, pool);
        }
        else
        {
            relTemplatePool[prefab] = pool;
        }

        return pool;
    }

    /// <summary>
    /// Creates a new pool for the selected prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parentForSpawned"></param>
    /// <param name="prefillQuantity"></param>
    public void CreatePool(GameObject prefab, Transform parentForSpawned, int prefillQuantity)
    {
        GetOrCreatePool(prefab, parentForSpawned, prefillQuantity);
    }

    /// <summary>
    /// Creates a new pool for the selected prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="prefillQuantity"></param>
    public void CreatePool(GameObject prefab, int prefillQuantity)
    {
        GetOrCreatePool(prefab, null, prefillQuantity);
    }

    /// <summary>
    /// Creates a new pool for the selected prefab
    /// </summary>
    /// <param name="prefab"></param>
    /// <param name="parentForSpawned"></param>
    public void CreatePool(GameObject prefab, Transform parentForSpawned)
    {
        GetOrCreatePool(prefab, parentForSpawned, defaultPrefillQuantity);
    }

    /// <summary>
    /// Creates a new pool for the selected prefab
    /// </summary>
    /// <param name="prefab"></param>
    public void CreatePool(GameObject prefab)
    {
        GetOrCreatePool(prefab, null, defaultPrefillQuantity);
    }

}

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
    /// <param name="prefab"></param>
    public static void Recycle(this GameObject go)
    {
        PoolManager.Instance.Recycle(go);
    }
}
