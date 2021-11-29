using UnityEngine;

public class GasCrankMovement : MonoBehaviour
{
    public float rotateSpd = 1f;
    public float minAngle = -18f;
    public float maxAngle = 45f;
    public float boostMaxAngle = 60f;
    public float deadzoneAngle = 5f;
    
    [HideInInspector] public float power;

    private Input _Input;
    
    // Start is called before the first frame update
    void Start()
    {
        _Input = GetComponent<Input>();
    }

    // Update is called once per frame
    void Update()
    {
        //TODO: Make compatible with VR input 
        //Changes crank rotation based on scroll wheel input 
        var rot = transform.rotation.eulerAngles;
        if (_Input.crankAxis > 0f)
        {
            if (rot.z + (rotateSpd * Time.deltaTime) <= maxAngle || 
                rot.z + (rotateSpd * Time.deltaTime) >= 360 + minAngle)
            {
                transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z + (rotateSpd * Time.deltaTime));
            }
            else transform.rotation = Quaternion.Euler(rot.x, rot.y, maxAngle);
            print("Forward");
        }

        if (_Input.crankAxis < 0f)
        {
            if (rot.z - (rotateSpd * Time.deltaTime) >= 360 + minAngle || 
                rot.z - (rotateSpd * Time.deltaTime) <= maxAngle)
            {
                transform.rotation = Quaternion.Euler(rot.x, rot.y, rot.z - (rotateSpd * Time.deltaTime));
            }
            else transform.rotation = Quaternion.Euler(rot.x, rot.y, 360 + minAngle);
            print("Reverse");
        }
        print(transform.rotation.eulerAngles.z);

        if (transform.rotation.eulerAngles.z <= 180) power = transform.rotation.eulerAngles.z / maxAngle;
        else power = (360 - transform.rotation.eulerAngles.z) / -maxAngle;
        
        print(power);
    }
}
