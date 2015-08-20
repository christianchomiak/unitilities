using UnityEngine;

using Unitilities.Tuples;
using Unitilities;

public class TestScript : MonoBehaviour 
{
    public TupleI p1 = TupleI.zero;
    public TupleI p2 = TupleI.one;
    public Vector2 v = Vector2.zero;
    public Unitilities.Colors.HSVColor c = Unitilities.Colors.HSVColor.cyan;

    [Regex(@"^(?:\d{1,3}\.){3}\d{1,3}$", "Invalid IP address!\nExample: '127.0.0.1'")]
    //[Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", "Invalid IP address!\nExample: '127.0.0.1'")]
    public string serverAddress = "192.168.0.1";
    
    [ReadOnly]
    public Transform parentOfNewClone;

    public void Awake()
    {
        v = p2 + p1;
    }
}
