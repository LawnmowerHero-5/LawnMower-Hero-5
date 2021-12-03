using UnityEngine;

public class GasCrankMovement : MonoBehaviour
{
    public float rotateSpd = 1f;
    public float minAngle = -18f;
    public float maxAngle = 45f;
    public float boostMaxAngle = 60f; //Max angle when boosting
    public float deadzoneAngle = 5f; //How big the zRot must be before the crank rotates
    
    [HideInInspector] public float power; //Used to change the z rotation into a float used to multiply with movement speed of lawnmower

    private float zRot; //Targeted z rotation, but actual rotation can be overwritten based on the deadzone
    
    private Input _Input;

    public Transform grabbablePos;
    
    void Start()
    {
        _Input = GetComponent<Input>();
    }
    
    void Update()
    {
        //TODO: Make compatible with VR input 
        //Changes crank rotation based on scroll wheel input 
        var rot = transform.localRotation.eulerAngles;
        var add = rotateSpd * Time.deltaTime;
        if (_Input.crankAxis > 0f)
        {
            if (zRot + add <= maxAngle || 
                zRot + add >= 360 + minAngle)
            {
                zRot += add;
            }
            else zRot = maxAngle;
            print("Forward");
        }

        if (_Input.crankAxis < 0f)
        {
            if (rot.z - add >= 360 + minAngle || 
                rot.z - add <= maxAngle)
            {
                zRot -= add;
            }
            else zRot = minAngle;
            print("Reverse");
        }
        
        //Implementation of the deadzone
        if (Mathf.Abs(zRot) >= deadzoneAngle) transform.localRotation = Quaternion.Euler(rot.x, rot.y, zRot);
        else transform.localRotation = Quaternion.Euler(rot.x, rot.y, 0);
        
        //Sets power based on rotation
        rot = transform.localRotation.eulerAngles;
        
        if (rot.z == 0) power = 0;
        else if (rot.z  <= 180) power = (rot.z) / maxAngle;
        else power = (360 - rot.z) / - (maxAngle);
    }
}
