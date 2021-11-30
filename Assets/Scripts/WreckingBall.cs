using UnityEngine;
using System.Collections;
using System;

public class WreckingBall : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementSpeed, rollSpeed, torq, jumpSpeed, grapplingSpeed;
    [SerializeField] Camera fpsCamera;
    Vector3 movingRotation;
    private bool jumping, grappling;
    public Vector3 movement;
    ParticleEffects particleEffects;
    private Vector3 verticalMovement, horizontalMovement, relativeMovementDirection, lastVelocity, lastAngularVelocity;
    [SerializeField] GroundedCollider groundedCollider;
    [SerializeField] GrapplingGun grapplingGun;

    // Start is called before the first frame update
    void Start()
    {
        particleEffects = new ParticleEffects();
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame

    void Update()
    {
        horizontalMovement = fpsCamera.transform.up * -Input.GetAxis("Horizontal");
        verticalMovement = fpsCamera.transform.right * Input.GetAxis("Vertical");
        relativeMovementDirection = ((fpsCamera.transform.forward * Input.GetAxis("Vertical")) + (fpsCamera.transform.right * Input.GetAxis("Horizontal"))).normalized;
        movingRotation = horizontalMovement + verticalMovement;
        grappling = grapplingGun.grappling;
        

    }
    private void FixedUpdate()
    {

        movement = new Vector3(relativeMovementDirection.x * movementSpeed, 0, relativeMovementDirection.z * movementSpeed) * (grappling ? grapplingSpeed : 1);


        /*float forceMagnitude = (rb.velocity + new Vector3(relativeMovementDirection.x * movementSpeed, rb.velocity.y, relativeMovementDirection.z * movementSpeed)).magnitude;
        Debug.Log("forceMagnitude: " + forceMagnitude);

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero; 
            */


        Vector3 grapplingUpperForce;
        Vector3 horizontalVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Debug.Log("Velocity: " + horizontalVelocity.magnitude);
        if(grappling && horizontalVelocity.magnitude > 8 && relativeMovementDirection.magnitude>0)
        {
            particleEffects.playParticle("Flaming");
            grapplingUpperForce = (relativeMovementDirection + Vector3.up)/5;
        } else
        {
            
            particleEffects.stopParticle("Flaming");
            grapplingUpperForce = Vector3.zero;
        }
        
        rb.velocity = new Vector3(movement.x, rb.velocity.y, movement.z) + grapplingUpperForce;
        lastVelocity = rb.velocity;
        lastAngularVelocity = rb.angularVelocity;
       
        if (rb.angularVelocity.magnitude < 2 && groundedCollider.isGrounded)
        {
            rb.angularDrag = 5;
        }
        else
        {
            rb.angularDrag = 0.05f;
        }
        
        



        if (Input.GetKey(KeyCode.Space) && groundedCollider.isGrounded && !jumping)
        {
            StartCoroutine("Jump");
        }
    }

    IEnumerator Jump()
    {
        jumping = true;
        rb.velocity += Vector3.up * jumpSpeed;
        yield return new WaitForSeconds(0.5f);
        jumping = false;

    }
}
