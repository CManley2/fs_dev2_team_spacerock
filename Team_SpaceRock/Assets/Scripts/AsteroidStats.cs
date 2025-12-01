using UnityEngine;

public class Asteroid : MonoBehaviour, IDamage
{
    [Header("Health")]
    [SerializeField] int maxHP = 3;
    int currentHP;

    public enum AsteroidSize { XSmall, Small, Medium, Large, XLarge }

    [Header("Size")]
    [SerializeField] AsteroidSize size = AsteroidSize.Medium;

    [SerializeField] float speedXSmall = 6f;
    [SerializeField] float speedSmall = 5f;
    [SerializeField] float speedMedium = 4f;
    [SerializeField] float speedLarge = 3f;
    [SerializeField] float speedXLarge = 2f;

    [Header("Movement")]
    [SerializeField] float rotationSpeed = 30f;

    [Header("Player Chase")]
    [SerializeField] float detectRange = 15f;
    [SerializeField] float chaseSpeedMultiplier = 1.5f;

    Rigidbody rb;

    Vector3 startPos;
    Quaternion startRot;


    void Start()
    {
        currentHP = maxHP;

        rb = GetComponent<Rigidbody>();

        startPos = transform.position;
        startRot = transform.rotation;

        SetupMovement();

        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateGameGoal(1);
        }
    }

    void SetupMovement()
    {
        if (rb == null) return;

        Vector3 dir = new Vector3(
            Random.Range(-1f, 1f),
            0f,
            Random.Range(-1f, 1f)
        ).normalized;

        float moveSpeed = GetSpeedForSize();

        rb.linearVelocity = dir * moveSpeed;

        Vector3 randomSpin = Random.insideUnitSphere * rotationSpeed;
        rb.angularVelocity = randomSpin * Mathf.Deg2Rad;
    }

    float GetSpeedForSize()
    {
        switch (size)
        {
            case AsteroidSize.XSmall: return speedXSmall;
            case AsteroidSize.Small: return speedSmall;
            case AsteroidSize.Medium: return speedMedium;
            case AsteroidSize.Large: return speedLarge;
            case AsteroidSize.XLarge: return speedXLarge;
        }

        return speedMedium;
    }

    void Update()
    {
        if (rb == null) return;
        if (GameManager.instance == null) return;
        if (GameManager.instance.player == null) return;

        Transform playerTransform = GameManager.instance.player.transform;

        Vector3 toPlayer = playerTransform.position - transform.position;
        float dist = toPlayer.magnitude;

        if (dist <= detectRange)
        {
            Vector3 dir = toPlayer.normalized;
            float moveSpeed = GetSpeedForSize() * chaseSpeedMultiplier;
            rb.linearVelocity = dir * moveSpeed;
        }
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
        if (GameManager.instance != null)
        {
            GameManager.instance.UpdateGameGoal(-1);
        }

        Destroy(gameObject);
    }

}
