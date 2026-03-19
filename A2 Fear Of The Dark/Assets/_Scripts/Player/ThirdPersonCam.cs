using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform cam;
    [SerializeField] private Transform player;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float rotationSpeed;

    private bool climbing = false;
    private bool grounded;
    private Vector3 inputDir;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, 1f + 0.3f);
    }

    private void LateUpdate()
    {
        // rotate orientation
        Vector3 viewDir = player.position - new Vector3(cam.position.x, player.position.y, cam.position.z);
        orientation.forward = viewDir.normalized;

        // roate player object
        if (grounded && !climbing)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;
        }

        if (inputDir != Vector3.zero)
            playerObj.forward = Vector3.Slerp(playerObj.forward, inputDir.normalized, Time.deltaTime * rotationSpeed);
    }

    public void LockRotation(bool lockRot)
    {
        if (lockRot)
        {
            climbing = true;
        }
        else
        {
            climbing = false;
        }
    }
}
