using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnPause = new UnityEvent();
    public UnityEvent Unpause = new UnityEvent();
    public UnityEvent DeathScreen = new UnityEvent();
    private bool isPaused = false;
    private InputAction pauseAction;
    
    void OnEnable()
    {
        var inputMap = InputSystem.actions;
        pauseAction = inputMap.FindAction("UI/Cancel");
        if (pauseAction != null) pauseAction.Enable();
    }
    
    void OnDisable()
    {
        if (pauseAction != null) pauseAction.Disable();
    }
    
    void Update()
    {
        if (pauseAction != null && pauseAction.WasPressedThisFrame())
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
    public static void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
