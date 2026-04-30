using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Torch : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject torchLight;
    [SerializeField] private Light torchLightSpotlight;

    [Header("Cone Settings")]
    [SerializeField] float range = 5f;
    [SerializeField] float coneAngle = 30f;
    [SerializeField] int rayCount = 19;
    [SerializeField] LayerMask hitLayers;

    [Header("Cooldown Settings")]
    [SerializeField] float maxHoldTime = 2f;
    [SerializeField] float cooldownMultiplier = 0.5f;
    [SerializeField] float currentCooldown = 0f;

    [Header("Damage Settings")]
    [SerializeField] float damagePerTick = 10f;
    [SerializeField] float damageTickDelay = 0.1f;

    [Header("Debug")]
    [SerializeField] bool showDebugRays = true;
    
    [Header("Testing")]
    [SerializeField] private List<GameObject> currentlyDetectedEnemies = new List<GameObject>();
    [SerializeField] private float holdDuration = 0f;
    [SerializeField] private bool ableToDamage = true;

    private void Update()
    {
        // Handle cooldown timer
        if (currentCooldown > 0f)
        {
            currentCooldown -= Time.deltaTime;
            if (currentCooldown < 0f)
            {
                currentCooldown = 0f;
                ableToDamage = true;
                showDebugRays = true;
            }
                
            return; // Can't use while on cooldown
        }

        if (Input.GetButton("Fire1") && ableToDamage)
        {
            torchLight.GetComponent<MeshRenderer>().enabled = true;
            holdDuration += Time.deltaTime;
            holdDuration = Mathf.Clamp(holdDuration, 0f, maxHoldTime);

            if (holdDuration == maxHoldTime)
            {
                showDebugRays = false;
                ableToDamage = false;
            }

            // Apply damage while held
            CastCone();
        }
        else if (Input.GetButtonUp("Fire1") || (!ableToDamage)) // && currentCooldown > 0f))
        {
            // Release - calculate cooldown based on hold duration
            torchLight.GetComponent<MeshRenderer>().enabled = false;
            currentCooldown = holdDuration * cooldownMultiplier;
            if (currentCooldown < 0.5f) currentCooldown = 0.5f;
            StartCoroutine(TorchCooldownEffect());
            holdDuration = 0f;
        }
    }

    private void CastCone()
    {
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        float angleStep = coneAngle / (rayCount - 1);
        float startAngle = -coneAngle / 2f;

        // Clear and rebuild detected enemies list
        currentlyDetectedEnemies.Clear();

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * forward;

            if (Physics.Raycast(origin, direction, out RaycastHit hit, range, hitLayers))
            {
                // Add to currently detected list (avoid duplicates)
                if (!currentlyDetectedEnemies.Contains(hit.collider.gameObject))
                {
                    currentlyDetectedEnemies.Add(hit.collider.gameObject);
                }

                if (showDebugRays)
                {
                    Debug.DrawRay(origin, direction * hit.distance, Color.red, 0.01f);
                }
            }
            else
            {
                if (showDebugRays)
                {
                    Debug.DrawRay(origin, direction * range, Color.green, 0.01f);
                }
            }
        }

        // Apply damage to all currently detected enemies
        foreach (GameObject enemy in currentlyDetectedEnemies)
        {
            if (enemy != null)
            {
                EnemyHealth health = enemy.GetComponent<EnemyHealth>();
                if (health != null)
                {
                    health.StartTakeDamage((float)damagePerTick, (float)damageTickDelay);
                }
            }
        }
    }

    private IEnumerator TorchCooldownEffect()
    {
        float elapsedTime = 0f;
        float cooldownDuration = currentCooldown;
        float startIntensity = torchLightSpotlight.intensity;
        torchLightSpotlight.intensity = 0f;
        while (elapsedTime < cooldownDuration)
        {
            torchLightSpotlight.intensity = Mathf.Lerp(torchLightSpotlight.intensity, startIntensity, elapsedTime / cooldownDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebugRays) return;

        Gizmos.color = Color.yellow;
        Vector3 origin = transform.position;
        Vector3 forward = transform.forward;

        float angleStep = coneAngle / (rayCount - 1);
        float startAngle = -coneAngle / 2f;

        for (int i = 0; i < rayCount; i++)
        {
            float currentAngle = startAngle + (angleStep * i);
            Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * forward;
            Gizmos.DrawLine(origin, origin + direction * range);
        }
    }
}