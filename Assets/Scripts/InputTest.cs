using UnityEngine;
using UnityEngine.InputSystem;

public class InputTest : MonoBehaviour
{
    private SpeakerAnimation _RadioAnimation;

    private void Start()
    {
        _RadioAnimation = GetComponent<SpeakerAnimation>();
        print(_RadioAnimation);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.sKey.wasPressedThisFrame)
        {
            _RadioAnimation.SpeakerBounce();
        }
    }
}
