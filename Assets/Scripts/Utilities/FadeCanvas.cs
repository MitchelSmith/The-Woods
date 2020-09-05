using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    public void FadeInGameOverScreen()
    {
        GetComponent<Animator>().SetBool("Alive", false);
    }
}
