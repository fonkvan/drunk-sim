using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    public ConfigurableJoint leftLeg;
    public ConfigurableJoint rightLeg;
    public ConfigurableJoint upperSpine;

    private AudioSource footstepsAudio;
    
    //public Rigidbody rb

    private bool legLift;
    private bool leaning;
    private int key;
    private Rigidbody hips;
    private ConfigurableJoint hipJoint;

    private void Start()
    {
        hips = GetComponent<Rigidbody>();
        hipJoint = GetComponent<ConfigurableJoint>();
        legLift = false;
        leaning = false;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;

        footstepsAudio = GetComponent<AudioSource>();
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
            if (!footstepsAudio.isPlaying)
            {
                print("playing footsteps");
                footstepsAudio.Play();
            }
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
        if (Input.GetAxisRaw("Mouse X") > 0)
        {
            leanDir = hips.transform.right;
            leaning = true;
        }
        else if (Input.GetAxisRaw("Mouse X") < 0)
        {
            leanDir = -hips.transform.right;
            leaning = true;
        }
        else
        {
            leaning = false;
        }
        
        Lean(leanDir);
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Waypoint"))
        {
            
        }
    }
}
