using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnPause = new UnityEvent();
    public UnityEvent Unpause = new UnityEvent();
    public UnityEvent DeathScreen = new UnityEvent();
    private bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                UnpauseGame();
                isPaused = false;
            }
            else
            {
                Pause();
                isPaused = true;
            }
        }
    }
    public static void LoadScene(int sceneBuildIndex)
    {
        SceneManager.LoadScene(sceneBuildIndex);
    }
    public static void Quit()
    {
        Application.Quit();
    }
    public void Pause()
    {
        Time.timeScale = 0f;
        OnPause.Invoke();
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1f;
        Unpause.Invoke();
    }
    public void PlayerDeath()
    {
        Time.timeScale = 0f;
        DeathScreen.Invoke();
    }
}
