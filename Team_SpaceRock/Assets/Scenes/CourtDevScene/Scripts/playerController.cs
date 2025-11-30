using UnityEngine;

public class playerController : MonoBehaviour
{

    [Header("----- Component -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Stats -----")]
    [Range(1, 5)] [SerializeField] int lives;
    [Range(1, 5)][SerializeField] int rollSpeed;
    [Range(2, 5)] [SerializeField] int dashMod;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float acceleration = 8f;
    [SerializeField] float deceleration = 6f;

    int livesOrig;

    Vector3 currentVelocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        livesOrig = lives;
    }

    // Update is called once per frame
    void Update()
    {
        movement();
        rotation();
    }

    void movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        float u = 0f;
        if (Input.GetKey(KeyCode.Space)) u += 1f;
        if (Input.GetKey(KeyCode.LeftControl)) u -= 1f;

        Vector3 inputDir = (transform.right * h) + (transform.up * u) + (transform.forward * v);

        if (inputDir.magnitude > 1f)
            inputDir.Normalize();

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

    void rotation()
    {
        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Q)) rollInput -= 1f;
        if (Input.GetKey(KeyCode.E)) rollInput += 1f;

        transform.Rotate(Vector3.forward * rollInput * rollSpeed * Time.deltaTime, Space.Self);
    }

}
