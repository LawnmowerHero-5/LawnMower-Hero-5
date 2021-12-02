using System;
using Unity.Mathematics;
using UnityEngine;
using Valve.VR;

[RequireComponent(typeof(Rigidbody))]
public class PickupAble : MonoBehaviour
{
    /*
    Any object with this script can be picked up by the controller
    And will detach if enough force is applied (like when you try to move it through a wall)
    */
    [Header("Options")] [Tooltip("Child of gameobject showing where the hold point is relative to controller (null if no specification needed")] 
    [SerializeField] private Transform _holdPoint;
    [Tooltip("Force required to detach object on collisions (can be set to infinity to make unbreakable) (is applied directly to the fixed joint of controller)")]
    [SerializeField] private float _breakForceToDetach = 2500f;

    [Header("Hold Point Visualization")]
    [Tooltip("Weither to visualize mesh hold point or not")] 
    [SerializeField] private bool e_visualizeHoldPoint;
    [Tooltip("Mash of the controller holding the object")]
    [SerializeField] private Mesh e_controllerMesh;

    public float BreakForceToDetach => _breakForceToDetach;

    [NonSerialized] public HandFunctionality currentHeldByHand;
    [NonSerialized] public bool heldSinceRespawn;
    [NonSerialized] public float lastHeldFixedTime;
    
    public Rigidbody RigidBody { get; private set; }

    private void Awake()
    {
        RigidBody = GetComponent<Rigidbody>();
        //Nullsafe hold point
        _holdPoint = (_holdPoint == null) ? transform : _holdPoint;
    }

    private void OnDrawGizmos() //visualize hold point
    {
        //Calculate offset pos for position and rotation
        Transform holdPoint = (_holdPoint == null) ? transform : _holdPoint;
        Vector3 pos = transform.position + (holdPoint.position - transform.position);
        Quaternion rot = transform.rotation * holdPoint.rotation * Quaternion.Inverse(transform.rotation);
        Gizmos.DrawMesh(e_controllerMesh, pos, rot);
    }

    public void SetHoldPointToTransform(Transform trans)   //Moves the object's hold point to the transform parameter in function
    {
        RigidBody.velocity = Vector3.zero;
        RigidBody.angularVelocity = Vector3.zero;
        transform.SetPositionAndRotation(trans.position, trans.rotation);
        transform.Translate(-(_holdPoint.position - transform.position));
        transform.Rotate(-(_holdPoint.eulerAngles - transform.eulerAngles));
    }
}
