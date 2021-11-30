using System;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class PickupAble : MonoBehaviour
{
    private HandFunctionality _currentHeldByHand;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void AttachToController(HandFunctionality handFunctionality)  //Attaches PickupAble to controllers fixedjoint (cannot haldle null because it should already be checked)
    {
        print("aieøoufh");
        //Drop if object held by another, or the same, hand
        if (_currentHeldByHand != null)
        {
            DetachFromController(_currentHeldByHand, null);
        }
        
        //Set currently held pickupable in handfunctionality
        handFunctionality.currentlyHeldPickupAble = this;
        
        //Set position to fixed joint position
        transform.position = handFunctionality.transform.position;
        

        //Attach to fixed joint
        transform.parent = handFunctionality.transform;

        //set active hand
        _currentHeldByHand = handFunctionality;
    }

    public void DetachFromController(HandFunctionality handFunctionality, SteamVR_Behaviour_Pose behaviourPose)
    {
        print("detach");
        //Detach from fixed joint
        transform.parent = null;
        
        
        //Apply controller velocity if behaviourPose is not null
        if (behaviourPose != null)
        {
            _rigidBody.velocity = behaviourPose.GetVelocity();
            _rigidBody.angularVelocity = behaviourPose.GetAngularVelocity();
        }

        //Clear held values
        handFunctionality.currentlyHeldPickupAble = null;
        _currentHeldByHand = null;
    }
}
