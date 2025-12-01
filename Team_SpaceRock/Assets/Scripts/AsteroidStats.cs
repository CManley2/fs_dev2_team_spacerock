using UnityEngine;

public class Asteroid : MonoBehaviour, IDamage
{
    [SerializeField] int maxHP = 3;
    int currentHP;

    void Start()
    {
        currentHP = maxHP;
    }

    public void takeDamage(int amount)
    {
        currentHP -= amount;

        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // TODO: particles, score, sound, etc
        Destroy(gameObject);
    }
}
