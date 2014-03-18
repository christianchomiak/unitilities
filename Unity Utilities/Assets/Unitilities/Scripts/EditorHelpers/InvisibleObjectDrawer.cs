using UnityEngine;

/// <summary>
/// Script that shows the location of a GameObject (only works in the Editor)
/// </summary>
public class InvisibleObjectDrawer : MonoBehaviour
{
    public bool isEnabled;

    public float radius = 1f;
    
    public Color color = Color.white;

    void Awake()
    {
        if (!ApplicationHelper.PlatformIsEditor)
            DestroyImmediate(this);
    }

    /// <summary>
    /// Draw a sphere to signal the object's position on editor view
    /// </summary>
    void OnDrawGizmos()
    {
        if (!isEnabled)
            return;

        Gizmos.color = this.color;
        Gizmos.DrawWireSphere(this.transform.position, this.radius);
    }

}
