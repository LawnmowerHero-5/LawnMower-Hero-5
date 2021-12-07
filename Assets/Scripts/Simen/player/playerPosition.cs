using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerPosition : MonoBehaviour
{
    public transformVariable trans;
    public Rigidbody rb;
    private playFabManager _playFabManager;
    private scoreManager _scoreController;

    private void Start()
    {
        _playFabManager = GetComponent<playFabManager>();
        _scoreController = GetComponent<scoreManager>();
    }

    private void Update()
    {
        trans.playerTransform = transform;
    }

    
}