using UnityEngine;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour
{
    [Tooltip("First Person Camera")] [SerializeField] Camera FPCamera = null;
    [Tooltip("Meters")] [SerializeField] float range = 1f;

    [SerializeField] Image pickupIcon = null;
    [SerializeField] Image noteIcon = null;
    [SerializeField] Image generalIcon = null;

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
        
        // Only handle looking at interactable objects
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
            case "Flashlight":
            case "Weapon":
                InteractWithCollectableObject(hit);
                break;
            case "Radio":
                InteractWithGeneralObject(hit);
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
        generalIcon.enabled = false;
    }

    private void InteractWithCollectableObject(RaycastHit hit)
    {
        // Ensures only one icon showing at a time
        HideIcons();
        pickupIcon.enabled = true;

        if (Input.GetButtonDown("Interact"))
        {
            switch (hit.transform.tag)
            {
                case "Ammo":
                    InteractWithAmmo(hit);
                    break;
                case "Flashlight":
                    InteractWithFlashlight(hit);
                    break;
                case "Weapon":
                    InteractWithWeapon(hit);
                    break;
            }
        }
    }

    private void InteractWithAmmo(RaycastHit hit)
    {
        AmmoPickup ammo = hit.transform.GetComponent<AmmoPickup>();
        FindObjectOfType<Ammo>().IncreaseCurrentAmmo(ammo.type, ammo.amount);
        Destroy(ammo.gameObject);
    }

    private void InteractWithFlashlight(RaycastHit hit)
    {
        GameObject flashlight = hit.transform.gameObject;
        FlashlightSystem flashlightSystem = transform.GetComponent<FlashlightSystem>();
        flashlightSystem.PickupFlashlight();
        Destroy(flashlight);
    }

    private void InteractWithWeapon(RaycastHit hit)
    {
        GameObject weapon = hit.transform.gameObject;
        GetComponentInChildren<WeaponSwitcher>().hasPistol = true;
        Destroy(weapon);
    }

    private void InteractWithGeneralObject(RaycastHit hit)
    {
        // Ensures only one icon showing at a time
        HideIcons();
        generalIcon.enabled = true;

        if (Input.GetButtonDown("Interact"))
        {
            switch (hit.transform.tag)
            {
                case "Radio":
                    InteractWithRadio(hit);
                    break;
            }
        }
    }

    /// <summary>
    /// Mutes the radio
    /// </summary>
    /// <param name="hit"></param>
    private void InteractWithRadio(RaycastHit hit)
    {
        GameObject radio = hit.transform.gameObject;
        AudioSource audio = radio.GetComponent<AudioSource>();
        audio.mute = !audio.mute;
    }
}
