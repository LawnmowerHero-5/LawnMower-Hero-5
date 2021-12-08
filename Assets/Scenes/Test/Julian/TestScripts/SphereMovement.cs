using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class SphereMovement : MonoBehaviour
{
    [Tooltip("The Rigidbody of the Sphere")]
    public Rigidbody sphereRB;
    
    public float acceleration = 8f, reverseAccel = 4f, maxSpeed = 50f, turnSpeed = 90f;

    // Used to get a better speed feeling, whilst still having reasonable numbers in the inspector
    private float accelerationMultiplier = 1000f;

    // A kind of Multiplier for the forward power, and the Turning
    private float speedInput, turnInput;
    
    // Used to transform the Lawnmower to stick to the ground;
    private Quaternion slopeRotation;
    
    [Tooltip("Assign the Ground layer")]
    public LayerMask groundEquals;

    public testGASCRANK gasCrank;
    public NewSteeringWheelTest steeringWheel;
    [Tooltip("Change Divider to make the Steering/Gascrank reach max value with less input")]
    public float steeringDivider = 270, gasDivider = 20;
    
    
    void Start()
    {
        //Sets the sphere free, as to not be the child of the lawnmower, which would have made all movement be a sort of "Double" movement
        sphereRB.transform.parent = null;
    }
    
    void OnMove(InputValue inputValue)
    {
        // Here it gets input from the New Player Input, adds acceleration/ reverse acceleration based on if the vector is positive or negative (This is for controllers, and not from the VR interaction)
        if (inputValue.Get<Vector2>().x > 0)
        {
            speedInput = inputValue.Get<Vector2>().y * acceleration;
        }
        else
        {
            speedInput = inputValue.Get<Vector2>().y * reverseAccel;
        }
    }

    void OnLook(InputValue inputValue)
    {
        //Same as "OnMove()", but for turning
        turnInput = inputValue.Get<Vector2>().x;
    }

    void Update()
    {
        TranslateSteering();
        Mathf.Clamp(turnInput, -1, 1);
        transform.position = sphereRB.transform.position;
        
        //Complex formula to turn the Lawnmower, that stops the lawnmower from turning when standing still (due to speedInput)
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * speedInput/acceleration, 0f));
        RayCast();
    }
    
    private void TranslateSteering()
    {
        //Since theres a clamp, it wont go above 1 or below -1
        // !!BUT!! if the outputDivider is higher than 360, it will cause the SteeringMultiplier to never reach max multiplier.
        turnInput = (steeringWheel.outputAngle / steeringDivider);
        float tempSpeed = (gasCrank.outputAngle / gasDivider);
        if (tempSpeed > 0)
        {
            speedInput = tempSpeed * acceleration;
        }
        else
        {
            speedInput = tempSpeed * reverseAccel;
        }
    }

    private void RayCast()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 2f, groundEquals);
        // Gets the slopeRotation through the raycast, then Slerps (Smooths out) the rotation and then sets the lawnmowers rotation to the ground rotation
        slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, 1 * Time.deltaTime);

    }
    private void FixedUpdate()
    {
        //The function that propels the sphere forward
        if (Mathf.Abs(speedInput) > 0)
        {
            sphereRB.AddForce(transform.forward * speedInput * accelerationMultiplier);
        }
        
    }
}
