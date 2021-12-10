using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class BattleAudio : MonoBehaviour
{
    private EventInstance sfxBattle;
    private EventInstance sfxBattleIntense;
    
    private List<EventInstance?> audioSetup = new ();
    
    #region - PlayLogic -
    
    private void OnDestroy()
    {
        Music.StopLoop(sfxBattle);
        Music.StopLoop(sfxBattleIntense);
    }

    private void OnApplicationQuit()
    {
        Music.StopLoop(sfxBattle);
        Music.StopLoop(sfxBattleIntense);
    }

    private void OnDisable()
    {
        Music.Pause(sfxBattle);
        Music.Pause(sfxBattleIntense);
    }

    private void OnEnable()
    {
        //Music.Play(sfxBattle);
    }
    
    #endregion

    private void Start()
    {
        audioSetup.Add(Music.PlayLoop("battle_theme", transform));
        audioSetup.Add(Music.PlayLoop("battle_theme_intense", transform));

        if (audioSetup[0] != null) sfxBattle = (EventInstance) audioSetup[0];
        if (audioSetup[1] != null) sfxBattleIntense = (EventInstance) audioSetup[1];
    }

    private void FixedUpdate()
    {
        if (SphereMovement.EnemiesInRange >= 3)
        {
            print("SO MANY ENEMIES");
            Music.Play(sfxBattle);
            
            if (SphereMovement.EnemiesInRange >= 5) Music.Play(sfxBattleIntense);
        }
        else
        {
            print("nope");
            Music.Pause(sfxBattle);
            Music.Pause(sfxBattleIntense);
        }
    }
}
