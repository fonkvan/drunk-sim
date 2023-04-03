using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private GameObject grabbedObject;
    private Rigidbody rb;
    public int isLeftorRight;
    public bool alreadyGrabbing = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(isLeftorRight))
        {
            print("trying to grab");
            FixedJoint fj = grabbedObject.AddComponent<FixedJoint>();
            fj.connectedBody = rb;
            fj.breakForce = 9000;

            print(grabbedObject);
            print(fj);
        }
        else if(Input.GetMouseButtonUp(isLeftorRight))
        {
            if (grabbedObject != null)
            {
                Destroy(grabbedObject.GetComponent<FixedJoint>());
                print("letting go");
            }

            grabbedObject = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ontriggerEnter");
        if (other.gameObject.CompareTag("Grabbable"))
        {
            grabbedObject = other.gameObject;
            print(grabbedObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grabbedObject = null;
    }
}
