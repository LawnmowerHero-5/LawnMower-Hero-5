using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public int radioOn = 1;

    private void Update()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)
        {
            if (radioOn == 1) radioOn = 0;
            else radioOn = 1;
        }
    }
}
