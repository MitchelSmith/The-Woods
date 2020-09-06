using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Tooltip("First Person Camera")] [SerializeField] Camera FPCamera = null;
    [Tooltip("Meters")] [SerializeField] float range = 1f;

    [SerializeField] Image pickupIcon = null;
    [SerializeField] Image noteIcon = null;

    private int layerMask = 0;

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
        switch (hit.transform.tag)
        {
            case "Ammo":
                InteractWithAmmo(hit);
                break;
            case "Note":
                noteIcon.enabled = true;
                // Handle clicking on note
                break;
        }
    }

    private void HideIcons()
    {
        pickupIcon.enabled = false;
        noteIcon.enabled = false;
    }

    private void InteractWithAmmo(RaycastHit hit)
    {
        pickupIcon.enabled = true;

        if (Input.GetButtonDown("Interact"))
        {
            AmmoPickup ammo = hit.transform.GetComponent<AmmoPickup>();
            FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammo.type, ammo.amount);
            Destroy(ammo.gameObject);
        }
    }
}
