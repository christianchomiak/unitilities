using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Unitilities;

public class TestScript : MonoBehaviour 
{
    public Unitilities.Tuples.TupleI pair;
    public Unitilities.Colors.HSVColor c;

    //[Regex(@"^(?:\d{1,3}\.){3}\d{1,3}$", "Invalid IP address!\nExample: '127.0.0.1'")]
    //[Regex(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", "Invalid IP address!\nExample: '127.0.0.1'")]
    //public string serverAddress = "192.168.0.1";

    public void Awake()
    { 
    }
}
