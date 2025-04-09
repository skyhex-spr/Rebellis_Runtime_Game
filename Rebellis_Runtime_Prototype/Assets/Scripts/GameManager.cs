using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PanelController PanelController;
    public CameraController CameraController;

    [HideInInspector]
    public RebelisAPIHandler RebelisAPIHandler;

    public List<RebellisAnimatorController> Avatars = new List<RebellisAnimatorController>();

    private void Awake()
    {
        RebelisAPIHandler = GetComponent<RebelisAPIHandler>();
    }

    void Start()
    {
        Avatars = FindObjectsByType<RebellisAnimatorController>(FindObjectsSortMode.None).ToList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AreAllDeselctedAvatars()
    {
        foreach (var avatar in Avatars) 
        {
            if (avatar.Selected)
                return false;
        }

        return true;
    }

    public void SelectAllAvatars()
    {
        foreach (var avatar in Avatars)
        {
            avatar.ToggleAvatar(true);
        }
    }

}
