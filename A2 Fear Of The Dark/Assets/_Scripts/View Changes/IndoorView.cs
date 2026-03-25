using Unity.Cinemachine;
using UnityEngine;

public class IndoorView : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ViewChangeSO viewChangeSO;
    [SerializeField] private CinemachineCameraOffset cameraOffset;
    [SerializeField] private CinemachineOrbitalFollow orbitalFollow;

    [Header("Settings")]
    [SerializeField] private float topRadius;
    [SerializeField] private float centerRadius;
    [SerializeField] private float bottomRadius;


    private void OnEnable()
    {
        viewChangeSO.ChangeViewIndoor += ChangeView;
    }

    private void OnDisable()
    {
        viewChangeSO.ChangeViewIndoor -= ChangeView;
    }

    private void ChangeView()
    {

        cameraOffset.Offset = new Vector3(-0.5f, 0, 0);
        orbitalFollow.Orbits.Top = new Cinemachine3OrbitRig.Orbit{Height = -2f, Radius = topRadius};
        orbitalFollow.Orbits.Center = new Cinemachine3OrbitRig.Orbit{Height = -2.5f, Radius = centerRadius};
        orbitalFollow.Orbits.Bottom = new Cinemachine3OrbitRig.Orbit{Height = -3f, Radius = bottomRadius};
    }
}