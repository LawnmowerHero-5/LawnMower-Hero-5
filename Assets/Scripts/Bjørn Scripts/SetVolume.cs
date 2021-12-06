using UnityEngine;

public class SetVolume : MonoBehaviour
{
    [SerializeField] private Music _Music;

    public void SetMasterVolume(float slider)
    {
        var val = 0f;
        if (slider >= 1) val = Mathf.Log10(slider / 100) * 20;
        else val = -80f;
        _Music.masterVolume = val;
    }
    public void SetMusicVolume(float slider)
    {
        var val = 0f;
        if (slider >= 1) val = Mathf.Log10(slider / 100) * 20;
        else val = -80f;
        _Music.musicVolume = val;
    }
    public void SetAmbianceVolume(float slider)
    {
        var val = 0f;
        if (slider >= 1) val = Mathf.Log10(slider / 100) * 20;
        else val = -80f;
        _Music.ambianceVolume = val;
    }
    public void SetSFXVolume(float slider)
    {
        var val = 0f;
        if (slider >= 1) val = Mathf.Log10(slider / 100) * 20;
        else val = -80f;
        _Music.sfxVolume = val;
    }
}
