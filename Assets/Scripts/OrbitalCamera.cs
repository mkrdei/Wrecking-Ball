using UnityEngine;
using Cinemachine;
public class OrbitalCamera : MonoBehaviour
{

    [SerializeField] private float sensitivityY;
    [SerializeField] private float sensitivityScroll;
    [SerializeField] private float maxZoom = 100;
    [SerializeField] private float minZoom = 20;
    [SerializeField] private float maxYOrbit = 20;
    private CinemachineOrbitalTransposer vcam;
    private float y;
    private float scroll;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineOrbitalTransposer>();
        scroll = -40;
        y = 10;
    }
    void Update()
    {

        y -= +Input.GetAxis("Mouse Y") * sensitivityY;
        y = Mathf.Clamp(y, -maxYOrbit, maxYOrbit);
        vcam.m_FollowOffset.y = y;
        scroll += Input.GetAxis("Mouse ScrollWheel") * sensitivityScroll;
        scroll = Mathf.Clamp(scroll, -maxZoom, -minZoom);
        vcam.m_FollowOffset.x = scroll;
        vcam.m_FollowOffset.z = scroll;
    }
}
