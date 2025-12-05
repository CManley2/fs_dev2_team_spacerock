using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] float gameTimer;
    [SerializeField] TextMeshProUGUI timerTextBox;
    [SerializeField] TextMeshProUGUI scoreTextBox;

    [SerializeField] GameObject activeMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject pauseMenu;

    int minutes;
    int seconds;

    float originalTimeScale;
    bool isPaused;

    public int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        score = 0;
        instance = this;
        originalTimeScale = Time.timeScale;
    }

    private void Update()
    {
        gameTimer -= Time.deltaTime;
        minutes = (int)(gameTimer / 60);
        seconds = (int)(gameTimer % 60);
        timerTextBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        scoreTextBox.text = score.ToString();

        if (gameTimer <= 0)
        {
            GameOver();
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (activeMenu == null)
            {
                Pause();
                activeMenu = pauseMenu;
                activeMenu.SetActive(true);
            }
            else if (activeMenu == pauseMenu)
            {
                Unpause();
            }
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

    public void UpdateScore(int amount)
    {
        score += amount;
    }

}
