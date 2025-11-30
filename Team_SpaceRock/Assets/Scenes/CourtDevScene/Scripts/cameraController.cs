using UnityEngine;

public class cameraController : MonoBehaviour
{

    [SerializeField] float sens = 200f;
    [SerializeField] float rollSpeed = 1f;
    [SerializeField] bool invertY = false;

    float camRotX = 0f;
    float camRotY = 0f;
    float rollRot = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * sens * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * sens * Time.deltaTime;

        camRotY += mouseX;

        if (invertY)
            camRotX += mouseY;
        else
            camRotX -= mouseY;

        float rollInput = 0f;
        if (Input.GetKey(KeyCode.E)) rollInput -= 0.5f;
        if (Input.GetKey(KeyCode.Q)) rollInput += 0.5f;
        rollRot += rollInput * rollSpeed * Time.deltaTime;

        transform.parent.localRotation = Quaternion.Euler(camRotX, camRotY, rollRot);
    }
}
