using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;

    public ConfigurableJoint leftLeg;
    public ConfigurableJoint rightLeg;
    public ConfigurableJoint upperSpine;
    
    //public Rigidbody rb

    private bool legLift;
    private bool leaning;
    private int key;
    private float lastMouseX;
    private Rigidbody hips;
    private ConfigurableJoint hipJoint;

    private void Start()
    {
        hips = GetComponent<Rigidbody>();
        hipJoint = GetComponent<ConfigurableJoint>();
        lastMouseX = Input.mousePosition.x;
        legLift = false;
        leaning = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }
    
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A) && !legLift)
        {
            legLift = true;
            key = 97;
        }
        else if (Input.GetKey(KeyCode.D) && !legLift)
        {
            legLift = true;
            key = 100;
        }
        else if (!(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && legLift)
        {
            legLift = false;
        }

        if (legLift)
        {
            LiftLeg(key);
        }
        else
        {
            DropLeg(key);
        }
        
        if (Input.GetKey(KeyCode.W))
        {
            hips.AddForce(hips.transform.forward * speed * Time.deltaTime);
        }

        Vector3 leanDir = Vector3.zero;
        if (Mathf.Approximately(Input.mousePosition.x, lastMouseX))
        {
            leaning = false;
        }
        else if (Input.mousePosition.x - lastMouseX > Mathf.Epsilon)
        {
            leanDir = hips.transform.right;
            leaning = true;
        }
        else if(Input.mousePosition.x - lastMouseX < Mathf.Epsilon)
        {
            leanDir = -hips.transform.right;
            leaning = true;
        }

        if (leaning && Debug.isDebugBuild)
        {
            Debug.Log("LEANING");
        }
        Lean(leanDir);
        lastMouseX = Input.mousePosition.x;
    }

    void LiftLeg(int val)
    {
        if (val == (int)KeyCode.A)
        {
            RotateLeg(-65f, leftLeg);
        }
        
        if (val == (int)KeyCode.D)
        {
            RotateLeg(-65f, rightLeg);
        }
    }

    void DropLeg(int val)
    {
        if (val == 97)
        {
            RotateLeg(0f, leftLeg);
        }
        
        if (val == 100)
        {
            RotateLeg(0f, rightLeg);
        }
    }

    void Lean(Vector3 direction)
    {
        if (direction == Vector3.zero)
        {
            Quaternion goal = Quaternion.Euler(0f, 0f, 0f);
            upperSpine.targetRotation = goal;
        }
        //if leaning right
        else if (Mathf.Approximately(Vector3.Dot(hips.transform.right, direction), 1f))
        {
            Quaternion goal = Quaternion.Euler(0f, 0f, 15f);
            upperSpine.targetRotation = goal;
            //lerp to (0, ++current, 0) for hips
            Quaternion hipGoal = hipJoint.targetRotation * Quaternion.Euler(0f, -1f, 0f);
            hipJoint.targetRotation = hipGoal;
        }
        //leaning left
        else
        {
            Quaternion goal = Quaternion.Euler(0f, 0f, -15f);
            upperSpine.targetRotation = goal;
            //lerp to (0, --current, 0)
            Quaternion hipGoal = hipJoint.targetRotation * Quaternion.Euler(0f, 1f, 0f);
            hipJoint.targetRotation = hipGoal;
        }
    }

    void RotateLeg(float target, ConfigurableJoint leg)
    {
        Quaternion goal = Quaternion.Euler(target, 0f, 0f);
        leg.targetRotation = goal;
    }
}
