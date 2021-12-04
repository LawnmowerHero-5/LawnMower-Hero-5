using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputForTesting : MonoBehaviour
{
    public float HorizontalInput;
    public float VerticalInput;

    void OnMove(InputValue inputValue)
    {
        HorizontalInput = inputValue.Get<Vector2>().x;
        print(inputValue.Get<Vector2>());
        VerticalInput = inputValue.Get<Vector2>().y;
    }
}
