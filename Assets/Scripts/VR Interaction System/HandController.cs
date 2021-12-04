using UnityEngine;
using Valve.VR;

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

    public bool IsTriggerDown   //Is trigger of this controller currently held down or not
    {
        get
        {
            return _triggerDownAction.GetState(BehaviourPose.inputSource);
        }
    }

    public bool IsTriggerStateUp    //Has the trigger changed from down to up this frame
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
    }

    private void Awake()
    {
        BehaviourPose = GetComponent<SteamVR_Behaviour_Pose>();
    }
}
