using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PanelController PanelController;
    public CameraController CameraController;

    [HideInInspector]
    public RebelisAPIHandler RebelisAPIHandler;

    private void Awake()
    {
        RebelisAPIHandler = GetComponent<RebelisAPIHandler>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
