using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLawnmowerBody : MonoBehaviour
{
    public GameObject lawnMowerBody;

    public Vector3 target;
    public Vector3 offset;
    
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private float smoothTime = 0.1f;
    // Start is called before the first frame update
    private void Start()
    {
        offset = lawnMowerBody.transform.position - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        target = lawnMowerBody.transform.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, smoothTime);
    }
}
