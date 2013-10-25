using UnityEngine;

public class InvisibleObjectDrawer : MonoBehaviour
{
    public bool isEnabled;

    public float radius = 1f;
    
    public Color color = Color.white;

    void Awake()
    {
        if (!Utilities.PlatformIsEditor())
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
