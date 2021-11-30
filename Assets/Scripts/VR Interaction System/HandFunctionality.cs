using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(HandController))]
public class HandFunctionality : MonoBehaviour
{
    //[HideInInspector]
    public PickupAble currentlyHeldPickupAble;
    
    private List<PickupAble> _pickupAblesInTriggerRadius = new List<PickupAble>();
    
    private HandController _handController;

    private void Awake()
    {
        _handController = GetComponent<HandController>();
    }

    private void Update()
    {
        //Pick up pickupable if not null
        if (_handController.IsTriggerStateDown)
        {
            print("trigger state DOWN");
            PickupAble closestPickupAble = CheckGetClosestPickupAble();
            if (closestPickupAble != null)
            { 
                closestPickupAble.AttachToController(this);
            }
        }
        
        //Drop held pickupable if not null
        else if (_handController.IsTriggerStateUp)
        {
            print("Trigger state UP");
            if (currentlyHeldPickupAble != null)
            {
                currentlyHeldPickupAble.DetachFromController(this, _handController.BehaviourPose);
            }
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

    private void OnValidate()
    {
        //The spherecollider must be a trigger to prevent actual collisions
        SphereCollider sc = GetComponent<SphereCollider>();
        if (!sc.isTrigger)
        {
            sc.isTrigger = true;
            Debug.Log($"SphereCollider in {gameObject.name} must be a trigger. Automatically set to true by script");
        }
    }

    private PickupAble CheckGetClosestPickupAble()  //Will return the closest PickupAble object or null if there is none in the collider
    {
        if (_pickupAblesInTriggerRadius.Count == 0)
        {
            return null;
        }

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
}
