using FMOD.Studio;
using UnityEngine;

public class WaspAudio : MonoBehaviour
{
    private EventInstance sfxBuzz;
    
    #region - PlayLogic -
    
    private void OnDestroy()
    {
        Music.StopLoop(sfxBuzz);
    }

    private void OnApplicationQuit()
    {
        Music.StopLoop(sfxBuzz);
    }

    private void OnDisable()
    {
        Music.Pause(sfxBuzz);
    }

    private void OnEnable()
    {
        Music.Play(sfxBuzz);
    }
    
    #endregion
    
    void Start()
    {
        var inst = Music.PlayLoop("SFX/Buzz", transform);

        if (inst != null) sfxBuzz = (EventInstance) inst;
    }
    
    private void Update()
    {
        Music.UpdateAudioPosition(sfxBuzz, transform);
    }
}
