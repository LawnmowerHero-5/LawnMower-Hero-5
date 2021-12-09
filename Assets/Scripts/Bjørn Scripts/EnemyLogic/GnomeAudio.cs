using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;

public class GnomeAudio : MonoBehaviour
{
    private List<EventInstance?> audioSetup;
    private EventInstance sfxMove;
    private EnemyMovement move;

    // Start is called before the first frame update
    void Start()
    {
        audioSetup.Add(Music.PlayLoop("SFX/walking_grass_1", transform));

        if (audioSetup[0] != null) sfxMove = (EventInstance) audioSetup[0];
        
        move = GetComponent<EnemyMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Music.UpdateAudioPosition(sfxMove, transform);
        
        if (move.inCombat)
        {
            Music.Play(sfxMove);
            print("AUDIO");
        }
        else
        {
            Music.Pause(sfxMove);
            print("Paused");
        }
        
        
    }
}
