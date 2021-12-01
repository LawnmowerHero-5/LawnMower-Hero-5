using UnityEngine;

public class SpeakerAnimation : MonoBehaviour
{
    private Animator speaker;
    [SerializeField] public ParticleSystem _PSNotes;

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

    public void PlayPSNotes()
    {
        if (!_PSNotes.isPlaying) _PSNotes.Play();
    }

    public void StopPSNotes()
    {
        _PSNotes.Stop();
    }
}
