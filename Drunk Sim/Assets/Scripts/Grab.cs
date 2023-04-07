using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    private GameObject grabbedObject;
    private Rigidbody rb;
    public ConfigurableJoint armToRotate;
    private ConfigurableJoint thisHand;

    public bool leftAlreadyGrabbed = false;
    public bool rightAlreadyGrabbed = false;
    public int isLeftOrRight;

    private Quaternion naturalArmRotation = Quaternion.Euler(310f, 0f, 0f);
    private Quaternion leftGrabRotation = Quaternion.Euler(10f, 310f, 60f);
    private Quaternion rightGrabRotation = Quaternion.Euler(350f, 50f, 300f);

    private Quaternion naturalHandRotation = Quaternion.identity;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thisHand = GetComponent<ConfigurableJoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLeftOrRight == 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!rightAlreadyGrabbed)
                {
                    armToRotate.targetRotation = rightGrabRotation;

                    print("trying to grab");

                    if (grabbedObject != null)
                    {
                        FixedJoint fj = grabbedObject.AddComponent<FixedJoint>();
                        fj.connectedBody = rb;
                        fj.breakForce = 9000;

                        thisHand.targetRotation = grabbedObject.transform.rotation;
                        

                        print(grabbedObject);
                        print(fj);

                        if (fj.connectedBody == rb)
                        {
                            rightAlreadyGrabbed = true;
                            print("grabbed " + grabbedObject);
                        }
                    }

                }
                else
                {
                    Destroy(grabbedObject.GetComponent<FixedJoint>());
                    print("letting go");
                    grabbedObject = null;

                    armToRotate.targetRotation = naturalArmRotation;

                    thisHand.targetRotation = naturalHandRotation;

                    if (grabbedObject == null)
                    {
                        rightAlreadyGrabbed = false;
                        print("dropped an object");
                    }
                }
            }

        }
        else if (isLeftOrRight == 0)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!leftAlreadyGrabbed)
                {
                    armToRotate.targetRotation = leftGrabRotation;

                    print("trying to grab");

                    if (grabbedObject != null)
                    {
                        FixedJoint fj = grabbedObject.AddComponent<FixedJoint>();
                        fj.connectedBody = rb;
                        fj.breakForce = 9000;

                        Vector3 newDirection = Vector3.RotateTowards(transform.forward, grabbedObject.transform.position - transform.position, Mathf.PI / 2, 0.0f);

                        thisHand.targetRotation = Quaternion.LookRotation(newDirection);

                        print(grabbedObject);
                        print(fj);

                        if (fj.connectedBody == rb)
                        {
                            leftAlreadyGrabbed = true;
                            print("grabbed " + grabbedObject);
                        }
                    }


                }
                else
                {
                    Destroy(grabbedObject.GetComponent<FixedJoint>());
                    print("letting go");
                    grabbedObject = null;

                    armToRotate.targetRotation = naturalArmRotation;

                    thisHand.targetRotation = naturalHandRotation;

                    if (grabbedObject == null)
                    {
                        leftAlreadyGrabbed = false;
                        print("dropped an object");
                    }
                }
            }
        }
        

            
    }

    private void OnTriggerEnter(Collider other)
    {
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
