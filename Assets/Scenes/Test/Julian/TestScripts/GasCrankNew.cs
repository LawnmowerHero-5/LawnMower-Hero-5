using UnityEngine;
using UnityEngine.Events;

public class GasCrankNew : MonoBehaviour
{/*
    //angle threshold to trigger if we reached limit
    public float angleBetweenThreshold = 1f;
    //State of the hinge joint : either reached min or max or none if in between
    public HingeJointState hingeJointState = HingeJointState.None;

    //Event called on min reached
    public UnityEvent OnMinLimitReached;
    //Event called on max reached
    public UnityEvent OnMaxLimitReached;

    private float angleStickyOffset; // offset between wheel rotation and hand position on grab
    private Transform _handTransform;
    private bool handSticked;
    
    public Vector3 RelativePos;
    public GameObject crankBase;
    
    public enum HingeJointState { Min,Max,None}
    private HingeJoint hinge;
    
    void Start()
    {
        hinge = GetComponent<HingeJoint>();
    }
    private void OnStickedHandsChanged(InteractAble.Hand[] stickedHands)
    {
        foreach (InteractAble.Hand hand in stickedHands)
        {
            if (hand.Transform != null)
            {
                _handTransform = hand.Transform;
                CalculateOffset();
                handSticked = true;
            }
            else
            {
                _handTransform = null;
                CalculateOffset();
                handSticked = false;
            }
        }
    }
    private void CalculateOffset()
    {
        float rawAngle = CalculateRawAngle();
        angleStickyOffset = hinge.angle - rawAngle;
    }

    private float CalculateRawAngle()
    {
        RelativePos = crankBase.transform.InverseTransformPoint(_handTransform.position); // GETTING RELATIVE POSITION BETWEEN STEERING WHEEL BASE AND HAND
        
        return Mathf.Atan2( RelativePos.y, RelativePos.x) * Mathf.Rad2Deg; // GETTING CIRCULAR DATA FROM X & Z RELATIVES  VECTORS
    }

    private void FixedUpdate()
    {
        
        float angle;
        if (handSticked)
        {
            angle = CalculateRawAngle() + angleStickyOffset; // When hands are holding the wheel hand dictates how the wheel moves
            // angleSticky Offset is calculated on wheel grab - makes wheel not to rotate instantly to the users hand
        }

        hinge.angle = new Vector3(angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
        float angleWithMinLimit = Mathf.Abs(hinge.angle - hinge.limits.min);
        float angleWithMaxLimit = Mathf.Abs(hinge.angle - hinge.limits.max);

        print(hinge.angle);

        //Reached Min
        if(angleWithMinLimit < angleBetweenThreshold)
        {
            if (hingeJointState != HingeJointState.Min)
                OnMinLimitReached.Invoke();

            hingeJointState = HingeJointState.Min;
        }
        //Reached Max
        else if (angleWithMaxLimit < angleBetweenThreshold)
        {
            if (hingeJointState != HingeJointState.Max)
                OnMaxLimitReached.Invoke();

            hingeJointState = HingeJointState.Max;
        }
        //No Limit reached
        else
        {
            hingeJointState = HingeJointState.None;
        }
    }
*/}
