using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPosition : MonoBehaviour
{
    public transformVariable trans;
    
    [SerializeField] private int _frameInterval = 100;

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