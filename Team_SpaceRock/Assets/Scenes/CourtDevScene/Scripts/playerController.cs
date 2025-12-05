using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("----- Component -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Stats -----")]
    [Range(1, 5)][SerializeField] int lives;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float acceleration = 5f;
    [SerializeField] float deceleration = 10f;
    [SerializeField] float rollSpeed = 90f;

    [Header("----- Dash -----")]
    [SerializeField] float dashSpeed = 25f;
    [SerializeField] float dashDuration = 0.25f;
    [SerializeField] float dashCooldown = 5f;

    bool isDashing = false;
    float dashTimer = 0f;
    float dashCooldownTimer = 0f;

    [Header("----- Guns -----")]
    [SerializeField] int shootDamage;
    [SerializeField] float shootRate;
    float shootTimer;

    public GameObject Bullet;
    public Transform RightFirePoint;
    public Transform LeftFirePoint;
    bool fireSwitch = true;

    public float projectileSpeed;

    Vector3 currentVelocity;
    Vector3 inputDir;

    float rollAngle = 0f;  // <<< persistent roll

    void Update()
    {
        movement();
        dash();
        rotation();
    }

    // ---------------------------------------
    // MOVEMENT
    // ---------------------------------------
    void movement()
    {
        shootTimer += Time.deltaTime;

        if (Input.GetButton("Fire1") && shootTimer >= shootRate)
        {
            shoot();
        }

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float u = 0f;
        if (Input.GetKey(KeyCode.Space)) u += 1f;
        if (Input.GetKey(KeyCode.LeftControl)) u -= 1f;

        inputDir = (transform.right * h) + (transform.up * u) + (transform.forward * v);

        if (inputDir.magnitude > 1f)
            inputDir.Normalize();

        // If dashing, ignore acceleration/deceleration
        if (isDashing)
        {
            controller.Move(currentVelocity * Time.deltaTime);
            return;
        }

        Vector3 targetVelocity = inputDir * moveSpeed;

        if (inputDir.magnitude > 0.01f)
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                targetVelocity,
                acceleration * Time.deltaTime
            );
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(
                currentVelocity,
                Vector3.zero,
                deceleration * Time.deltaTime
            );
        }

        controller.Move(currentVelocity * Time.deltaTime);
    }

    // ---------------------------------------
    // ROTATION (ROLL)
    // ---------------------------------------
    void rotation()
    {
        float rollInput = 0f;

        if (Input.GetKey(KeyCode.Q)) rollInput += 1f;
        if (Input.GetKey(KeyCode.E)) rollInput -= 1f;

        rollAngle = rollInput * rollSpeed * Time.deltaTime;

        Mathf.Clamp(rollAngle, -45, 45);

        // Apply rotation
        controller.transform.Rotate(0, 0, rollAngle);

    }

    // ---------------------------------------
    // DASH
    // ---------------------------------------
    void dash()
    {
        dashCooldownTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0f)
        {
            Vector3 dashDir = (inputDir.sqrMagnitude > 0.1f) ? inputDir : transform.forward;
            dashDir.Normalize();

            currentVelocity = dashDir * dashSpeed;

            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
            }
        }
    }

    // ---------------------------------------
    // SHOOTING
    // ---------------------------------------
    void shoot()
    {
        shootTimer = 0;

        Transform firePoint = fireSwitch ? RightFirePoint : LeftFirePoint;
        fireSwitch = !fireSwitch;

        GameObject projectile = Instantiate(Bullet, firePoint.position, firePoint.rotation);

        Projectile proj = projectile.GetComponent<Projectile>();
        proj.Initialize(shootDamage);

        Rigidbody rb = projectile.GetComponent<Rigidbody>();

        if (rb != null)
            rb.linearVelocity = firePoint.forward * projectileSpeed;

        //Luke's additions
        SoundManager.instance.audioSource.pitch = Random.Range(0.8f, 1);
        SoundManager.instance.audioSource.PlayOneShot(SoundManager.instance.shootSound);
    }

}
