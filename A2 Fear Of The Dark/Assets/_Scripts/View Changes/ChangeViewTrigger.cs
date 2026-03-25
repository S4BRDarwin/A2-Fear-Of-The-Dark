using System;
using UnityEngine;

public class ChangeViewTrigger : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ViewChangeSO viewChangeSO;

    [SerializeField] private ViewState viewState;

    public enum ViewState
    {
        Indoor,
        Outdoor
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (viewState == ViewState.Indoor)
            {
                viewChangeSO.RaiseEventChangeViewIndoor();
            }
            else if (viewState == ViewState.Outdoor)
            {
                viewChangeSO.RaiseEventChangeViewOutdoor();
            }
        }
    }
}
