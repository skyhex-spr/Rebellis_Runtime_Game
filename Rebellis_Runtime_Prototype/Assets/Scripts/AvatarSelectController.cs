using HighlightPlus;
using UnityEngine;

public class AvatarSelectController : MonoBehaviour
{
    public LayerMask LayerToSelect;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit,Mathf.Infinity, LayerToSelect))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.red, 2f);
                Debug.Log("Actually hit: " + hit.collider.name);
                RebellisAnimatorController rebelliscontroller = hit.collider.gameObject.GetComponent <RebellisAnimatorController>();
                rebelliscontroller.ToggleAvatar();
            }
        }
    }
}
