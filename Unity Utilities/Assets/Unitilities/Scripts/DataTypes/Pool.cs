using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Generic pool to manage the creation and removal of elements in game
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class Pool<T> where T : MonoBehaviour
{
    /// <summary>
    /// Template that will be used to create new elements
    /// </summary>
    [SerializeField]
    T prefab;

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

    /// <summary>
    /// References of all spawned elements
    /// </summary>
    protected List<T> spawned;

    /// <summary>
    /// Elements ready to be spawned
    /// </summary>
    Stack<T> availablePool;

    public T Prefab
    {
        get
        {
            return prefab;
        }
    }

    public Pool(T prefab)
    {
        this.prefab = prefab;
        //this.probability = probability;

        this.spawned = new List<T>();
        this.availablePool = new Stack<T>();
    }

    /// <summary>
    /// Prefills the pool
    /// </summary>
    /// <param name="parent">Parent object of all spawned elements</param>
    public void Init(Transform parent = null)
    {
        this.spawned = new List<T>();
        this.availablePool = new Stack<T>();

        if (this.prefillPool)
        {
            FillPool(prefillQuantity, parent);
        }
    }

    /// <summary>
    /// Picks an existing element from the pool or creates a new one
    /// if the pool is empty
    /// </summary>
    /// <param name="position">Desired spawn position</param>
    /// <param name="parent">Desired gameobject parent</param>
    /// <returns></returns>
    public virtual T Spawn(Vector3 position, Transform parent = null)
    {
        T candidate = null;

        if (availablePool.Count == 0)
        {
            candidate = GameObject.Instantiate(prefab, position, Quaternion.Euler(0, 0, 0)) as T; //prefab.Clone(position, parent);

            candidate.transform.parent = parent;
            candidate.transform.position = position;
        }
        else
        {
            candidate = availablePool.Pop();
            candidate.gameObject.SetPosition(position);
            candidate.gameObject.SetActive(true);
        }

        this.spawned.Add(candidate);

        return candidate;
    }

    /// <summary>
    /// Creates new elements and stores them in the pool
    /// </summary>
    /// <param name="quantity">Number of elements to fill the pool with</param>
    /// <param name="parent"></param>
    public void FillPool(int quantity, Transform parent = null)
    {
        for (int i = 0; i < quantity; i++)
        {
            T o = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.Euler(0, 0, 0)) as T; //prefab.Clone(Vector3.zero, EnemyFactory.Instance.transform);

            o.transform.parent = parent;

            o.gameObject.SetActive(false);
            availablePool.Push(o);
        }
    }

    /// <summary>
    /// Removes an element from the game and stores it the pool
    /// </summary>
    /// <param name="e">Element to remove</param>
    public void Withdraw(T e)
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