using UnityEngine;

public class RadioAnimation : MonoBehaviour
{
    [HideInInspector] public Animator radio;

    private void Start()
    {
        radio = GetComponent<Animator>();
    }

    public void SpeakerJumpLeft()
    {
        radio.Play("JumpLeftShort");
    }
    public void SpeakerJumpFarRight()
    {
        radio.Play("JumpFarRight");
    }
    public void SpeakerJumpFarLeft()
    {
        radio.Play("JumpFarLeft");
    }

    public void Idle()
    {
        radio.Play("Idle");
    }
}
