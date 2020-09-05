using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas = null;

    private CanvasGroup canvasGroup = null;
    private FadeCanvas fadeCanvas = null;

    private void Start()
    {
        // Start transparent so it can fade in
        canvasGroup = gameOverCanvas.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        gameOverCanvas.enabled = false;

        fadeCanvas = gameOverCanvas.GetComponent<FadeCanvas>();
    }

    public void HandleDeath()
    {
        fadeCanvas.FadeInGameOverScreen();
        gameOverCanvas.enabled = true;

        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
