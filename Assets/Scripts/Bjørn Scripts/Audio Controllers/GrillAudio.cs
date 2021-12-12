using FMOD.Studio;
using UnityEngine;

public class GrillAudio : MonoBehaviour
{
    private EventInstance? audioSetup;
    private EventInstance sfxSizzle;
    
    #region - PlayLogic -
    
    private void OnDestroy()
    {
        Music.StopLoop(sfxSizzle);
    }

    private void OnApplicationQuit()
    {
        Music.StopLoop(sfxSizzle);
    }

    private void OnDisable()
    {
        Music.Pause(sfxSizzle);
        SceneController.swappedScene -= StopAudio;
    }

    private void OnEnable()
    {
        Music.Play(sfxSizzle);
        SceneController.swappedScene += StopAudio;
    }

    private void StopAudio()
    {
        Music.StopLoop(sfxSizzle);
    }
    
    #endregion

    private void Start()
    {
        audioSetup = Music.PlayLoop("SFX/sizzle", transform);

        if (audioSetup != null) sfxSizzle = (EventInstance) audioSetup;
    }

    private void FixedUpdate()
    {
        Music.UpdateAudioPosition(sfxSizzle, transform);
    }
}