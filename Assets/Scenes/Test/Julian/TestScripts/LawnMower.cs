using UnityEngine;
using UnityEngine.InputSystem;

public class LawnMower : MonoBehaviour
{
    // Todo: Add breaking if SpeedMultiplier is 0 or less if speed is positive, and opposite (if needed)
    [Tooltip("The first two in the array, correspond to the steering wheels")]
    public WheelCollider[] wheels;
    [Header("Speed")]
    [SerializeField] private float motorPower = 40f;
    private float _SpeedMultiplier = 0.1f; // range between -1 & 1, negative values will make the car go backwards.
    
    [Header("Steering")]
    [SerializeField] private float steeringPower = 10f;
    private float _SteeringMultiplier = 0;

    [Header("Mass")]
    public GameObject centerOfMass;
    
    private Rigidbody _rigidbody;

    private NewSteeringWheelTest _steering;



    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        _rigidbody.centerOfMass = centerOfMass.transform.localPosition;
    }
    private void Update()
    {
        // Useless if it gets the multiplier from other script !! WILL LIMIT MAX Multiplier !!
        Mathf.Clamp(_SpeedMultiplier, -1, 1);
        Mathf.Clamp(_SteeringMultiplier, -1, 1);


        _SteeringMultiplier = _steering.outputAngle / 360;
    }
    
    // This takes Controller input, for testing on pc !! Not for VR version!!
    // Todo: Remove before finalBuild
    void OnMove(InputValue inputValue)
    {
        _SteeringMultiplier = inputValue.Get<Vector2>().x;
        _SpeedMultiplier = inputValue.Get<Vector2>().y;
    }
    private void FixedUpdate()
    {
        foreach (var wheel in wheels)
        {
            // TODO: Change over to power from TorqueHandle 
            wheel.motorTorque = (motorPower / 4) * _SpeedMultiplier;
        }

        for (int i = 0; i < wheels.Length; i++)
        {
            if (i < 2)
            {
                wheels[i].steerAngle = steeringPower * _SteeringMultiplier;
            }
        }
    }
    
}
