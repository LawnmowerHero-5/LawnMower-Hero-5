using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class BattleAudio : MonoBehaviour
{
    private EventInstance sfxBattle;

    private List<EventInstance?> audioSetup = new ();

    private bool enteredBattle;

    [SerializeField] private Music _Music;
    
    #region - PlayLogic -
    
    private void OnDestroy()
    {
        Music.StopLoop(sfxBattle);
    }

    private void OnApplicationQuit()
    {
        Music.StopLoop(sfxBattle);
    }

    private void OnDisable()
    {
        Music.Pause(sfxBattle);
    }

    private void OnEnable()
    {
        //Music.Play(sfxBattle);
    }
    
    #endregion

    private void Start()
    {
        audioSetup.Add(Music.PlayLoop("battle_theme", transform));

        if (audioSetup[0] != null) sfxBattle = (EventInstance) audioSetup[0];
    }

    private void FixedUpdate()
    {
        Music.UpdateAudioPosition(sfxBattle, transform);
        
        if (SphereMovement.EnemiesInRange >= 2)
        {
            if (!enteredBattle)
            {
                Music.StopLoop(sfxBattle);
                var i = Music.PlayLoop("battle_theme", transform);
                if (i != null) sfxBattle = (EventInstance) i;

                enteredBattle = true;
            }
            
            Music.SetParameter("InBattle", 1);
            print("SO MANY ENEMIES");
            Music.Play(sfxBattle);

            if (SphereMovement.EnemiesInRange >= 4) Music.SetParameter("Intensity", 1);
            else Music.SetParameter(("Intensity"), 0);
        }
        else
        {
            enteredBattle = false;
            
            print("nope");
            Music.SetParameter("InBattle", 0);
        }
    }
}
