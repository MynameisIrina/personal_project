using UnityEngine;

public class PauseControl : MonoBehaviour
{
    public static bool isPaused;

    private void Start()
    {
        isPaused = false;
    }

    public void PauseGame()
    {
        if (isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
}
