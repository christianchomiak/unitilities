using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class TimeManager : Singleton<TimeManager> 
{
    /// <summary>
    /// Tuple class of one TimeLayer and a flot
    /// </summary>
    /// <typeparam name="First">Time Layer</typeparam>
    /// <typeparam name="Second">float value</typeparam>
    [System.Serializable]
    public class TupleTimeLayerScale : Tuple<TimeLayer, float>
    {
        public TupleTimeLayerScale(TimeLayer a, float b)
            : base(a, b)
        {
        }
    }

    public enum TimeLayer { REALTIME };

    public Dictionary<TimeLayer, float> timeScales = new Dictionary<TimeLayer, float>();

    public List<TimeLayer> layers;
    public List<float> scales;
    public int numberOfLayers = 0;
    

    protected override void Awake()
    {
        base.Awake();

        timeScales = new Dictionary<TimeLayer, float>();

        for (int i = 0; i < numberOfLayers; i++)
        {
            float value = 0f;

            if (timeScales.TryGetValue(layers[i], out value))
            {
            }
            else
            {
                timeScales.Add(layers[i], scales[i]);
            }
        }

        var keys = new List<TimeLayer>(timeScales.Keys);
        foreach (TimeLayer key in keys)
        {
            Debug.Log("K: " + key.ToString() + ", V: " + timeScales[key]);
        }
    }

	// Use this for initialization
	void Start () 
    {
	}


    // Update is called once per frame
    void Update()
    {

    }

    public float Scale(TimeLayer layer)
    {
        // When a program often has to try keys that turn out not to 
        // be in the dictionary, TryGetValue can be a more efficient  
        // way to retrieve values.
        float value = 0f;

        if (timeScales.TryGetValue(layer, out value))
        {
            return value;
        }
        else
        {
            return Time.timeScale;
        }
    }

    public float Scale()
    {
        return Time.timeScale;
    }

    public float DeltaTime()
    {
        return Time.deltaTime;
    }

    public float DeltaTime(TimeLayer layer)
    {
        float scale = Scale(layer);

        return Time.deltaTime * scale;
    }
}
