using UnityEngine;
using System.Collections;

public class LedgeClimb : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private LayerMask climbableWallLayer;
    [SerializeField] private LayerMask climbableLedgeLayer;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ledge Climb Settings")]
    [SerializeField] private float verticalOffset = 1.5f;
    [SerializeField] private float maxClimbHeight = 2.5f;
    [SerializeField] private float wallCheckDistance = 1f;
    [SerializeField] private float climbDuration = 1f; 

    [Header("Debug")]
    [SerializeField] private Color checkWallRayColour = Color.red;
    [SerializeField] private Color checkHeightRayColour = Color.blue;

    private float playerHeight;
    private bool ableToClimb = false;
    private bool isClimbing = false;
    private Vector3 climbStartPos;
    private Vector3 climbEndPosY;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + playerObj.forward * wallCheckDistance);
    }

    void Start()
    {
        if (Physics.Raycast(ledgeCheck.position, Vector3.down, out RaycastHit groundHit, 10f, groundLayer))
        {
            playerHeight = groundHit.distance * 2f;
        }
        else
        {
            playerHeight = 2f;
        }
    }

    void Update()
    {
        ledgeCheck.rotation = playerObj.rotation;

        if(!isClimbing)
        {
            CheckAbleToClimb();
        }

        if (ableToClimb && Input.GetKeyDown(KeyCode.Space) && !isClimbing)
        {
            isClimbing = true;
            Climb();
        }
    }

    private void CheckAbleToClimb()
    {
        ableToClimb = false;

        Vector3 checkDirection = new Vector3(playerObj.forward.x, 0f, playerObj.forward.z).normalized;
        Vector3 origin = ledgeCheck.position;

        Debug.DrawLine(origin, origin + checkDirection * wallCheckDistance, Color.yellow, 0.1f);

        if (Physics.Raycast(origin, checkDirection, out RaycastHit wallHit, wallCheckDistance, climbableWallLayer))
        {
            Debug.DrawLine(origin, wallHit.point, checkWallRayColour, 0.1f);

            Vector3 heightCheckRayOrigin = wallHit.point + Vector3.up * 0.05f;
            Debug.DrawLine(heightCheckRayOrigin, heightCheckRayOrigin + Vector3.up * maxClimbHeight, Color.gray, 0.1f);

            if (Physics.Raycast(heightCheckRayOrigin, Vector3.up, out RaycastHit topHit, maxClimbHeight, climbableLedgeLayer))
            {
                Debug.DrawLine(heightCheckRayOrigin, topHit.point, checkHeightRayColour, 0.1f);

                float wallHeight = topHit.point.y - wallHit.point.y;
                ableToClimb = true;
                climbEndPosY = new Vector3(playerObj.position.x, topHit.point.y + verticalOffset + 0.05f, playerObj.position.z);
            }
        }
    }

    private void Climb()
    {
        climbStartPos = transform.position;
        Vector3 climbTarget = climbEndPosY;
        StartCoroutine(ClimbCoroutine(climbTarget));
    }

    private IEnumerator ClimbCoroutine(Vector3 targetPos)
    {
        float elapsedTime = 0f;

        while (elapsedTime < climbDuration)
        {
            transform.position = Vector3.Lerp(climbStartPos, targetPos, elapsedTime / climbDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;

        Vector3 forwardOffset = ledgeCheck.forward * 0.5f;
        Vector3 finalTarget = targetPos + forwardOffset;
        float forwardDuration = 0.25f;
        elapsedTime = 0f;

        while (elapsedTime < forwardDuration)
        {
            transform.position = Vector3.Lerp(targetPos, finalTarget, elapsedTime / forwardDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = finalTarget;
        isClimbing = false;
        ableToClimb = false;
    }

}
