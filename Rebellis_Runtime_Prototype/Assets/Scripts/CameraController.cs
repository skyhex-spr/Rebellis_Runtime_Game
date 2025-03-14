using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{

    private GameManager _gameManager;

    public float rotationSpeed = 200f;
    public float moveSpeed = 5f;
    public float zoomSpeed = 2f;
    public Transform target;
    public BoxCollider bounds; // Added bounds for movement restriction

    public CinemachineFollow Cinefollowl;

    private float rotationY;
    private float zoomLevel = 1f;

    private void Start()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
    }

    void Update()
    {
        if (target != null && !_gameManager.PanelController.IsPanelOpen)
        {
            // Rotation Logic
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            rotationY += mouseX;
            target.rotation = Quaternion.Euler(0f, rotationY, 0f);

            // Movement Logic (Forward on Z-axis)
            float moveForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
            float moveSide = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

            Vector3 forwardMovement = target.forward * moveForward;
            Vector3 sideMovement = target.right * moveSide;
            Vector3 newPosition = target.position + forwardMovement + sideMovement;

            // Clamp position within the bounds
            if (bounds != null)
            {
                newPosition.x = Mathf.Clamp(newPosition.x, bounds.bounds.min.x, bounds.bounds.max.x);
                newPosition.z = Mathf.Clamp(newPosition.z, bounds.bounds.min.z, bounds.bounds.max.z);
            }

            target.position = newPosition;

            // Zoom logic
            float wheelInput = Input.GetAxis("Mouse ScrollWheel");
            zoomLevel = Mathf.Clamp(zoomLevel + wheelInput * zoomSpeed, 1f, 5f); // Clamp zoom level to a range of 1 to 5

            // Adjust FollowOffset Y to reflect zoom, ensuring it's between 0 and 5
            float zoomY = Mathf.Lerp(5f, 0f, (zoomLevel - 1f) / 4f); // Interpolate zoom effect between 5 and 0
            Cinefollowl.FollowOffset.y = zoomY; // Apply the zoom effect
        }
    }


}