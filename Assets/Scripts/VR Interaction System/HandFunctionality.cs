using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(FixedJoint))]
[RequireComponent(typeof(HandController))]
public class HandFunctionality : MonoBehaviour
{
    /*
    Makes the controller be able to read input from HandController script, and supports following features:
    * Pick up objects with the pickUpAble script (and they will break off if you try to move them through walls)
    */
    
    [Header("Hand Options")] 
    [Tooltip("Radius of the sphere detecting pickupAbles (GrabRangeAmount)")] 
    [SerializeField] private float _grabRadius = 0.1f;
    
    public PickupAble CurrentlyHeldPickupAble { private get; set; }
    private List<PickupAble> _pickupAblesInTriggerRadius = new List<PickupAble>();

    public FixedJoint FixedJoint { get; private set; }
    private HandController _handController;

    private void Awake()
    {
        FixedJoint = GetComponent<FixedJoint>();
        _handController = GetComponent<HandController>();
    }

    private void Update()
    {
        //Pick up pickupAble if not null
        if (_handController.IsTriggerStateDown)
        {
            AttachPickupAbleToController(CheckGetClosestPickupAble());
        }
        //Drop held pickupAble if not null
        else if (_handController.IsTriggerStateUp)
        {
            DetachPickupAbleFromController(CurrentlyHeldPickupAble);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //PickupAble check add to list
        if (other.TryGetComponent(typeof(PickupAble), out Component component))
        {
            _pickupAblesInTriggerRadius.Add((PickupAble) component);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //PickupAble check remove from list
        if (other.TryGetComponent(typeof(PickupAble), out Component component))
        {
            _pickupAblesInTriggerRadius.Remove((PickupAble) component);
        }
    }

    private void OnJointBreak(float breakForce)
    {
        //Create new fixedjoint if the last one breaks
        FixedJoint = gameObject.AddComponent<FixedJoint>();
        DetachPickupAbleFromController(CurrentlyHeldPickupAble);
    }

    private void OnValidate()
    {
        //Set sphere collider trigger radus to specified value in this script
        GetComponent<SphereCollider>().radius = _grabRadius;
        //The spherecollider must be a trigger to prevent actual collisions
        SphereCollider sc = GetComponent<SphereCollider>();
        if (!sc.isTrigger)
        {
            sc.isTrigger = true;
            Debug.Log($"SphereCollider in {gameObject.name} must be a trigger. Automatically set to true by script");
        }
        //The Rigidbody on the controllers must be kinematic to prevent whacky physics when objects are picked up
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb.isKinematic == false)
        {
            rb.isKinematic = true;
            Debug.Log($"Rigidbody in {gameObject.name} must be kinematic to prevent whacky physics. Automatically set to true in script");
        }
    }

    private PickupAble CheckGetClosestPickupAble()  //Will return the closest PickupAble object or null if there is none in the collider
    {
        if (_pickupAblesInTriggerRadius.Count == 0)
        {
            return null;
        }

        //find closest pickupAble from pickupAble withing trigger list
        float closestDistance = float.MaxValue;
        PickupAble closestPickupAble = null;
        foreach (PickupAble pickupAble in _pickupAblesInTriggerRadius)
        {
            if (pickupAble == null)
            {
                continue;
            }

            float distance = Vector3.Distance(pickupAble.transform.position, transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPickupAble = pickupAble;
            }
        }

        return closestPickupAble;
    }

    private void AttachPickupAbleToController(PickupAble pickupAble)
    {
        if (pickupAble == null)
            return;
        
        //Detach pickupable from already held hand if it is already held
        if (pickupAble.currentHeldByHand != null)
        {
            DetachPickupAbleFromController(pickupAble);
        }
        
        //Attach PickupAble to controller fixed joint, and disable velocity to avoid the fixed joint breaking
        transform.SetPositionAndRotation(transform.position + pickupAble.HoldPointRelPos, transform.rotation * pickupAble.HoldPointRelRot);
        pickupAble.RigidBody.velocity = Vector3.zero;
        pickupAble.RigidBody.angularVelocity = Vector3.zero;
        FixedJoint.connectedBody = pickupAble.RigidBody;
        FixedJoint.breakForce = pickupAble.BreakForceToDetach;

        //Set held values
        CurrentlyHeldPickupAble = pickupAble;
        pickupAble.currentHeldByHand = this;
    }

    private void DetachPickupAbleFromController(PickupAble pickupAble)
    {
        if (pickupAble == null)
            return;
        
        //Detach from controller fixed joint
        pickupAble.currentHeldByHand.FixedJoint.connectedBody = null;
        
        //Set pickupAble held since respawn to true
        pickupAble.heldSinceRespawn = true;
        //reset pickupAble seconds since last held
        pickupAble.lastHeldFixedTime = Time.fixedTime;
        
        //Apply velocity of controllers
        pickupAble.RigidBody.velocity = _handController.BehaviourPose.GetVelocity();    //add check for when velocity does not need to be calculated
        pickupAble.RigidBody.angularVelocity = _handController.BehaviourPose.GetAngularVelocity();
        
        //Clear held values
        pickupAble.currentHeldByHand.CurrentlyHeldPickupAble = null;
        pickupAble.currentHeldByHand = null;
    }
}
