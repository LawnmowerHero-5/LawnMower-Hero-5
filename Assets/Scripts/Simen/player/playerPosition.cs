using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerPosition : MonoBehaviour
{
    public transformVariable trans;
    
    [SerializeField] private int _frameInterval = 100;

    private playFabManager _playFabManager;
    private scoreController _scoreController;

    private void Start()
    {
        _playFabManager = GetComponent<playFabManager>();
        _scoreController = GetComponent<scoreController>();
    }

    private void Update()
    {
        if (Time.frameCount % _frameInterval == 0)
        {
            Position();
        }
    }

    private void Position()
    {
        trans.targetTransform = transform;
        _frameInterval = 100;
    }
}