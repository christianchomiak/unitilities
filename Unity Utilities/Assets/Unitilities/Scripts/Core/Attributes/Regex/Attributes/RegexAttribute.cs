/// <summary>
/// RegexAttribute v1.0.0
/// 
/// Taken from: http://blogs.unity3d.com/2012/09/07/property-drawers-in-unity-4/
/// </summary>

using UnityEngine;

namespace Unitilities
{

    public class RegexAttribute : PropertyAttribute
    {
        public readonly string pattern;
        public readonly string helpMessage;
        public RegexAttribute(string pattern, string helpMessage)
        {
            this.pattern = pattern;
            this.helpMessage = helpMessage;
        }
    }

}