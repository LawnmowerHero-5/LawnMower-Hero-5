using UnityEngine;

public class SpeakerAnimation : MonoBehaviour
{
    private Animator speaker;

    private void Start()
    {
        speaker = GetComponent<Animator>();
    }

    public void SpeakerBounce()
    {
        speaker.Play("Bounce");
    }

    public void Idle()
    {
        speaker.Play("Idle");
    }
}
