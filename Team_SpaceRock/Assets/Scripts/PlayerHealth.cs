using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [Header("Health")]
    [SerializeField] int maxHP = 5;
    int currentHP;

    [Header("UI")]
    [SerializeField] Slider healthSlider;
    [SerializeField] TMP_Text healthText;

    void Start()
    {
        currentHP = maxHP;

        if (healthSlider != null)
        {
            healthSlider.minValue = 0;
            healthSlider.maxValue = maxHP;
            healthSlider.value = maxHP;
        }

        UpdateUI();
    }

    public void takeDamage(int amount)
    {
        currentHP -= amount;
        if (currentHP < 0) currentHP = 0;

        UpdateUI();

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void UpdateUI()
    {
        if (healthSlider != null)
        {
            healthSlider.value = currentHP;
        }

        if (healthText != null)
        {
            healthText.text = currentHP + "/" + maxHP;
        }
    }

    void Die()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.YouLose();
        }
    }
}