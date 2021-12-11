using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleAudio : MonoBehaviour
{
    private EventInstance sfxBattle;

    private List<EventInstance?> audioSetup = new ();

    private bool enteredBattle;

    [SerializeField] private Music _Music;
    [SerializeField] private transformVariable _playerInfo;
    
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
        SceneController.swappedScene -= StopAudio;
    }

    private void OnEnable()
    {
        SceneController.swappedScene += StopAudio;
        //Music.Play(sfxBattle);
    }

    private void StopAudio()
    {
        Music.StopLoop(sfxBattle);
    }
    
    #endregion

    private void Start()
    {
        audioSetup.Add(Music.PlayLoop("battle_theme", transform));

        if (audioSetup[0] != null) sfxBattle = (EventInstance) audioSetup[0];
        Music.Pause(sfxBattle);
    }

    private void FixedUpdate()
    {
        Music.UpdateAudioPosition(sfxBattle, transform);
        
        if (_playerInfo.enemiesInRange >= 2)
        {
            if (!enteredBattle)
            {
                Music.StopLoop(sfxBattle);
                var i = Music.PlayLoop("battle_theme", transform);
                if (i != null) sfxBattle = (EventInstance) i;

                enteredBattle = true;
            }
            
            Music.SetParameter("InBattle", 1);
            print("SO MANY ENEMIES: " + _playerInfo.enemiesInRange);
            Music.Play(sfxBattle);

            if (_playerInfo.enemiesInRange >= 4) Music.SetParameter("Intensity", 1);
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
