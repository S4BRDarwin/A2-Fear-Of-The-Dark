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

    void FixedUpdate()
    {
        orientation.forward = cam.forward;
    }
}
