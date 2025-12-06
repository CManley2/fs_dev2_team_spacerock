using UnityEngine;

public class cameraController : MonoBehaviour
{
    [SerializeField] float sens = 200f;
    [SerializeField] float rollSpeed;
    [SerializeField] bool invertY = false;

    float camRotX = 0f;
    float camRotY = 0f;

    [Header("Player")]
    [SerializeField] Transform player;

    void Start()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindWithTag("Player");
            if (p != null)
            {
                player = p.transform;
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (player != null)
        {
            Vector3 euler = player.localRotation.eulerAngles;
            camRotX = euler.x;
            camRotY = euler.y;
        }
    }

    void Update()
    {
        if (player == null)
            return;

        float mouseX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        camRotY += mouseX;

        if (invertY)
            camRotX += mouseY;
        else
            camRotX -= mouseY;

        float currentRoll = player.localRotation.eulerAngles.z;

        player.localRotation = Quaternion.Euler(camRotX, camRotY, currentRoll);
    }
}
