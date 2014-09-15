using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PoolManager : Singleton<PoolManager> 
{
    [SerializeField]
    List<Pool> pools;
    
    /// <summary>
    /// Each spawned GameObject and the pool that created it
    /// </summary>
    Dictionary<GameObject, Pool> parentPools;

    /// <summary>
    /// Each GameObject template and the pool that creates clones from it
    /// </summary>
    Dictionary<GameObject, Pool> templatesPools;
    

    protected override void Awake()
    {
        base.Awake();

        if (pools == null)
            pools = new List<Pool>();
        
        templatesPools = new Dictionary<GameObject, Pool>();
        parentPools = new Dictionary<GameObject, Pool>();
        
        MaintainPools();
    }

    protected void MaintainPools()
    {
        templatesPools.Clear();

        pools.RemoveAll(p => pools == null);

        for (int i = 0; i < pools.Count; i++)
        {
            if (pools[i] != null)
            {
                templatesPools.Add(pools[i].Prefab, pools[i]);
            }
        }
    }

    #if UNITY_EDITOR

    void LateUpdate()
    {
        if (pools.Count != templatesPools.Count)
        {
            MaintainPools();
        }
    }

    #endif

    
    public void ClearPools()
    {
        pools.Clear();
        templatesPools.Clear();
        parentPools.Clear();
    }


    public GameObject Spawn(GameObject prefab, Vector3 position)
    {
        Pool pool = null;
        GameObject newObject = null;
        Pool newPool = null;

        if (templatesPools.TryGetValue(prefab, out pool))
        {
            if (pool != null)
            {
                newObject = pool.Spawn(position);
                parentPools.Add(newObject, pool);
                return newObject;
            }
            else
            {
                newPool = new Pool(prefab);
                newPool.Init();
                
                newObject = newPool.Spawn(position);

                parentPools[prefab] = newPool;
            }
        }
        else
        {
            newPool = new Pool(prefab);
            newPool.Init();

            newObject = newPool.Spawn(position);

            //Associate the new spawned object with its creator pool
            parentPools.Add(newObject, newPool);
        }


        //Register the new pool
        pools.Add(newPool);

        //Associate the new pool with its template
        templatesPools.Add(prefab, newPool);  

        return newObject;
    }
    
    public void Recycle(GameObject go)
    {
        Pool parentPool = null;

        if (parentPools.TryGetValue(go, out parentPool))
        {
            if (parentPool != null)
            {
                go.transform.parent = this.transform;

                parentPools.Remove(go);

                parentPool.Recycle(go);
                return;
            }
            else
            {
                parentPools.Remove(go);
            }
        }

        Destroy(go);
    }
    
    public List<GameObject> GetAllSpawnedFrom(GameObject prefab)
    {
        List<GameObject> allSpawned = new List<GameObject>();

        Pool parentPool = null;
        if (parentPools.TryGetValue(prefab, out parentPool))
        {
            if (parentPool != null)
            {
                return parentPool.Spawned;
            }
        }

        return allSpawned;
    }

}
