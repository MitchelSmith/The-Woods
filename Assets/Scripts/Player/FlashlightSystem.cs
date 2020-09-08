using UnityEngine;

public class FlashlightSystem : MonoBehaviour
{
    [SerializeField] GameObject flashlight = null;
    [SerializeField] bool flashlightOn = false;
    [SerializeField] AudioClip flashlightClick = null;
    private bool hasFlashlight = false;

    void Start()
    {
        flashlight.SetActive(false);
        hasFlashlight = false;
        flashlightOn = false;

        foreach (Transform child in flashlight.transform)
        {
            child.gameObject.SetActive(flashlightOn);
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Flashlight") && hasFlashlight)
        {
            ToggleFlashlight();
        }
    }

    public void PickupFlashlight()
    {
        hasFlashlight = true;
        flashlight.SetActive(true);
        ToggleFlashlight();
    }

    public void ToggleFlashlight()
    {
        flashlightOn = !flashlightOn;
        GetComponent<AudioSource>().PlayOneShot(flashlightClick);

        foreach (Transform child in flashlight.transform)
        {
            child.gameObject.SetActive(flashlightOn);
        }
    }
}
