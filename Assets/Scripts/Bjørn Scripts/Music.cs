using FMODUnity;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Runtime.InteropServices;
using FMOD;
using FMOD.Studio;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class Music : MonoBehaviour
{
    public static Music instance;
    public Transform playAtPos;

    [HideInInspector] public float masterVolume = 1f;
    [HideInInspector] public float musicVolume = 1f;
    [HideInInspector] public float ambianceVolume = 1f;
    [HideInInspector] public float sfxVolume = 1f;

    //Channel variables
    public int channelCount;
    private int currentChannel;

    //Start point of channels
    public float[] channelStart;

    //The total length of the different channel tracks
    public float[] channelLength;
  
    //Keeps track of how long a channel has played for
    [SerializeField] private float[] channelCurrentTime;

    //Which event is played by the musicInstance
    [SerializeField] [EventRef] private string music;

    public TimelineInfo timelineInfo;
    private GCHandle timelineHandle;

    private EVENT_CALLBACK beatCallback;
    private EventInstance musicInstance;
    
    //Scripts
    [SerializeField] private SpeakerAnimation _SpeakerAnimation;
    [SerializeField] private RadioAnimation _RadioAnimation;

    private PlayerInput _Input;

    //Class used to store information about the timeline
    [StructLayout(LayoutKind.Sequential)]
    public class TimelineInfo
    {
        public float BPM;
        public int currentBeat;
        public int previousBeat;
        public StringWrapper lastMarker;
    }

    private void Awake()
    {
        instance = this;

        if (music != null)
        {
            musicInstance = RuntimeManager.CreateInstance(music);
            musicInstance.set3DAttributes(playAtPos.To3DAttributes());
            musicInstance.start();
        }
    }

    private void Start()
    {
        if (music != null)
        {
            timelineInfo = new TimelineInfo();
            beatCallback = new EVENT_CALLBACK(BeatEventCallback);
            timelineHandle = GCHandle.Alloc(timelineInfo);
            musicInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
            musicInstance.setCallback(beatCallback,
                EVENT_CALLBACK_TYPE.TIMELINE_BEAT | 
                          EVENT_CALLBACK_TYPE.TIMELINE_MARKER);
        }

        _Input = GetComponent<PlayerInput>();
    }

    private void OnDestroy()
    {
        musicInstance.setUserData(IntPtr.Zero);
        musicInstance.stop(STOP_MODE.IMMEDIATE);
        musicInstance.release();
        timelineHandle.Free();
    }

    private void Update()
    {
        //Makes the music instance follow the object
        musicInstance.set3DAttributes(playAtPos.To3DAttributes());

        #region - ChannelLogic -
        
        //Update channel play time
        for (var i = 0; i < channelCount; i++)
        {
            channelCurrentTime[i] += Time.deltaTime;

            if (channelCurrentTime[i] >= channelLength[i]) channelCurrentTime[i] -= channelLength[i];
        }

        //Swap Channel
        if (Keyboard.current.nKey.wasPressedThisFrame)
        {
            currentChannel++;
            if (currentChannel >= channelCount) currentChannel = 0;

            var timePos = (int) ((channelStart[currentChannel] + channelCurrentTime[currentChannel])*1000); //Multiply by 1000 to change seconds into milliseconds
            musicInstance.setTimelinePosition(timePos);
            print("Changed timeline position to " + timePos);
        }

        #endregion - ChannelLogic -

        #region - Animation -
        
        //TODO: After VR Input is implemented, stop jumping animation if player tries to interact with the radio (except if holding something else) 

        // GET VALUE FROM FMOD - RuntimeManager.StudioSystem.getParameterByName("AudioOn", out var audio);
        // SET VALUE IN FMOD - RuntimeManager.StudioSystem.setParameterByName("AudioOn", 1f);

        //Play speaker bounce animation on each beat
        if (timelineInfo.previousBeat != timelineInfo.currentBeat)
        {
            timelineInfo.previousBeat = timelineInfo.currentBeat;
            RuntimeManager.StudioSystem.getParameterByName("AudioOn", out var val);
            RuntimeManager.StudioSystem.getParameterByName("Whitenoise", out var noise);
            if ((int) val == 1 && noise < 0.75) //Only runs animations if radio is turned on and the white noise is not too strong
            {
                if (timelineInfo.BPM > 30f) _SpeakerAnimation.SpeakerBounce(); //Speaker Animation

                //Radio animation
                if (_RadioAnimation.radio.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
                {
                    _RadioAnimation.SpeakerJumpLeft();
                }
                else if (_RadioAnimation.radio.GetCurrentAnimatorStateInfo(0).IsName("JumpLeftShort"))
                {
                    _RadioAnimation.SpeakerJumpFarRight();
                }
                else if (_RadioAnimation.radio.GetCurrentAnimatorStateInfo(0).IsName("JumpFarLeft"))
                {
                    _RadioAnimation.SpeakerJumpFarRight();
                }
                else if (_RadioAnimation.radio.GetCurrentAnimatorStateInfo(0).IsName("JumpFarRight"))
                {
                    _RadioAnimation.SpeakerJumpFarLeft();
                }
            }
        }
        
        //Play PS Notes
        //TODO: Stop PS from playing when no song is playing
        if (timelineInfo.BPM < 30f) _SpeakerAnimation.StopPSNotes(); //NOT TESTED. Code to stop PS when no song is playing
        if (_Input.radioOn == 1) _SpeakerAnimation.PlayPSNotes();
        else _SpeakerAnimation.StopPSNotes();

        #endregion

        var firsttest = RuntimeManager.StudioSystem.setParameterByName("AudioOn", _Input.radioOn);
        var secondtest = RuntimeManager.StudioSystem.setParameterByName("MasterVolume", masterVolume);
        RuntimeManager.StudioSystem.setParameterByName("MusicVolume", musicVolume);
        RuntimeManager.StudioSystem.setParameterByName("AmbienceVolume", ambianceVolume);
        RuntimeManager.StudioSystem.setParameterByName("SFXVolume", sfxVolume);

        print(firsttest + " : " + secondtest + " : " + masterVolume);
    }
    
    //Returns information from the current GCHandle
    [AOT.MonoPInvokeCallback(typeof(EVENT_CALLBACK))]
    private static RESULT BeatEventCallback(EVENT_CALLBACK_TYPE type, IntPtr instancePtr, IntPtr paramPtr)
    {
        //Gets the event instance
        EventInstance instance = new EventInstance(instancePtr);

        //Saves the pointer to our user data
        IntPtr timelineInfoPtr;
        RESULT result = instance.getUserData(out timelineInfoPtr);

        //Makes sure that the result is usable, if not send a LogError
        if (result != RESULT.OK)
        {
            Debug.LogError("Timeline Callback error: " + result);
        }
        else if (timelineInfoPtr != IntPtr.Zero)
        {
            //Saves the retrieved GCHandle from the timeline
            GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
            TimelineInfo timelineInfo = (TimelineInfo) timelineHandle.Target;

            //Check which type of GCHandle is retrieved
            switch (type)
            {
                case EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                    {
                        print("Found Beat");
                        //Saves the beat from the GCHandle
                        var param = (TIMELINE_BEAT_PROPERTIES) Marshal.PtrToStructure(paramPtr,
                                    typeof(TIMELINE_BEAT_PROPERTIES));
                        
                        timelineInfo.currentBeat = param.beat;
                        timelineInfo.BPM = param.tempo;
                    }
                    break;
                case EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                    {
                        print("Found Marker");
                        //Saves the marker name from the GCHandle
                        var param = (TIMELINE_MARKER_PROPERTIES) Marshal.PtrToStructure(paramPtr, 
                                    typeof(TIMELINE_MARKER_PROPERTIES));
                        
                        timelineInfo.lastMarker = param.name;
                    }
                    break;
            }
        }

        return RESULT.OK;
    }

    public void SetParameter(string name, float value)
    {
        RuntimeManager.StudioSystem.setParameterByName(name, value);
    }
    
    public float GetParameter(string name)
    {
        RuntimeManager.StudioSystem.getParameterByName(name, out var value);

        return value;
    }

    public void SetChannel(int channel)
    {
        if (currentChannel != channel && channel < channelCount && channel >= 0)
        {
            currentChannel = channel;
            if (currentChannel >= channelCount || currentChannel < 0) currentChannel = 0;

            var timePos =
                (int) ((channelStart[currentChannel] + channelCurrentTime[currentChannel]) *
                       1000); //Multiply by 1000 to change seconds into milliseconds
            musicInstance.setTimelinePosition(timePos);
        }
    }
}