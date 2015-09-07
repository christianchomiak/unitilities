/// <summary>
/// Pool v1.1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Structure that holds blueprints of gameobjects and handles
/// the spawned and recycled objects.
/// 
/// Useful to avoid using Instantiate/Destroy, functions that
/// can have an impact on the performance of the game if used
/// repeteadly.
/// 
/// Includes:
///     * Spawning new GameObjects
///     * Recycling old GameObjects
///     * Pre-filling the pool
///     * Flushing of the pool
/// </summary>

using UnityEngine;
using System.Collections.Generic;

namespace Unitilities.Pools
{
    /// <summary>
    /// Generic pool to manage the creation and removal of elements in game
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [System.Serializable]
    public class Pool
    {
        #region Variables

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

        #endregion

        #region Accessors

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

        #endregion

        #region Constructors

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

        #endregion

        #region Public Functions

        public void Init()
        {
            spawned = new List<GameObject>();
            stored = new Stack<GameObject>();
        }

        /// <summary>
        /// Prefills the pool
        /// </summary>
        public void PreFill()
        {
            FillPool(prefillQuantity); //, this.parentForSpawned);
        }

        public virtual GameObject Spawn()
        {
            return Spawn(Vector3.zero);
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
                candidate.transform.SetParent(customParent, false);// parent = customParent;
            else
                candidate.transform.SetParent(parentForSpawned, false);//parent = parentForSpawned;


            spawned.Add(candidate);

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
                GameObject o = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.Euler(Vector3.zero)) as GameObject; //prefab.Clone(Vector3.white, EnemyFactory.Instance.transform);

                o.transform.SetParent(parentForPooled, false); //parent = this.parentForPooled;

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

            ResetInstanceTransform(e);
            
            /*e.transform.parent = null;
            e.transform.position = prefab.transform.position;
            e.transform.localScale = prefab.transform.localScale;
            e.transform.rotation = prefab.transform.rotation;*/

            e.SetActive(false);
            e.transform.SetParent(parentForPooled, false); // parent = this.parentForPooled;

            spawned.Remove(e);
            stored.Push(e);
        }

        /// <summary>
        /// Empties the pool
        /// </summary>
        public void Flush(bool destroyStoredElements = false)
        {
            if (destroyStoredElements)
            {
                foreach (GameObject go in stored)
                {
                    if (go != null)
                        GameObject.Destroy(go);
                }
            }

            stored.Clear();
        }

        /// <summary>
        /// Resets the Transform values of the GameObject to the ones of its blueprint
        /// </summary>
        /// <param name="go">Element to remove</param>
        public void ResetInstanceTransform(GameObject go)
        {
            if ((object) go == null)
                return;

            go.transform.parent = null;
            go.transform.position = prefab.transform.position;
            go.transform.localScale = prefab.transform.localScale;
            go.transform.rotation = prefab.transform.rotation;
        }

        #endregion
    }

}