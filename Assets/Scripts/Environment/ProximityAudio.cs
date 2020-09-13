using System.Collections;
using UnityEngine;

public class ProximityAudio : MonoBehaviour
{
    [SerializeField] float maxVolume = 0.4f;

    private AudioSource audioSource = null;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        audioSource.volume = maxVolume;
    }

    void OnTriggerExit(Collider other)
    {
        audioSource.volume = 0;
    }
}
