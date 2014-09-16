using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic pool to manage the creation and removal of elements in game
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class Pool
{
    /// <summary>
    /// Template that will be used to create new elements
    /// </summary>
    [SerializeField]
    GameObject prefab;

    /// <summary>
    /// Amount of clones that will prefill the poll. Use 0 for no prefill
    /// </summary>
    [SerializeField]
    int prefillQuantity = 0;

    [HideInInspector]
    public Transform parentForPooled;    

    public Transform parentForSpawned;

    
    /// <summary>
    /// References of all spawned elements
    /// </summary>
    List<GameObject> spawned;

    /// <summary>
    /// Elements ready to be spawned
    /// </summary>
    Stack<GameObject> stored;


    public GameObject Prefab
    {
        get
        {
            return prefab;
        }
    }

    public List<GameObject> Spawned
    {
        get
        {
            return spawned;
        }
    }

    public Pool(GameObject prefab)
        : this(prefab, null, null, 0)
    {
    }

    public Pool(GameObject prefab, Transform parentForPooled, Transform parentForSpawned)
        : this(prefab, parentForPooled, parentForSpawned, 0)
    {
    }

    public Pool(GameObject prefab, int prefillQuantity)
        : this(prefab, null, null, prefillQuantity)
    {
    }

    public Pool(GameObject prefab, Transform parentForPooled, Transform parentForSpawned, int prefillQuantity)
    {
        this.prefab = prefab;

        Init();

        this.prefillQuantity = prefillQuantity;

        this.parentForPooled = parentForPooled;
        this.parentForSpawned = parentForSpawned;

        PreFill();
    }

    public void Init()
    {
        this.spawned = new List<GameObject>();
        this.stored = new Stack<GameObject>();
    }

    /// <summary>
    /// Prefills the pool
    /// </summary>
    public void PreFill()
    {
        FillPool(this.prefillQuantity); //, this.parentForSpawned);
    }

    public virtual GameObject Spawn(Vector3 position)
    {
        return Spawn(position, Quaternion.Euler(Vector3.zero));
    }

    public virtual GameObject Spawn(Quaternion rotation)
    {
        return Spawn(Vector3.zero, rotation);
    }

    /// <summary>
    /// Picks an existing element from the pool or creates a new one if the pool is empty
    /// </summary>
    /// <param name="position">Desired spawn position</param>
    /// <param name="customParent">Desired gameobject customParent</param>
    /// <returns></returns>
    public virtual GameObject Spawn(Vector3 position, Quaternion rotation, Transform customParent = null)
    {
        GameObject candidate = null;

        if (stored.Count == 0)
        {
            candidate = GameObject.Instantiate(prefab, position, rotation) as GameObject; //prefab.Clone(position, customParent);

            //candidate.transform.customParent = customParent;
            candidate.SetPosition(position); // transform.position = position;
        }
        else
        {
            candidate = stored.Pop();

            candidate.SetPosition(position);

            candidate.SetActive(true);
        }

        if (customParent != null)
            candidate.transform.parent = customParent;
        else 
            candidate.transform.parent = parentForSpawned;


        this.spawned.Add(candidate);

        return candidate;
    }

    /// <summary>
    /// Creates new elements and stores them in the pool
    /// </summary>
    /// <param name="quantity">Number of elements to fill the pool with</param>
    /// <param name="customParent"></param>
    public void FillPool(int quantity) //, Transform customParent = null)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject o = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject; //prefab.Clone(Vector3.zero, EnemyFactory.Instance.transform);

            o.transform.parent = this.parentForPooled;

            o.gameObject.SetActive(false);
            stored.Push(o);
        }
    }

    /// <summary>
    /// Removes an element from the game and stores it the pool
    /// </summary>
    /// <param name="e">Element to remove</param>
    public void Recycle(GameObject e)
    {
        if (!spawned.Contains(e))
        {
            GameObject.Destroy(e);
            return;
        }

        e.SetActive(false);
        e.transform.parent = this.parentForPooled;

        spawned.Remove(e);
        stored.Push(e);
    }


    /// <summary>
    /// Empties the pool
    /// </summary>
    public void Flush()
    {
        stored.Clear();
    }
}