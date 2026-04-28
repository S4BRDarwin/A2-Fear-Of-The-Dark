using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] Transform orientation;
    [SerializeField] Transform cam;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        orientation.forward = new Vector3(cam.forward.x, 0f, cam.forward.z).normalized;
    }
}
