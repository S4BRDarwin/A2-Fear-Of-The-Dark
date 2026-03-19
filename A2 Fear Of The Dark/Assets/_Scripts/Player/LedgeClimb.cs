using UnityEngine;
using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;

public class LedgeClimb : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ThirdPersonCam thirdPersonCam;
    [SerializeField] private Transform playerObj;
    [SerializeField] private Transform ledgeCheck;
    [SerializeField] private LayerMask climbableWallLayer;
    [SerializeField] private LayerMask climbableLedgeLayer;
    [SerializeField] private LayerMask groundLayer;

    [Header("Ledge Climb Settings")]
    [SerializeField] private float verticalOffset = 0.1f;
    [SerializeField] private float maxClimbHeight = 2.5f;
    [SerializeField] private float wallCheckDistance = 1f;
    [SerializeField] private float climbDuration = 1f; 

    [Header("Debug")]
    [SerializeField] private Color checkWallRayColour = Color.red;
    [SerializeField] private Color checkHeightRayColour = Color.blue;

    private bool ableToClimb = false;
    private bool isClimbing = false;
    private Vector3 faceNormal = Vector3.zero;
    private Vector3 topHitPoint = Vector3.zero;
    private Vector3 wallHitSpot;
    private Vector3 climbStartPos;
    private Vector3 climbEndPosY;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + playerObj.forward * wallCheckDistance);
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

            wallHitSpot = wallHit.point;
            faceNormal = wallHit.normal;

            Vector3 heightCheckRayOrigin = wallHit.point + Vector3.up * 0.05f;
            Debug.DrawLine(heightCheckRayOrigin, heightCheckRayOrigin + Vector3.up * maxClimbHeight, Color.gray, 0.1f);

            if (Physics.Raycast(heightCheckRayOrigin, Vector3.up, out RaycastHit topHit, maxClimbHeight, climbableLedgeLayer))
            {
                Debug.DrawLine(heightCheckRayOrigin, topHit.point, checkHeightRayColour, 0.1f);
                topHitPoint = topHit.point;
                ableToClimb = true;
            }
        }
    }

    private void Climb()
    {
        thirdPersonCam.LockRotation(true);
        StartCoroutine(FaceWall());
        
        StartCoroutine(ClimbCoroutine());
    }

    private IEnumerator ClimbCoroutine()
    {
        float elapsedTime = 0f;
        float readyDuration = 0.25f;

        while (elapsedTime < readyDuration)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(wallHitSpot.x, transform.position.y, wallHitSpot.z), elapsedTime / readyDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.1f);

        climbStartPos = transform.position;
        climbEndPosY = new Vector3(transform.position.x, topHitPoint.y + verticalOffset + 0.05f, transform.position.z);
        Vector3 climbTarget = climbEndPosY;

        while (elapsedTime < climbDuration)
        {
            transform.position = Vector3.Lerp(climbStartPos, climbTarget, elapsedTime / climbDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Vector3 forwardOffset = ledgeCheck.forward * 0.5f;
        Vector3 finalTarget = climbTarget + forwardOffset;
        float forwardDuration = 0.25f;
        elapsedTime = 0f;

        while (elapsedTime < forwardDuration)
        {
            transform.position = Vector3.Lerp(climbTarget, finalTarget, elapsedTime / forwardDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isClimbing = false;
        thirdPersonCam.LockRotation(false);
        ableToClimb = false;
    }

    private IEnumerator FaceWall()
    {
        Vector3 targetDirection = -faceNormal;
        targetDirection.y = 0f;
        targetDirection.Normalize();
        
        Quaternion startRotation = playerObj.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        float elapsedTime = 0f;
        float duration = 0.2f;

        while (elapsedTime < duration)
        {
            playerObj.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = targetRotation;
    } 
}