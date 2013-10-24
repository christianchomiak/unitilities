using UnityEngine;

public class InvisibleObjectDrawer : MonoBehaviour
{
    [SerializeField]
    float radius = 1f;
    
    [SerializeField]
    Color color = Color.white;

    void Awake()
    {
        if (radius <= 0f)
            radius = 1f;
    }

    /// <summary>
    /// Draw a sphere to signal the object's position on editor view
    /// </summary>
    void OnDrawGizmos()
    {
        Gizmos.color = this.color;
        Gizmos.DrawWireSphere(this.transform.position, this.radius);
    }

}
