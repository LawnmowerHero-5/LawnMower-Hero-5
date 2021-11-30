using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPosition : MonoBehaviour
{
    public transformVariable trans;

    private void Update()
    {
        trans.targetTransform = transform;
    }
}
