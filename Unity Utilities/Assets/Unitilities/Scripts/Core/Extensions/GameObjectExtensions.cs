/// <summary>
/// GameObjectExtensions v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Functions that facilitate the use of GameObjects
/// </summary>

using UnityEngine;
using System.Collections.Generic;

namespace Unitilities
{

    public static class GameObjectExtensions
    {
        #region Recursive Modifiers

        /// <summary>
        /// Sets value to ALL colliders (including the children's)
        /// </summary>
        /// <param name="gameObject">Root object</param>
        /// <param name="tf">Value to be set</param>
        public static void SetCollisionRecursively(this GameObject gameObject, bool tf)
        {
            Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
            foreach (Collider collider in colliders)
                collider.enabled = tf;
        }

        /// <summary>
        /// Sets value to ALL renderers (including the children's)
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="tf">Value to be set</param>
        public static void SetVisualRecursively(this GameObject gameObject, bool tf)
        {
            Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
                renderer.enabled = tf;
        }
        #endregion


        #region Recursive Getters

        /// <summary>
        /// Gets all the components of the gameobject, including its children's, if the tag is correct
        /// </summary>
        /// <typeparam name="T">Type of the component to look for</typeparam>
        /// <param name="gameObject">Base GameObject</param>
        /// <param name="tag">Tag that must match</param>
        /// <returns>All components found</returns>
        public static T[] GetComponentsInChildrenWithTag<T>(this GameObject gameObject, string tag)
            where T : Component
        {
            List<T> results = new List<T>();

            if (gameObject.CompareTag(tag))
                results.Add(gameObject.GetComponent<T>());

            foreach (Transform t in gameObject.transform)
                results.AddRange(t.gameObject.GetComponentsInChildrenWithTag<T>(tag));

            return results.ToArray();
        }

        /// <summary>
        /// Gets the first component of the gameobject, including its children's, that has a certain tag
        /// </summary>
        /// <typeparam name="T">Type of the component to look for</typeparam>
        /// <param name="gameObject">Base GameObject</param>
        /// <param name="tag">Tag that must match</param>
        /// <returns>First component found</returns>
        public static T GetComponentInChildrenWithTag<T>(this GameObject gameObject, string tag)
            where T : Component
        {

            if (gameObject.CompareTag(tag))
                return gameObject.GetComponent<T>();

            foreach (Transform t in gameObject.transform)
            {
                T comp = t.gameObject.GetComponentInChildrenWithTag<T>(tag);
                if (comp != null)
                    return comp;
            }

            return null;
        }

        /// <summary>
        /// Gets the first components of the gameobject, including its ancestors'
        /// </summary>
        /// <typeparam name="T">Type of the component to look for</typeparam>
        /// <param name="gameObject">Base GameObject</param>
        /// <returns>First component found</returns>
        public static T GetComponentInParents<T>(this GameObject gameObject)
            where T : Component
        {
            for (Transform t = gameObject.transform; t != null; t = t.parent)
            {
                T result = t.GetComponent<T>();
                if (result != null)
                    return result;
            }

            return null;
        }

        /// <summary>
        /// Gets all the components of the gameobject, including its ancestors', if the tag is correct
        /// </summary>
        /// <typeparam name="T">Type of the component to look for</typeparam>
        /// <param name="gameObject">Base GameObject</param>
        /// <returns>All components found</returns>
        public static T[] GetComponentsInParents<T>(this GameObject gameObject)
            where T : Component
        {
            List<T> results = new List<T>();
            for (Transform t = gameObject.transform; t != null; t = t.parent)
            {
                T result = t.GetComponent<T>();
                if (result != null)
                    results.Add(result);
            }

            return results.ToArray();
        }

        /// <summary>
        /// Gets all the components of the gameobject, including its ancestors', if the tag is correct
        /// </summary>
        /// <typeparam name="T">Type of the component to look for</typeparam>
        /// <param name="gameObject">Base GameObject</param>
        /// <returns>All components found</returns>
        public static T[] GetComponentsInParentsWithTag<T>(this GameObject gameObject, string tag)
        where T : Component
        {
            List<T> results = new List<T>();

            if (gameObject.CompareTag(tag))
                results.Add(gameObject.GetComponent<T>());

            foreach (Transform t in gameObject.transform)
                results.AddRange(t.gameObject.GetComponentsInParentsWithTag<T>(tag));

            return results.ToArray();
        }

        #endregion


        #region Layers

        /// <summary>
        /// Set the layer of this GameObject and all of its children.
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="layer">New layer for the object</param>
        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            foreach (Transform t in gameObject.transform)
                t.gameObject.SetLayerRecursively(layer);
        }

        /// <summary>
        /// Gets a mask of all the layers that can collide with the object
        /// </summary>
        /// <param name="gameObject"></param>
        /// <param name="layer"></param>
        /// <returns></returns>
        public static int GetCollisionMask(this GameObject gameObject, int layer = -1)
        {
            if (layer == -1)
                layer = gameObject.layer;

            int mask = 0;
            for (int i = 0; i < 32; i++)
                mask |= (Physics.GetIgnoreLayerCollision(layer, i) ? 0 : 1) << i;

            return mask;
        }

        #endregion


        #region Transform

        /// <summary>
        /// Sets the position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="v">New position</param>
        public static void SetPosition(this GameObject gObj, Vector3 v)
        {
            gObj.transform.position = v;
        }

        /// <summary>
        /// Sets the position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="x">X field</param>
        /// <param name="y">Y field</param>
        /// <param name="z">Z field</param>
        public static void SetPosition(this GameObject gObj, float x, float y, float z)
        {
            gObj.transform.position = new Vector3(x, y, z);
        }

        /// <summary>
        /// Sets the X position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="x">X field</param>
        public static void SetX(this GameObject gObj, float x)
        {
            gObj.transform.position = new Vector3(x, gObj.transform.position.y, gObj.transform.position.z);
        }

        /// <summary>
        /// Sets the Y position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="y">Y field</param>
        public static void SetY(this GameObject gObj, float y)
        {
            gObj.transform.position = new Vector3(gObj.transform.position.x, y, gObj.transform.position.z);
        }

        /// <summary>
        /// Sets the Z position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="z">Z field</param>
        public static void SetZ(this GameObject gObj, float z)
        {
            gObj.transform.position = new Vector3(gObj.transform.position.x, gObj.transform.position.y, z);
        }


        /// <summary>
        /// Adds to the X position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="x">X field</param>
        public static void AddX(this GameObject gObj, float x)
        {
            gObj.transform.position = new Vector3(gObj.transform.position.x + x, gObj.transform.position.y, gObj.transform.position.z);
        }

        /// <summary>
        /// Adds to the Y position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="y">Y field</param>
        public static void AddY(this GameObject gObj, float y)
        {
            gObj.transform.position = new Vector3(gObj.transform.position.x, gObj.transform.position.y + y, gObj.transform.position.z);
        }

        /// <summary>
        /// Adds to the Z position of a Game Object
        /// </summary>
        /// <param name="gObj"></param>
        /// <param name="z">Z field</param>
        public static void AddZ(this GameObject gObj, float z)
        {
            gObj.transform.position = new Vector3(gObj.transform.position.x, gObj.transform.position.y, gObj.transform.position.z + z);
        }


        #endregion


        #region Clone

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="copyName">Does the new instance will have the same name from the original?</param>
        /// <returns></returns>
        public static GameObject Clone(this GameObject original, bool copyName = true)
        {
            if (copyName)
                return original.Clone(original.name);
            else
                return (GameObject.Instantiate(original) as GameObject);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="position">Position of the new instance.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, Vector3 position)
        {
            return original.Clone(position, original.transform.rotation, original.name, original.transform.parent);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="name">Name of the new instance.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, string name)
        {
            return original.Clone(original.transform.position, original.transform.rotation, name, original.transform.parent);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="customParent">Parent of the new instance (use 'null' to put the new instance at the root)</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation is modified such that the object keeps the same world space position, rotation and scale as before.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, Transform parent, bool worldPositionStays = false)
        {
            return original.Clone(original.transform.position, original.transform.rotation, original.name, parent, worldPositionStays);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="position">Position of the new instance.</param>
        /// <param name="customParent">Parent of the new instance (use 'null' to put the new instance at the root)</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation is modified such that the object keeps the same world space position, rotation and scale as before.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, Vector3 position, Transform parent, bool worldPositionStays = false)
        {
            return original.Clone(position, original.transform.rotation, original.name, parent, worldPositionStays);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="name">Name of the new instance.</param>
        /// <param name="customParent">Parent of the new instance (use 'null' to put the new instance at the root)</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation is modified such that the object keeps the same world space position, rotation and scale as before.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, string name, Transform parent, bool worldPositionStays = false)
        {
            return original.Clone(original.transform.position, original.transform.rotation, name, parent, worldPositionStays);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="position">Position of the new instance.</param>
        /// <param name="name">Name of the new instance.</param>
        /// <param name="customParent">Parent of the new instance (use 'null' to put the new instance at the root)</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation is modified such that the object keeps the same world space position, rotation and scale as before.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, Vector3 position, string name, Transform parent, bool worldPositionStays = false)
        {
            return original.Clone(position, original.transform.rotation, name, parent, worldPositionStays);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="position">Position of the new instance.</param>
        /// <param name="rotation">Rotation of the new instance.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation)
        {
            return original.Clone(position, rotation, original.name, original.transform.parent);
        }

        /// <summary>
        /// Creates a new instance of the Game Object
        /// </summary>
        /// <param name="original">Original object to clone from.</param>
        /// <param name="position">Position of the new instance.</param>
        /// <param name="rotation">Rotation of the new instance.</param>
        /// <param name="name">Name of the new instance.</param>
        /// <param name="customParent">Parent of the new instance (use 'null' to put the new instance at the root)</param>
        /// <param name="worldPositionStays">If true, the parent-relative position, scale and rotation is modified such that the object keeps the same world space position, rotation and scale as before.</param>
        /// <returns>A clone of the Game Object</returns>
        public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation, string name, Transform parent, bool worldPositionStays = false)
        {
            GameObject clone = GameObject.Instantiate(original, position, rotation) as GameObject;

            clone.transform.SetParent(parent, worldPositionStays);
            clone.name = name;

            return clone;
        }

        #endregion
    }

}