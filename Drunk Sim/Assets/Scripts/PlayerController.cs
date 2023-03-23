using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float strafeSpeed;

    public ConfigurableJoint leftLeg;
    public ConfigurableJoint rightLeg;

    private bool legLift;
    private int key;
    private float lastMouseX;
    private Rigidbody hips;

    private void Start()
    {
        hips = GetComponent<Rigidbody>();
        lastMouseX = Input.mousePosition.x;
    }
    
    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.A))
        {
            legLift = true;
            key = 97;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            legLift = true;
            key = 100;
        }
        else
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

        if (Input.mousePosition.x - lastMouseX > 0)
        {
            Lean(1f);
        }
        else if(Input.mousePosition.x - lastMouseX < 0)
        {
            Lean(-1f);
        }
    }

    void LiftLeg(int val)
    {
        if (val == 97)
        {
            RotateLeg(45f, leftLeg);
        }
        
        if (val == 100)
        {
            RotateLeg(45f, rightLeg);
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

    void Lean(float direction)
    {
        
    }

    void RotateLeg(float target, ConfigurableJoint leg)
    {
        Quaternion goal = Quaternion.Euler(target, 0f, 0f);
        leg.targetRotation = goal;
    }
}
