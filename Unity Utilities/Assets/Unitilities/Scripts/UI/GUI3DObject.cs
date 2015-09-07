/// <summary>
/// GUI3DObject v1.0.0 by Christian Chomiak, christianchomiak@gmail.com
/// 
/// Script to position a gameobject relative to the screen (like an UI element).
/// 
/// To be used along with OrthographicCameraSetup.
/// </summary>

using UnityEngine;

namespace Unitilities.UI
{

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

        OrthographicCameraSetup oCamera;

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
        }

        // Use this for initialization
        void Start()
        {
            //Debug.Log("Aspect: " + Camera.main.aspect);
            if (customRatio == Vector2.zero)
            {
                customRatio = new Vector2(Screen.width, Screen.height);
            }

            SetRelativeData();
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

            oCamera = Camera.main.GetComponent<OrthographicCameraSetup>();
            SetRelativeSize();
            SetRelativePosition();
        }

        void SetRelativeSize()
        {
            if (relativeTo == SizeRelation.BothSidesOfScreen)
            {
                transform.localScale = new Vector3(relativeSize.x * Camera.main.aspect, relativeSize.y, transform.localScale.z);
                //this.transform.position = new Vector3(relativePosition.x * Camera.main.aspect, relativePosition.y, relativePosition.z);
            }
            else if (relativeTo == SizeRelation.OneSideOfScreen)
            {
                float ratioX = 1f;
                float ratioY = 1f;
                float unit = relativeSingleSize;

                float screenFactor = 1f;
                if (selectedAxis == ScreenAxis.X)
                {
                    ratioY = customRatio.y / customRatio.x;
                    unit *= Camera.main.aspect;
                    screenFactor = Screen.width;
                    //this.transform.localScale = new Vector3(relativeSize.x * Camera.main.aspect, relativeSize.y * Camera.main.aspect, transform.localScale.z);
                    //this.transform.position = new Vector3(relativePosition.x * Camera.main.aspect, relativePosition.y, relativePosition.z);
                }
                else if (selectedAxis == ScreenAxis.Y)
                {
                    ratioX = customRatio.x / customRatio.y;
                    unit *= 1;
                    screenFactor = Screen.height;
                    //this.transform.localScale = new Vector3(relativeSize.x, relativeSize.y, transform.localScale.z);
                };
                if (oCamera.matchRealScreenSize)
                    transform.localScale = new Vector3(unit * ratioX * screenFactor, unit * ratioY * screenFactor, transform.localScale.z);
                else
                    transform.localScale = new Vector3(unit * ratioX, unit * ratioY, transform.localScale.z);
            }
        }

        void SetRelativePosition()
        {
            Vector2 offset = Vector2.zero;

            Bounds mainBounds = new Bounds(Vector3.zero, Vector3.zero);
            //mainBounds = this.renderer.bounds;
            switch (anchorBounds)
            {
                case AnchorBounds.Renderer:
                    if (GetComponent<Renderer>()) { mainBounds = GetComponent<Renderer>().bounds; }
                    break;
                case AnchorBounds.Collider:
                    if (GetComponent<Collider>()) { mainBounds = GetComponent<Collider>().bounds; }
                    break;
                default:
                    break;
            }


            if (mainBounds.size.magnitude != 0)
            {
                switch (positionAnchor)
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
                Debug.LogWarning("Warning: GUI3DItem with invalid bound settings -> " + gameObject, gameObject);
            }


            if (oCamera.matchRealScreenSize)
                transform.position = new Vector3(Screen.width * relativePosition.x + offset.x, Screen.height * relativePosition.y + offset.y, transform.position.z);
            else
                transform.position = new Vector3(relativePosition.x * Camera.main.aspect + offset.x, relativePosition.y + offset.y, transform.position.z);

        }

    }

}