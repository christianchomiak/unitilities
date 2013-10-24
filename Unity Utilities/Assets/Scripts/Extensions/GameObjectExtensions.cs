using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class GameObjectExtensions
{
    #region Recursive Modifiers
    public static void SetCollisionRecursively(this GameObject gameObject, bool tf)
    {
        Collider[] colliders = gameObject.GetComponentsInChildren<Collider>();
        foreach (Collider collider in colliders)
            collider.enabled = tf;
    }

    public static void SetVisualRecursively(this GameObject gameObject, bool tf)
    {
        Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
            renderer.enabled = tf;
    }
    #endregion


    #region Recursive Getters
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

    // Set the layer of this GameObject and all of its children.
    public static void SetLayerRecursively(this GameObject gameObject, int layer)
    {
        gameObject.layer = layer;
        foreach (Transform t in gameObject.transform)
            t.gameObject.SetLayerRecursively(layer);
    }

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
    public static void SetPosition(this GameObject gObj, Vector3 v)
    {
        gObj.transform.position = v;
    }

    public static void SetPosition(this GameObject gObj, float x, float y, float z)
    {
        gObj.transform.position = new Vector3(x, y, z);
    }

    public static void SetX(this GameObject gObj, float x)
    {
        gObj.transform.position = new Vector3(x, gObj.transform.position.y, gObj.transform.position.z);
    }

    public static void SetY(this GameObject gObj, float y)
    {
        gObj.transform.position = new Vector3(gObj.transform.position.x, y, gObj.transform.position.z);
    }

    public static void SetZ(this GameObject gObj, float z)
    {
        gObj.transform.position = new Vector3(gObj.transform.position.x, gObj.transform.position.y, z);
    }
    #endregion


    #region Clone

    public static GameObject Clone(this GameObject original, bool copyName = true)
    {
        if (copyName)
            return original.Clone(original.name);
        else
            return (GameObject.Instantiate(original) as GameObject);
    }

    public static GameObject Clone(this GameObject original, Vector3 position)
    {
        return original.Clone(position, original.transform.rotation, original.transform.parent, original.name);
    }

    public static GameObject Clone(this GameObject original, string name)
    {
        return original.Clone(original.transform.position, original.transform.rotation, original.transform.parent, name);
    }

    public static GameObject Clone(this GameObject original, Transform parent)
    {
        return original.Clone(original.transform.position, original.transform.rotation, parent, original.name);
    }

    public static GameObject Clone(this GameObject original, Vector3 position, Transform parent)
    {
        return original.Clone(position, original.transform.rotation, parent, original.name);
    }

    public static GameObject Clone(this GameObject original, Transform parent, string name)
    {
        return original.Clone(original.transform.position, original.transform.rotation, parent, name);
    }

    public static GameObject Clone(this GameObject original, Vector3 position, Transform parent, string name)
    {
        return original.Clone(position, original.transform.rotation, parent, name);
    }

    public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation)
    {
        return original.Clone(position, rotation, original.transform.parent, original.name);
    }

    public static GameObject Clone(this GameObject original, Vector3 position, Quaternion rotation, Transform parent, string name)
    {
        GameObject clone = GameObject.Instantiate(original, position, rotation) as GameObject;

        clone.transform.parent = parent;
        clone.name = name;

        return clone;
    }

    #endregion
}
