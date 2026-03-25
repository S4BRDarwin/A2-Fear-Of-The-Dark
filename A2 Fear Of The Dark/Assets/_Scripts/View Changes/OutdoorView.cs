using UnityEngine;
using Unity.Cinemachine;

public class OutdoorView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ViewChangeSO viewChangeSO;
    [SerializeField] private CinemachineCameraOffset cameraOffset;
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;

    private void OnEnable()
    {
        viewChangeSO.ChangeViewOutdoor += ChangeView;
    }

    private void OnDisable()
    {
        viewChangeSO.ChangeViewOutdoor -= ChangeView;
    }

    private void ChangeView()
    {
        cameraOffset.Offset = Vector3.zero;
        orbitalFollow.Orbits.Top = new Cinemachine3OrbitRig.Orbit{Height = 0, Radius = 7};
        orbitalFollow.Orbits.Center = new Cinemachine3OrbitRig.Orbit{Height = -1, Radius = 8};
        orbitalFollow.Orbits.Bottom = new Cinemachine3OrbitRig.Orbit{Height = -2, Radius = 7};
    }
}
