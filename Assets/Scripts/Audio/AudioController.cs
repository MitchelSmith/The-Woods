using UnityEngine;

public class AudioController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<AudioSource>().Play();
    }
}
