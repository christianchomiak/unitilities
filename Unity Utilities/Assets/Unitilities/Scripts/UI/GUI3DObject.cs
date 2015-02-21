using UnityEngine;
using System.Collections;

public class GUI3DObject : MonoBehaviour
{
    public enum SizeRelation
    {
        OneSideOfScreen,
        BothSidesOfScreen
    }

    public enum ScreenAxis
    {
        X,
        Y
    }

    public enum AnchorBounds
    {
        Renderer,
        Collider
    }

    [HideInInspector]
    public int _choiceIndex = 0;

    public bool useRelativeData = true;

    public float relativeSingleSize = 1f;
    public Vector2 relativeSize = Vector3.one;
    public Vector2 relativePosition = Vector2.zero;

    public SizeRelation relativeTo = SizeRelation.BothSidesOfScreen;
    public ScreenAxis selectedAxis = ScreenAxis.X;

    public TextAnchor positionAnchor = TextAnchor.MiddleCenter;
    public AnchorBounds anchorBounds = AnchorBounds.Renderer;


    public Vector2 customRatio = Vector2.one;

    public void SetBothSideRelation(TextAnchor _positionAnchor, AnchorBounds _anchorBounds, Vector2 _relativePosition, Vector2 _relativeSize)
    {
        useRelativeData = true;

        relativeTo = SizeRelation.BothSidesOfScreen;
        positionAnchor = _positionAnchor;
        anchorBounds = _anchorBounds;
        relativePosition = _relativePosition;

        relativeSize = _relativeSize;

        SetRelativeData();
    }

    public void SetOneSideRelation(TextAnchor _positionAnchor, AnchorBounds _anchorBounds, Vector2 _relativePosition, ScreenAxis _axis, float _relativeSize, Vector2 _customRatio)
    {
        useRelativeData = true;

        relativeTo = SizeRelation.OneSideOfScreen;
        positionAnchor = _positionAnchor;
        anchorBounds = _anchorBounds;
        relativePosition = _relativePosition;

        selectedAxis = _axis;
        relativeSingleSize = _relativeSize;
        customRatio = _customRatio;

        SetRelativeData();
    }

    public void SetOneSideRelation(Vector2 _customRatio)
    {
        useRelativeData = true;

        relativeTo = SizeRelation.OneSideOfScreen;

        customRatio = _customRatio;

        SetRelativeData();
    }

    void Awake()
    {
        if (customRatio == Vector2.zero)
        {
            customRatio = new Vector2(Screen.width, Screen.height);
        }

        SetRelativeData();
    }

    // Use this for initialization
    void Start()
    {
        //Debug.Log("Aspect: " + Camera.main.aspect);
    }

    /*void OnGUI() 
    {
        if (!Application.isPlaying) // && this.updateValues)
        {
            SetRelativeSize();
            SetRelativePosition();
            this.updateValues = false;
        }
    }*/

    void SetRelativeData()
    {
        if (!useRelativeData)
            return;

        SetRelativeSize();
        SetRelativePosition();
    }

    void SetRelativeSize()
    {
        if (relativeTo == SizeRelation.BothSidesOfScreen)
        {
            this.transform.localScale = new Vector3(relativeSize.x * Camera.main.aspect, relativeSize.y, transform.localScale.z);
            //this.transform.position = new Vector3(relativePosition.x * Camera.main.aspect, relativePosition.y, relativePosition.z);
        }
        else if (relativeTo == SizeRelation.OneSideOfScreen)
        {
            float ratioX = 1f;
            float ratioY = 1f;
            float unit = relativeSingleSize;

            if (selectedAxis == ScreenAxis.X)
            {
                ratioY = customRatio.y / customRatio.x;
                unit *= Camera.main.aspect;
                //this.transform.localScale = new Vector3(relativeSize.x * Camera.main.aspect, relativeSize.y * Camera.main.aspect, transform.localScale.z);
                //this.transform.position = new Vector3(relativePosition.x * Camera.main.aspect, relativePosition.y, relativePosition.z);
            }
            else if (selectedAxis == ScreenAxis.Y)
            {
                ratioX = customRatio.x / customRatio.y;
                unit *= 1;
                //this.transform.localScale = new Vector3(relativeSize.x, relativeSize.y, transform.localScale.z);
            };

            this.transform.localScale = new Vector3(unit * ratioX, unit * ratioY, transform.localScale.z);
        }
    }

    void SetRelativePosition()
    {
        Vector2 offset = Vector2.zero;

        Bounds mainBounds = new Bounds(Vector3.zero, Vector3.zero);
        //mainBounds = this.renderer.bounds;
        switch (this.anchorBounds)
        {
            case AnchorBounds.Renderer:
                if (this.renderer) { mainBounds = this.renderer.bounds; }
                break;
            case AnchorBounds.Collider:
                if (this.collider) { mainBounds = this.collider.bounds; }
                break;
            default:
                break;
        }


        if (mainBounds.size.magnitude != 0)
        {
            switch (this.positionAnchor)
            {
                case TextAnchor.LowerCenter:
                    offset.x = 0;
                    offset.y = mainBounds.extents.y;
                    break;
                case TextAnchor.LowerLeft:
                    offset.x = mainBounds.extents.x;
                    offset.y = mainBounds.extents.y;
                    break;
                case TextAnchor.LowerRight:
                    offset.x = -mainBounds.extents.x;
                    offset.y = mainBounds.extents.y;
                    break;
                case TextAnchor.MiddleCenter:
                    offset.x = 0;
                    offset.y = 0;
                    break;
                case TextAnchor.MiddleLeft:
                    offset.x = mainBounds.extents.x;
                    offset.y = 0;
                    break;
                case TextAnchor.MiddleRight:
                    offset.x = -mainBounds.extents.x;
                    offset.y = 0;
                    break;
                case TextAnchor.UpperCenter:
                    offset.x = 0;
                    offset.y = -mainBounds.extents.y;
                    break;
                case TextAnchor.UpperLeft:
                    offset.x = mainBounds.extents.x;
                    offset.y = -mainBounds.extents.y;
                    break;
                case TextAnchor.UpperRight:
                    offset.x = -mainBounds.extents.x;
                    offset.y = -mainBounds.extents.y;
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("Warning: GUI3DItem with invalid bound settings -> " + this.gameObject, this.gameObject);
        }

        this.transform.position = new Vector3(relativePosition.x * Camera.main.aspect + offset.x, relativePosition.y + offset.y, this.transform.position.z);

    }

}
