using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;

public class SphereMovement : MonoBehaviour
{
    [Header("Scripts and GameObjects")]
    public Gascrank gasCrank;
    public SteeringWheel steeringWheel;
    
    [Tooltip("Wheels and dust have to be in the same order. (F.eks. Front right wheel has to be 0 for both dust and wheel)")]
    public GameObject[] wheels;
    [Tooltip("Wheels and dust have to be in the same order. (F.eks. Front right wheel has to be 0 for both dust and wheel)")]
    public VisualEffect[] dust;
    
    [Tooltip("The Rigidbody of the Sphere")]
    public Rigidbody sphereRb;
    
    [Header("Driving Modifiers")]
    public float acceleration = 8f, reverseAccel = 4f, turnSpeed = 90f;
    
    [Tooltip("Change Divider to make the Steering/Gascrank reach max value with less input")]
    public float steeringDivider = 270f, gasDivider = 20f;
    
    // Used to get a better speed feeling, whilst still having reasonable numbers in the inspector
    private const float AccelerationMultiplier = 1000f;

    // A kind of Multiplier for the forward power, and the Turning
    [HideInInspector]public float speedInput, turnInput;
    
    // Used to transform the Lawnmower to stick to the ground;
    private Quaternion _slopeRotation;
    
    [Header("LayerMasks")]
    [Tooltip("Assign the correct layer, for raycasts")]
    public LayerMask groundEquals, playerEquals;

    private LayerMask _notPlayer;
    
    [Header("Slowdown")]
    [Tooltip("The amount of slowdown per enemy in percent")]
    public float slowDownEnemy = 5f;
    [Tooltip("The amount of slowdown per wheel in the terrain(sandpit, pond etc)")]
    public float slowDownTerrain = 10f;
    //Corresponds to a speed multiplier, where 1 equals normal speed, and 0.5 is half speed.
    private float _slowDown = 1;
    //The amount of wheels slowed down, also used to make the lawnmower even slower in the pond
    private int _slowedWheels;
    [HideInInspector] public static int EnemiesInRange;
    
    //Int made to index if a wheel is on bad terrain or not
    private int[] _badWheels = new int[4];
    

    private void Start()
    {
        //Sets the sphere free, as to not be the child of the lawnmower, which would have made all movement be a sort of "Double" movement
        sphereRb.transform.parent = null;
        for (int i = 0; i < dust.Length - 1; i++)
        {
            dust[i].Stop();
        }
        _notPlayer =~playerEquals;
    }

    #region ControllerInput
    
    private void OnMove(InputValue inputValue)
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

    private void OnLook(InputValue inputValue)
    {
        //Same as "OnMove()", but for turning
        turnInput = inputValue.Get<Vector2>().x;
    }
    
    #endregion

    
    #region VR Input
    
    private void Update()
    {
        //TranslateSteering();
    }
    
    private void TranslateSteering()
    {
        //Since theres a clamp, it wont go above 1 or below -1
        // !!BUT!! if the outputDivider is higher than 360, it will cause the SteeringMultiplier to never reach max multiplier.
        turnInput = (steeringWheel.outputAngle / steeringDivider);
        Mathf.Clamp(turnInput, -1, 1);
        
        //Complex formula to turn the Lawnmower, that stops the lawnmower from turning when standing still (due to speedInput)
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, turnInput * turnSpeed * Time.deltaTime * speedInput/acceleration, 0f));
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
    
    #endregion
    
    
    private void FixedUpdate()
    {
        //Sends a raycast from the middle of the vehicle to get the slope rotation, to be able to rotate to fit
        RayCast();
        
        //Gets info from the wheels, such as if they're on hard terrain
        WheelCast(_notPlayer);
        SlowDown();
        Drive();
    }
    private void RayCast()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, -transform.up, out hit, 2f, groundEquals);
        // Gets the slopeRotation through the raycast, then Slerps (Smooths out) the rotation and then sets the lawnmowers rotation to the ground rotation
        _slopeRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, _slopeRotation, 1 * Time.deltaTime);

    }

    
    #region Slowing
    
    private void WheelCast(LayerMask layerMask)
    {
        RaycastHit hit;
        for (var i = 0; i < wheels.Length; i++)
        {
            if (Physics.Raycast(wheels[i].transform.position, Vector3.down, out hit, 3000f, layerMask))
            {
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("SandPit"))
                {
                    dust[i].Play();
                    _badWheels[i] = 1;
                    //print("Colliding with Sandpit");
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Pond"))
                {
                    _badWheels[i] = 2;
                    //PondVFX Todo
                    //print("Colliding with Pond");
                }
                else if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    //GrassVFX todo
                    dust[i].Stop();
                    _badWheels[i] = 0;
                    //print("Colliding with ground");
                }
            }
        }
    }
    private void SlowDown()
    {
        
        var result = 0;
        for (var i = 0; i < _badWheels.Length; i++)
        {
            if (_badWheels[i] == 1)
            {
                result += 1;
            }
            _slowedWheels = result;
        }
        
        _slowDown = 1 - (((EnemiesInRange * slowDownEnemy)/100) + ((_slowedWheels * slowDownTerrain)/100));
        Mathf.Clamp(_slowDown, 0.1f, 1f);
    }
    
    #endregion
    
    private void Drive()
    {
        //The function that propels the sphere forward
        if (Mathf.Abs(speedInput) > 0)
        {
            sphereRb.AddForce(transform.forward * speedInput * AccelerationMultiplier * _slowDown);
        }
        transform.position = sphereRb.transform.position;
    }
}
