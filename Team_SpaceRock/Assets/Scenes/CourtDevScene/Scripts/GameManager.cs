using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float gameTimer;
    [SerializeField] TextMeshProUGUI timerTextBox;
    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject gameOverMenu;

    int minutes;
    int seconds;

    float originalTimeScale;
    bool isPaused;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
        instance = this;
        originalTimeScale = Time.timeScale;
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        minutes = (int)(gameTimer / 60);
        seconds = (int)(gameTimer % 60);
        timerTextBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (gameTimer <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        Pause();
        activeMenu = gameOverMenu;
        activeMenu.SetActive(true);
    }

    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Unpause()
    {
        isPaused = false;
        Time.timeScale = originalTimeScale;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

}
