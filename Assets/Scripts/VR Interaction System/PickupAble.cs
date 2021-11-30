using System;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class PickupAble : MonoBehaviour
{
    [Tooltip("Force required to detach object on collisions (can be set to infinity to make unbreakable) (is applied directly to the fixed joint of controller)")]
    [SerializeField] private float breakForceToDetach = 200f;

    public float BreakForceToDetach
    {
        get
        {
            return breakForceToDetach;
        }
    }
    
    [HideInInspector]
    public HandFunctionality currentHeldByHand;

    public Rigidbody RigidBody { get; private set; }

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }
}
