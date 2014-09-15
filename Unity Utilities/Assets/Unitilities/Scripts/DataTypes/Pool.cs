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
    /// Whether the pool should have previously created elements at the start of the game
    /// </summary>
    [SerializeField]
    bool prefillPool = false;

    /// <summary>
    /// Amount of clones that will prefill the poll
    /// </summary>
    [SerializeField]
    int prefillQuantity = 1;

    [SerializeField]
    Transform spawnedParent;


    /// <summary>
    /// References of all spawned elements
    /// </summary>
    protected List<GameObject> spawned;

    /// <summary>
    /// Elements ready to be spawned
    /// </summary>
    Stack<GameObject> availablePool;

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
        : this(prefab, null, false, 0)
    {
    }

    public Pool(GameObject prefab, Transform parentTransform)
        : this(prefab, parentTransform, false, 0)
    {
    }

    public Pool(GameObject prefab, bool prefill, int prefillQuantity)
        : this(prefab, null, prefill, prefillQuantity)
    {
    }

    public Pool(GameObject prefab, Transform parentTransform, bool prefill, int prefillQuantity)
    {
        this.prefab = prefab;

        this.spawned = new List<GameObject>();
        this.availablePool = new Stack<GameObject>();

        this.prefillPool = prefill;
        this.prefillQuantity = prefillQuantity;

        this.spawnedParent = parentTransform;

        Init();
    }


    /// <summary>
    /// Prefills the pool
    /// </summary>
    public void Init()
    {
        /*this.spawned = new List<GameObject>();
        this.availablePool = new Stack<GameObject>();*/

        if (this.prefillPool)
        {
            FillPool(this.prefillQuantity); //, this.spawnedParent);
        }
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
    /// Picks an existing element from the pool or creates a new one
    /// if the pool is empty
    /// </summary>
    /// <param name="position">Desired spawn position</param>
    /// <param name="parent">Desired gameobject parent</param>
    /// <returns></returns>
    public virtual GameObject Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
    {
        GameObject candidate = null;

        if (availablePool.Count == 0)
        {
            candidate = GameObject.Instantiate(prefab, position, rotation) as GameObject; //prefab.Clone(position, parent);

            //candidate.transform.parent = parent;
            candidate.transform.position = position;
        }
        else
        {
            candidate = availablePool.Pop();
            candidate.gameObject.SetPosition(position);
            candidate.gameObject.SetActive(true);
        }

        if (parent == null)
            candidate.transform.parent = this.spawnedParent;
        else
            candidate.transform.parent = parent;

        this.spawned.Add(candidate);

        return candidate;
    }

    /// <summary>
    /// Creates new elements and stores them in the pool
    /// </summary>
    /// <param name="quantity">Number of elements to fill the pool with</param>
    /// <param name="parent"></param>
    public void FillPool(int quantity) //, Transform parent = null)
    {
        for (int i = 0; i < quantity; i++)
        {
            GameObject o = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0)) as GameObject; //prefab.Clone(Vector3.zero, EnemyFactory.Instance.transform);

            o.transform.parent = this.spawnedParent;

            o.gameObject.SetActive(false);
            availablePool.Push(o);
        }
    }

    /// <summary>
    /// Removes an element from the game and stores it the pool
    /// </summary>
    /// <param name="e">Element to remove</param>
    public void Recycle(GameObject e)
    {
        if (!spawned.Contains(e))
            return;

        e.gameObject.SetActive(false);
        spawned.Remove(e);
        availablePool.Push(e);
    }

    /// <summary>
    /// Empties the pool
    /// </summary>
    public void Flush()
    {
        availablePool.Clear();
    }
}