using System;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class PickupAble : MonoBehaviour
{
    /*
    Any object with this script can be picked up by the controller
    And will detach if enough force is applied (like when you try to move it through a wall)
    */
    [Header("Options")]
    [Tooltip("relative position offset where object snaps to controller")]
    [SerializeField] private Vector3 _holdPointRelPos;
    [Tooltip("relative rotation offset where object snaps to controller")] 
    [SerializeField] private Vector3 _holdPointRelRot;
    [Tooltip("Force required to detach object on collisions (can be set to infinity to make unbreakable) (is applied directly to the fixed joint of controller)")]
    [SerializeField] private float _breakForceToDetach = 2500f;

    [Header("Hold Point Visualization")] 
    [Tooltip("Weither to visualize mesh hold point or not")] 
    [SerializeField] private bool e_visualizeHoldPoint;
    [Tooltip("Mash of the controller holding the object")]
    [SerializeField] private Mesh e_controllerMesh;

    public Vector3 HoldPointRelPos => _holdPointRelPos;
    public Quaternion HoldPointRelRot => Quaternion.Euler(_holdPointRelRot);
    public float BreakForceToDetach => _breakForceToDetach;

    [NonSerialized] public HandFunctionality currentHeldByHand;
    [NonSerialized] public bool heldSinceRespawn;
    [NonSerialized] public float lastHeldFixedTime;
    
    public Rigidbody RigidBody { get; private set; }

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
    }

    private void OnDrawGizmosSelected()
    {
        if (!e_visualizeHoldPoint || e_controllerMesh == null)
            return;
        Gizmos.DrawMesh(e_controllerMesh, transform.position - HoldPointRelPos, transform.rotation * HoldPointRelRot);
    }
}
