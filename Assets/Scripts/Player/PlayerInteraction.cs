using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Tooltip("First Person Camera")] [SerializeField] Camera FPCamera = null;
    [Tooltip("Meters")] [SerializeField] float range = 1f;

    [SerializeField] Image pickupIcon = null;
    [SerializeField] Image noteIcon = null;

    int layerMask = 0;

    void Start()
    {
        layerMask = LayerMask.GetMask("InteractableObject");

        pickupIcon.enabled = false;
        noteIcon.enabled = false;
    }

    void Update()
    {
        ProcessRaycast();
    }

    private void ProcessRaycast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range, layerMask))
        {
            ProcessInteraction(hit);
        }
        else
        {
            HideIcons();
        }
    }

    private void ProcessInteraction(RaycastHit hit)
    {
        if (hit.transform.tag == "Ammo")
        {
            pickupIcon.enabled = true;
        }
    }

    private void HideIcons()
    {
        pickupIcon.enabled = false;
        noteIcon.enabled = false;
    }
}
