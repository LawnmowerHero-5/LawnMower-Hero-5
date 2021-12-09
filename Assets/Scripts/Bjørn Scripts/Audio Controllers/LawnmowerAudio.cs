using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class LawnmowerAudio : MonoBehaviour
{
    [SerializeField] private NewSteeringWheelTest _wheel;
    
    private List<EventInstance?> audioSetup = new ();
    private EventInstance sfxIdle;
    
    #region - PlayLogic -
    
    private void OnDestroy()
    {
        Music.StopLoop(sfxIdle);
    }

    private void OnApplicationQuit()
    {
        Music.StopLoop(sfxIdle);
    }

    private void OnDisable()
    {
        Music.Pause(sfxIdle);
    }

    private void OnEnable()
    {
        Music.Play(sfxIdle);
    }
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        audioSetup.Add(Music.PlayLoop("SFX/lawnmower_idle", transform));

        if (audioSetup[0] != null) sfxIdle = (EventInstance) audioSetup[0];
    }

    // Update is called once per frame
    void Update()
    {
        Music.UpdateAudioPosition(sfxIdle, transform);
        
        if (_wheel.handSticked)
        {
            Music.Play(sfxIdle);
        }
        else
        {
            Music.Pause(sfxIdle);
        }
    }
}
