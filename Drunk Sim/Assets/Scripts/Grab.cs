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

                    if (armToRotate.targetRotation == naturalArmRotation)
                    {
                        StartCoroutine(RotateArm(rightGrabRotation, rightAlreadyGrabbed));
                    }

                    #if UNITY_EDITOR
                        print("trying to grab");
                    #endif

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
                            
                            #if UNITY_EDITOR
                                print("grabbed " + grabbedObject);
                            #endif
                        }

                    }

                }
                else
                {
                    Destroy(grabbedObject.GetComponent<FixedJoint>());
                    
                    #if UNITY_EDITOR
                        print("letting go");
                    #endif
                    
                    grabbedObject = null;

                    armToRotate.targetRotation = naturalArmRotation;

                    thisHand.targetRotation = naturalHandRotation;

                    if (grabbedObject == null)
                    {
                        rightAlreadyGrabbed = false;
                        
                        #if UNITY_EDITOR
                            print("dropped an object");
                        #endif
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
                    if (armToRotate.targetRotation == naturalArmRotation)
                    {
                        StartCoroutine(RotateArm(leftGrabRotation, leftAlreadyGrabbed));
                    }

                    #if UNITY_EDITOR
                    print("trying to grab");
                    #endif

                    if (grabbedObject != null)
                    {
                        FixedJoint fj = grabbedObject.AddComponent<FixedJoint>();
                        fj.connectedBody = rb;
                        fj.breakForce = 9000;

                        Vector3 newDirection = Vector3.RotateTowards(transform.forward, grabbedObject.transform.position - transform.position, Mathf.PI / 2, 0.0f);

                        thisHand.targetRotation = Quaternion.LookRotation(newDirection);

                        #if UNITY_EDITOR
                            print(grabbedObject);
                            print(fj);
                        #endif

                        if (fj.connectedBody == rb)
                        {
                            leftAlreadyGrabbed = true;
                            
                            #if UNITY_EDITOR
                                print("grabbed " + grabbedObject);
                            #endif
                        }

                    }
                }
                else
                {
                    Destroy(grabbedObject.GetComponent<FixedJoint>());
                    
                    #if UNITY_EDITOR
                        print("letting go");
                    #endif
                    
                    grabbedObject = null;

                    armToRotate.targetRotation = naturalArmRotation;

                    thisHand.targetRotation = naturalHandRotation;

                    if (grabbedObject == null)
                    {
                        leftAlreadyGrabbed = false;
                        #if UNITY_EDITOR
                            print("dropped an object");
                        #endif
                    }
                }
            }
        }
        

            
    }

    IEnumerator RotateArm(Quaternion rotationExpected, bool alreadyGrabbed)
    {
        armToRotate.targetRotation = rotationExpected;

        yield return new WaitForSeconds(3f);



        if (!alreadyGrabbed)
        {
            armToRotate.targetRotation = naturalArmRotation;

            #if UNITY_EDITOR
            print("returning to default");
            #endif

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            grabbedObject = other.gameObject;

            #if UNITY_EDITOR
                print(grabbedObject);
            #endif
        }

        if (other.gameObject.CompareTag("Beer"))
        {
            grabbedObject = other.gameObject;
            GameManager.Instance.collectedBeer = GameManager.Instance.GoalIsType("GGetAnotherBeer");
            
            #if UNITY_EDITOR
                print(grabbedObject);
            #endif
        }

        if (other.gameObject.CompareTag("Banana"))
        {
            grabbedObject = other.gameObject;
            GameManager.Instance.collectedBanana = GameManager.Instance.GoalIsType("GGetBanana");
            
            #if UNITY_EDITOR
                print(grabbedObject);
            #endif
        }
    }

    private void OnTriggerExit(Collider other)
    {
        grabbedObject = null;
    }
}
