using UnityEngine;

public class RadioDial : MonoBehaviour
{
    public float rotateSpd = 120f;

    [HideInInspector] public float hertz = 88f;
    
    private float minHertz = 88f;
    private float maxHertz = 108f;
    private float dialMinAngle = -120f;
    private float dialMaxAngle = 120f;

    private float yRot;

    [SerializeField] private PlayerInput _Input;
    
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

        print(rot.y);
        transform.localRotation = Quaternion.Euler(rot.x, yRot, rot.z);

        var angle = yRot - dialMinAngle;
        //TODO: CONTINUE FROM HERE BJØRN WITH CHANGING ANGLE TO HERTZ VALUE
    }
}
