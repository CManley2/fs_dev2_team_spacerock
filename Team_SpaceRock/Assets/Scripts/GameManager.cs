using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;

    [SerializeField] TMP_Text asteroidCounterText;
    int asteroidTotal;
    int gameGoalCount;

    public GameObject player;
    public playerController playerScript;
    public Image playerHPBar;

    public bool isPaused;
    float timeScaleOrig;

    [SerializeField] float gameTimer = 120f;
    [SerializeField] TextMeshProUGUI timerTextBox;
    [SerializeField] TextMeshProUGUI scoreTextBox;

    int minutes;
    int seconds;

    public int score;

    void Awake()
    {
        instance = this;

        timeScaleOrig = Time.timeScale;

        score = 0;

        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerScript = player.GetComponent<playerController>();
        }
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                StatePause();
                menuActive = menuPause;
                if (menuActive != null)
                    menuActive.SetActive(true);
            }
            else if (menuActive == menuPause)
            {
                StateUnpause();
            }
        }

        if (!isPaused)
        {
            if (gameTimer > 0f)
            {
                gameTimer -= Time.deltaTime;
                if (gameTimer < 0f)
                    gameTimer = 0f;
            }
        }

        if (timerTextBox != null)
        {
            minutes = (int)(gameTimer / 60);
            seconds = (int)(gameTimer % 60);
            timerTextBox.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }

        if (scoreTextBox != null)
        {
            scoreTextBox.text = score.ToString();
        }

        if (gameTimer <= 0f && !isPaused)
        {
            YouLose();
        }
    }

    public void StatePause()
    {
        isPaused = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void StateUnpause()
    {
        isPaused = false;
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (menuActive != null)
        {
            menuActive.SetActive(false);
            menuActive = null;
        }
    }

    public void UpdateGameGoal(int amount)
    {
        gameGoalCount += amount;

        if (amount > 0)
        {
            asteroidTotal += amount;
        }

        if (asteroidCounterText != null && asteroidTotal > 0)
        {
            int destroyed = asteroidTotal - gameGoalCount;
            asteroidCounterText.text = destroyed + "/" + asteroidTotal;
        }

        if (gameGoalCount <= 0)
        {
            StatePause();
            menuActive = menuWin;
            if (menuActive != null)
                menuActive.SetActive(true);
        }
    }

    public void YouLose()
    {
        StatePause();
        menuActive = menuLose;
        if (menuActive != null)
            menuActive.SetActive(true);
    }

    public void UpdateScore(int amount)
    {
        score += amount;
    }
}