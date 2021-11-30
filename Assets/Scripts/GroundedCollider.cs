using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCollider : MonoBehaviour
{
    public bool isGrounded;
    [SerializeField] Transform wreckingBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = wreckingBall.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
