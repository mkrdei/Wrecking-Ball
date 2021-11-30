using UnityEngine;

public class DistanceJoint3D : MonoBehaviour
{

    public GrapplingGun grapplingGun;
    public bool DetermineDistanceOnStart = true;
    public float distance;
    public float Spring = 0.1f;
    public float Damper = 5f;

    protected Rigidbody Rigidbody;

    void Awake()
    {
        
        Rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        /*
        if (DetermineDistanceOnStart && ConnectedRigidbody != null)
            Distance = Vector3.Distance(Rigidbody.position, ConnectedRigidbody.position);*/
    }

    void FixedUpdate()
    {

        
        


    }
}