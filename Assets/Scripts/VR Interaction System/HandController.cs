using System;
using UnityEngine;
using Valve.VR;
using Object = System.Object;

[RequireComponent(typeof(SteamVR_Behaviour_Pose))]
public class HandController : MonoBehaviour
{
    /*
    This simplify getting action values by reducing them to simple get peroperties
    Only the input on the controller this script is attached are handled, so one script for each controller is required
    The reason I made the actions into properties is because I find it cleaner and more efficient than putting them in update each frame
    */
    
    [SerializeField] private SteamVR_Action_Boolean _triggerDownAction;

    public SteamVR_Behaviour_Pose BehaviourPose { get; private set; }

    //Might be deleted since it is probably better to only be able to get when a value is changed, on not just its current state
    /*public bool IsTriggerDown   //Is trigger of this controller currently held down or not
    {
        get
        {
            return _triggerDownAction.GetState(BehaviourPose.inputSource);
        }
    }*/

    //Not used since the sendmessage system is now used
    /*public bool IsTriggerStateUp    //Has the trigger changed from down to up this frame
    {
        get
        {
            return _triggerDownAction.GetStateUp(BehaviourPose.inputSource);
        }
    }
    
    public bool IsTriggerStateDown  //Has the trigger changed from up to down this frame
    {
        get
        {
            return _triggerDownAction.GetStateDown(BehaviourPose.inputSource);
        }
    }*/

    private void Awake()
    {
        BehaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    private void OnEnable()
    {
        if (_triggerDownAction != null)
        {
            _triggerDownAction.AddOnChangeListener(SendTriggerStateChangedMessage, BehaviourPose.inputSource);
        }
    }

    private void OnDisable()
    {
        if (_triggerDownAction != null)
        {
            _triggerDownAction.RemoveAllListeners(BehaviourPose.inputSource);
        }
    }

    private void SendTriggerStateChangedMessage(SteamVR_Action_Boolean actionBoolean, SteamVR_Input_Sources inputSources, bool state)
    {
        gameObject.SendMessage("OnTriggerButtonChanged", state);
    }
}
