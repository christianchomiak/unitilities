using UnityEngine;

/// <summary>
/// Script that shows the location of a GameObject (only works in the Editor)
/// </summary>
public class InvisibleObjectDrawer : MonoBehaviour
{
    public enum Shape { SPHERE, CUBE };

    public bool isEnabled;

    public Shape shape = Shape.SPHERE;

    public float radius = 1f;

    public Vector3 position;
    public Vector3 scale;

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

        if (shape == Shape.SPHERE)
            Gizmos.DrawWireSphere(this.transform.position, this.radius);
        else
        {
            Vector3 newScale = this.transform.localScale;
            newScale = newScale.MultiplyX(scale.x);
            newScale = newScale.MultiplyY(scale.y);
            newScale = newScale.MultiplyZ(scale.z);

            Gizmos.DrawWireCube(position + this.transform.position, newScale);
        }
    }

}