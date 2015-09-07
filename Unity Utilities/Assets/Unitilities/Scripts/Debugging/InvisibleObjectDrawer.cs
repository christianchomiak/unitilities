/// <summary>
/// InvisibleObjectDrawer v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Script to show a GameObject's position ingame.
/// Meant to be used on objects that are  have no meshrender.
/// </summary>

using UnityEngine;

namespace Unitilities.Debugging
{

    /// <summary>
    /// Script that shows the location of a GameObject (only works in the Editor)
    /// </summary>
    public class InvisibleObjectDrawer : MonoBehaviour
    {
        public enum Shape { SPHERE, CUBE };

        public bool isEnabled = true;

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

            Gizmos.color = color;

            if (shape == Shape.SPHERE)
                Gizmos.DrawWireSphere(transform.position, radius);
            else
            {
                Vector3 newScale = transform.localScale;
                /*newScale = newScale.MultiplyX(scale.x);
                newScale = newScale.MultiplyY(scale.y);
                newScale = newScale.MultiplyZ(scale.z);*/

                Gizmos.DrawWireCube(position + transform.position, newScale);
            }
        }

    }

}