/// <summary>
/// RegexAttribute v1.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Taken from: http://blogs.unity3d.com/2012/09/07/property-drawers-in-unity-4/
/// </summary>

using UnityEngine;
using System.Collections;

namespace Unitilities.Extras
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