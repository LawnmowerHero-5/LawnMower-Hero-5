using PlayerPreferences;
using UnityEngine;

public class RadioDial : MonoBehaviour
{
    public float rotateSpd = 120f;
    public float channelWidth = 0.8f; //How close to the channel value you have to be for no whitenoise to appear
    public float whiteNoiseFadeWidth = 1f; //Distance from no whitenoise to full whitenoise outside each channel
    public float[] channelHertzValues;

    private float minHertz = 88f;
    private float maxHertz = 108f;
    private float dialMinAngle = -140f;
    private float dialMaxAngle = 140f;

    private float yRot;

    [SerializeField] private PlayerInput _Input;
    [SerializeField] private Music _Music;
    [SerializeField] private DataController _Data;

    private void Awake()
    {
        var angle = (_Data.hertz - minHertz) * (dialMaxAngle - dialMinAngle) / (maxHertz - minHertz);
        var rot = transform.localEulerAngles;
        
        transform.localRotation = Quaternion.Euler(rot.x, -angle - dialMinAngle, rot.z);
        yRot = -angle - dialMinAngle;
    }

    // Update is called once per frame
    void Update()
    {
        var rot = transform.localEulerAngles;
        var add = rotateSpd * Time.deltaTime;

        if (_Input.rotateDir > 0f)
        {
            if (yRot + add <= dialMaxAngle) yRot += add;
            else yRot = dialMaxAngle;
        }
        if (_Input.rotateDir < 0f)
        {
            if (yRot - add >= dialMinAngle) yRot -= add;
            else yRot = dialMinAngle;
        }
        
        transform.localRotation = Quaternion.Euler(rot.x, yRot, rot.z);

        //Calculate Hertz
        var angle = -yRot - dialMinAngle;
        _Data.hertz = minHertz + (angle / (dialMaxAngle - dialMinAngle) * (maxHertz - minHertz));
        
        //Use Hertz to select channel & whitenoise
        if (_Data.hertz <= 97.5f) _Music.SetChannel(0); //Channel at 93
        else _Music.SetChannel(1); //Channel at 102

        //Calculates whether to play whitenoise or not
        var fullWhitenoise = true;
        for (var i = 0; i < channelHertzValues.Length; i++)
        {
            var dist = Mathf.Abs(_Data.hertz - channelHertzValues[i]);
            //Plays music at full strength without whitenoise
            if (dist <= channelWidth)
            {
                _Music.SetParameter("Whitenoise", 0);
                fullWhitenoise = false;
            }
            //Plays some music and some whitenoise
            else if (dist <= channelWidth + whiteNoiseFadeWidth)
            {
                _Music.SetParameter("Whitenoise", (dist - channelWidth)/whiteNoiseFadeWidth);
                fullWhitenoise = false;
            }
        }
        //Only plays whitenoise
        if (fullWhitenoise) _Music.SetParameter("Whitenoise", 1);
    }
}
