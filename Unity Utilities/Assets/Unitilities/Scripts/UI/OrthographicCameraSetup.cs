using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class OrthographicCameraSetup : MonoBehaviour
{
    /// <summary>
    /// Helps to run some functions at edition time (inside Unity Editor)
    /// Needs [ExecuteInEditMode] specified for the class
    /// </summary>
    public bool runInEdit = false;

    public bool matchRealScreenSize = false;

    [SerializeField]
    Camera camera;

    public Vector2 CameraSize
    {
        get
        {
            if (matchRealScreenSize)
            {
                return new Vector2(Screen.width, Screen.height);
            }
            else
            {
                return Vector2.one;
            }
        }
    }

    void Awake()
    {
        if (camera == null)
        {
            camera = Camera.main; // gameObject.GetComponent<Camera>();
        }
    }

    void Start()
    {
        //Debug.Log("Start Camera");
        MatchCameraToScreen();
    }

    void OnGUI()
    {
        if (!Application.isPlaying && this.runInEdit)
        {
            MatchCameraToScreen();
        }
    }

    /// <summary>
    /// Set gui camera size to match units against pixels 
    /// 
    /// NOTE: was not working right if done inside Awake (seems to work fine on Start), another user even said he had to do it with a 
    ///         coroutine to wait a bit, but the problem maybe happens only at app launch and works the right way for other screens/restarts
    /// </summary>
    protected void MatchCameraToScreen()
    {
        if (NeedToMatchCameraToScreen())
        {

            float h = matchRealScreenSize ? Screen.height : 1f;
            float w = matchRealScreenSize ? Screen.width : 1f;

            camera.orthographic = true;
            camera.orthographicSize = h / 2; //0.5f;
            camera.transform.position = new Vector3(0.5f * camera.aspect * w, 0.5f * h, -0.5f);
            camera.transform.position = new Vector3(0.5f * (matchRealScreenSize ? 1f : camera.aspect) * w, 0.5f * h, -0.5f);

            //Verify first to avoid updating repeated values because the scene will see itself as unsaved
            //this.camera.orthographicSize = Screen.height / 2;
            //this.camera.transform.position = new Vector3(0, 0, this.camera.transform.position.z); // Screen.width / 2, Screen.height / 2, this.camera.transform.position.z);
        }
    }

    /// <summary>
    /// Verify if the camera needs to be matched to screen size
    /// </summary>
    /// <returns>True if some parameters must be ajusted</returns>
    protected bool NeedToMatchCameraToScreen()
    {
        /*        if (!Mathf.Approximately(this.camera.orthographicSize, Screen.height / 2)
            || !Mathf.Approximately(this.camera.transform.position.x, Screen.width / 2)
            || !Mathf.Approximately(this.camera.transform.position.y, Screen.height / 2))*/

        if (!Mathf.Approximately(this.camera.orthographicSize, 0.5f)
            || !Mathf.Approximately(this.camera.transform.position.x, 0.5f * camera.aspect)
            || !Mathf.Approximately(this.camera.transform.position.y, 0.5f))
        {
            return true;
        }

        return false;
    }

}
