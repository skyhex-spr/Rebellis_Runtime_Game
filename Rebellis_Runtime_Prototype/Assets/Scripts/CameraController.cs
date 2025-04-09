using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    private GameManager _gameManager;

    public float rotationSpeed = 200f;
    public float moveSpeed = 5f;
    public float zoomSpeed = 2f;
    public Transform target;
    public BoxCollider bounds;

    public CinemachineFollow Cinefollowl;

    public UltimateJoystick joystick;
    public Canvas Canvasjoystick;

    private float rotationY;
    private float zoomLevel = 1f;
    private Vector2 lastTouchPosition;
    private bool isRotating = false;
    private float lastPinchDistance;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WebGLPlayer)
            Canvasjoystick.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (target != null && !_gameManager.PanelController.IsPanelOpen)
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                HandleTouchRotation();
                HandlePinchZoom();
                HandleMovementJoystick();
            }
            else
            {
                HandleMouseRotation();
                HandleMouseZoom();
                HandleMoveKeyboard();
            }

        }
    }

    // ---------- Windows / PC Controls ----------
    void HandleMoveKeyboard()
    {
        float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float moveSide = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        Vector3 forwardMovement = target.forward * moveForward;
        Vector3 sideMovement = target.right * moveSide;
        Vector3 newPosition = target.position + forwardMovement + sideMovement;
        if (bounds != null)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, bounds.bounds.min.x, bounds.bounds.max.x);
            newPosition.z = Mathf.Clamp(newPosition.z, bounds.bounds.min.z, bounds.bounds.max.z);
        }


        target.position = newPosition;
    }

    void HandleMouseRotation()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rotationY += mouseX;
            target.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }

    void HandleMouseZoom()
    {
        float wheelInput = Input.GetAxis("Mouse ScrollWheel");
        zoomLevel = Mathf.Clamp(zoomLevel + wheelInput * zoomSpeed, 1f, 5f);
        float zoomY = Mathf.Lerp(5f, 0f, (zoomLevel - 1f) / 4f);
        Cinefollowl.FollowOffset.y = zoomY;
    }

    // ---------- Android / iOS Controls ----------
    void HandleMovementJoystick()
    {
        float moveForward = joystick.GetVerticalAxis() * moveSpeed * Time.deltaTime;
        float moveSide = joystick.GetHorizontalAxis() * moveSpeed * Time.deltaTime;

        Vector3 forwardMovement = target.forward * moveForward;
        Vector3 sideMovement = target.right * moveSide;
        Vector3 newPosition = target.position + forwardMovement + sideMovement;

        if (bounds != null)
        {
            newPosition.x = Mathf.Clamp(newPosition.x, bounds.bounds.min.x, bounds.bounds.max.x);
            newPosition.z = Mathf.Clamp(newPosition.z, bounds.bounds.min.z, bounds.bounds.max.z);
        }

        target.position = newPosition;
    }
    void HandleTouchRotation()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            // Check if the touch is on the joystick
            if (!UltimateJoystick.GetInputActive("move"))
            {
                if (touch.phase == TouchPhase.Began)
                {
                    isRotating = true;
                    lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Moved && isRotating)
                {
                    float deltaX = touch.position.x - lastTouchPosition.x;
                    rotationY += deltaX * 0.2f;
                    target.rotation = Quaternion.Euler(0f, rotationY, 0f);
                    lastTouchPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    isRotating = false;
                }
            }
        }

    }

    void HandlePinchZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            float currentDistance = Vector2.Distance(touch1.position, touch2.position);
            if (touch1.phase == TouchPhase.Began || touch2.phase == TouchPhase.Began)
            {
                lastPinchDistance = currentDistance;
            }
            else
            {
                float pinchDelta = currentDistance - lastPinchDistance;
                zoomLevel = Mathf.Clamp(zoomLevel + pinchDelta * 0.01f, 1f, 5f);
                float zoomY = Mathf.Lerp(5f, 0f, (zoomLevel - 1f) / 4f);
                Cinefollowl.FollowOffset.y = zoomY;

                lastPinchDistance = currentDistance;
            }
        }
    }
}
