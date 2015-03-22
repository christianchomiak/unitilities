using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unitilities.Colors;

using Unitilities;

public class TestScript : MonoBehaviour {

    public string myHex = "";
    public Color col;

	// Use this for initialization
	void Start ()
    {
        //col = ColorFromHex(myHex);
        myHex = col.ToHex();
    }
	
	// Update is called once per frame
	void Update () 
    {
        HSVColor c = HSVColor.black;
        c.ToHex();
    }

    /*string FloatToHex(float f)
    {
        string hex = "";
        byte[] bytes = new byte[1];

        float f2 = f * 255f;
        bytes[0] = (byte)(f2);

        hex = System.BitConverter.ToString(bytes);
        return hex;
    }

    float HexToFloat(string hex)
    {
        float f = 0f;

        byte[] hexBytes = StringToByteArrayFastest(hex);
        Debug.Log("Bytes: " + hexBytes[0].ToString());

        f = ((float)hexBytes[0]) / 255f;

        return f;
    }*/



}
