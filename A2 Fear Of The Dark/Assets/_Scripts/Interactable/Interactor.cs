using TMPro;
using UnityEngine;

interface IInteractable
{
    [SerializeField] public string promptMessage { get; }
    void Interact();
}

public class Interactor : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Transform interactOriginPoint;
    [SerializeField] private float interactDistance = 2f;

    void Update()
    {
        promptText.text = string.Empty;
        Ray ray = new Ray(interactOriginPoint.position, interactOriginPoint.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance);
        RaycastHit hitInfo;
        
        if (Physics.Raycast(ray, out hitInfo, interactDistance))
        {
            if (hitInfo.collider.TryGetComponent(out IInteractable interactableObj))
            {
                promptText.text = interactableObj.promptMessage;
                if (Input.GetKeyDown(KeyCode.E) && interactableObj != null)
                {
                    interactableObj.Interact();
                }
            }
        }
    }
}
