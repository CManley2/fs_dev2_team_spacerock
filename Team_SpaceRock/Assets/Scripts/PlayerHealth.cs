using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour, IDamage
{
    [Header("Health")]
    [SerializeField] int maxHP = 5;
    int currentHP;

    [Header("UI")]
    [SerializeField] TMP_Text healthText;

    void Start()
    {
        currentHP = maxHP;
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