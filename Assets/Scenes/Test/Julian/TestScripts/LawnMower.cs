using UnityEngine;
using UnityEngine.InputSystem;

public class LawnMower : MonoBehaviour
{
    // Todo: Add breaking if SpeedMultiplier is 0 or less if speed is positive, and opposite (if needed)
    [Tooltip("The first two in the array, correspond to the steering wheels")]
    public WheelCollider[] wheels;
    [Header("Speed")]
    [SerializeField] private float motorPower = 40f;
    private float _speedMultiplier = 0f; // range between -1 & 1, negative values will make the car go backwards.
    
    [Header("Steering")]
    public NewSteeringWheelTest steering; //_steering.outputAngle goes from -360 - 360
    [SerializeField] private float steeringPower = 10f;
    private float _steeringMultiplier = 0;
    
    [Tooltip("Change outputDivider to make the Steering reach steeringMultiplier with less turning")]
    [SerializeField] private float outputDivider = 360;

    [Header("Mass")]
    public GameObject centerOfMass;
    
    private Rigidbody _rigidbody;
    
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
        Mathf.Clamp(_speedMultiplier, -1, 1);
        Mathf.Clamp(_steeringMultiplier, -1, 1);
    }
    private void TranslateInput()
    {
        //Since theres a clamp, it wont go above 1 or below -1
        // !!BUT!! if the outputDivider is higher than 360, it will cause the SteeringMultiplier to never reach max multiplier.
        //_SteeringMultiplier = steering.outputAngle / outputDivider;
    }
    // This takes Controller input, for testing on pc !! Not for VR version!!
    // Todo: Remove before finalBuild
    void OnMove(InputValue inputValue)
    {
        _steeringMultiplier = inputValue.Get<Vector2>().x;
        _speedMultiplier = inputValue.Get<Vector2>().y;
    }
    private void FixedUpdate()
    {
        foreach (var wheel in wheels)
        {
            // TODO: Change over to power from TorqueHandle 
            wheel.motorTorque = (motorPower / 4) * _speedMultiplier;
        }
        for (int i = 0; i < wheels.Length; i++)
        {
            if (i < 2)
            {
                wheels[i].steerAngle = steeringPower * _steeringMultiplier;
            }
        }
    }
    
}
