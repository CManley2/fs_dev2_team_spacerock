using UnityEngine;

public class cameraController : MonoBehaviour
{

    [SerializeField] float sens = 200f;
    [SerializeField] float rollSpeed;
    [SerializeField] bool invertY = false;

    float camRotX = 0f;
    float camRotY = 0f;

    public GameObject player;
    public CharacterController controller;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        controller = player.GetComponent<CharacterController>();

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

        float currentRoll = controller.transform.localRotation.eulerAngles.z;

        controller.transform.localRotation = Quaternion.Euler(camRotX, camRotY, currentRoll);
    }
}
