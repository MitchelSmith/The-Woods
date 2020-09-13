using System.Collections;
using UnityEngine;

public class LampController : MonoBehaviour
{
    private AudioSource audioSource = null;
    private Animator animator = null;
    private bool hasPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        animator.SetBool("Flickering", true);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed)
        {
            hasPlayed = true;
            animator.SetBool("Flickering", false);
            animator.SetTrigger("EnterProximity");

            StartCoroutine(StartEffects());
        }
    }

    private IEnumerator StartEffects()
    {
        audioSource.Play();

        yield return new WaitWhile(() => audioSource.isPlaying);

        TurnOffLight();
    }

    private void TurnOffLight()
    {
        Light[] lights = GetComponentsInChildren<Light>();

        GetComponent<Renderer>().material.DisableKeyword("_EMISSION");

        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }
}
