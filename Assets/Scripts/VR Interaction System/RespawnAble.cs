using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RespawnAble : MonoBehaviour
{
    private enum RespawnCondition
    {
        OutOfBounds, PickupAbleDropped
    }

    [Header("Respawn Options")]
    [Tooltip("The transform the GameObject gets respawned to (Automatically set to awake position if null)")]
    [SerializeField] private Transform _repawnTransform;
    [Tooltip("Conditions that make the GameObject respawn")]
    [SerializeField] private RespawnCondition[] _respawnConditions = new RespawnCondition[1];

    [Header("Out Of Bounds Options")] 
    [Tooltip("Distance from ")]
    [SerializeField] private float _respawnDistance;

    private Rigidbody _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        //Set transform to awake transform as default if it was null in the inspector
        if (_repawnTransform == null)
        {
            _repawnTransform = transform;
        }
    }

    private void OnValidate()
    {
        //This script has no function if respawnConditions is empty, so the smallest size i 1
        if (_respawnConditions.Length < 1)
        {
            _respawnConditions = new RespawnCondition[1];
        }
    }

    public void Respawn()   //respawns the object (made public so other scripts can force respawn if functionality needed
    {
        transform.position = _repawnTransform.position;
        transform.rotation = _repawnTransform.rotation;
        _rigidBody.velocity = new Vector3(0f, 0f, 0f);
        _rigidBody.angularVelocity = new Vector3(0f, 0f, 0f);
    }
}
