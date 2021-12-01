using UnityEngine;

public class RadioDial : MonoBehaviour
{
    private float minHertz = 88;
    private float maxHertz = 108;
    private float dialMaxRotation = 120;
    private float dialMinRotation = -120;
    
    [SerializeField] private PlayerInput _Input;
    
    // Update is called once per frame
    void Update()
    {
        var rot = transform.localRotation.eulerAngles;
        //Rotate channel dial
        if (_Input.rotateDir != 0)
        {
            //transform.rotation = Quaternion.Euler();
            transform.localRotation = Quaternion.Euler(rot.x, rot.y + (_Input.rotateDir * Time.deltaTime * 120), rot.z);
        }
        
        print(transform.localRotation.y);

        //Set value of channel & white noise based on hertz
    }
}
