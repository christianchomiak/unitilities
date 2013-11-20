using UnityEngine;
using System.Collections;

/// <summary>
/// Dummy script to test various things
/// </summary>
public class TestScript : MonoBehaviour
{
    int a = 0;


	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            a++;
            Debug.Log("A: " + a);
            PlayerPrefs.SetInt("A", a);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            PlayerPrefs.Save();
        }

        /*ScreenInput s = InputManager.Instance.TouchInput(0);

        if (s != null)
            Debug.Log(s.ToString());*/
	}
}
