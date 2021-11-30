using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    private LineRenderer lr;
    public Vector3 grapplePoint;
    [SerializeField] float distanceFromPoint;
    [SerializeField] LayerMask grappables;
    public bool grappling;
    [SerializeField] Transform cam, player;
    [SerializeField] float maxDistance, spring, damper, massScale;
    private ConfigurableJoint joint;
    private Rigidbody rigidbody, playerRigidbody;
    float distanceDiscrepancy;
    private Vector3 connection, velocityTarget, projectOnConnection, grappledVelocity;
    // Start is called before the first frame update
    void Awake()
    {
        playerRigidbody = player.GetComponent<Rigidbody>();
        lr = GetComponent<LineRenderer>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        
    }
    private void Update()
    {
        
        transform.position = player.position + new Vector3(0, 0.6f, 0);
        if (Input.GetMouseButtonDown(1))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            StopGrapple();
        }
        connection = playerRigidbody.position - grapplePoint;
        distanceDiscrepancy = distanceFromPoint - connection.magnitude;
        if (connection.magnitude < distanceFromPoint)
            distanceFromPoint = connection.magnitude;
        var velocityTarget = connection + (rigidbody.velocity + Physics.gravity * spring);
        var projectOnConnection = Vector3.Project(velocityTarget, connection);
        var grappledVelocity = ((velocityTarget - projectOnConnection) / (1 + damper * Time.deltaTime));
        DrawRope();
    }
    private void FixedUpdate()
    {
        

        if (grappling)
        {
            playerRigidbody.position += distanceDiscrepancy * connection.normalized;
            playerRigidbody.velocity = new Vector3(grappledVelocity.x,playerRigidbody.velocity.y,grappledVelocity.z);
        }
        
        
    }
    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxDistance, grappables))
        {
            joint = player.gameObject.AddComponent<ConfigurableJoint>();
            joint.autoConfigureConnectedAnchor = true;

            grapplePoint = hit.point;
            grappling = true;
            distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            joint.anchor = grapplePoint;
            Debug.Log("Grappling");

            //joint.xMotion = ConfigurableJointMotion.Locked;
            //joint.yMotion = ConfigurableJointMotion.Locked;
            //joint.zMotion = ConfigurableJointMotion.Locked;

            

            
            joint.massScale = massScale;

            lr.positionCount = 2;
        }
       
    }

    void DrawRope()
    {
        if (!joint) return;
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, grapplePoint);
    }
    void StopGrapple()
    {
        grappling = false;
        lr.positionCount = 0;
        Destroy(joint);
    }
}
