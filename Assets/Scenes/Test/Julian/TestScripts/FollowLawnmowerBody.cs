using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLawnmowerBody : MonoBehaviour
{
    public GameObject lawnMowerBody;

    public Vector3 target;
    public Vector3 offset;
    
    private Vector3 _velocity = Vector3.zero;
    public bool isSmoothed;
    private Vector3 _startPos;

    [SerializeField] private float smoothTime = 0.1f;
    // Start is called before the first frame update
    private void Start()
    {
        offset = lawnMowerBody.transform.position - transform.position;
        _startPos = transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        target = lawnMowerBody.transform.position + offset;
        if (isSmoothed)
        {
            transform.rotation = lawnMowerBody.transform.rotation;
            transform.position = Vector3.SmoothDamp(transform.position, target, ref _velocity, smoothTime);
        }
        else
        {
            transform.position = new Vector3(target.x, _startPos.y, target.z);
        }
    }
}
