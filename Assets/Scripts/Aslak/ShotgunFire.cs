using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PickupAble))]
[RequireComponent(typeof(InteractAble))]
public class ShotgunFire : MonoBehaviour
{
    public int pelletCount;
    public float spreadAngle;
    private float LifeTime = 3f; 
    public float pelletFireVel = 1;
    public GameObject pellet;
    public Transform BarrelExit;
    private List<Quaternion> pellets;
    private bool canFire = true;
    
    private bool _isHeld;
    
    void Awake()
    {
        pellets = new List<Quaternion>(new Quaternion[pelletCount]);
    }
    
    private void OnHeldByHandChanged(InteractAble.Hand heldByHand)
    {
        _isHeld = heldByHand.GameObject != null;
    }

    private void OnTrackpadButtonChanged(bool trackpadButtonState)
    {
        if (!trackpadButtonState || !_isHeld && !canFire)
        {
            return;
        }
        
            print("I AM THE GOD OF HELLFIRE AND I BRING YOU");

            if (canFire && _isHeld)
            {
                StartCoroutine(CantFireTimer());
            }
            
        
        
    }
    
    
    /*void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame && canFire)
        {
            print("I AM THE GOD OF HELLFIRE AND I BRING YOU");
            StartCoroutine(CantFireTimer());


        }
    }*/

    private IEnumerator CantFireTimer()
    {
        Fire();
        canFire = false;
        yield return new WaitForSeconds(20);
        canFire = true;
    }
        
    void Fire()
    {
       
        //GameObject p = new GameObject();
        for (int i = 0; i  <pelletCount ; i ++)
        {
            pellets[i] = Random.rotation;
            var p = (GameObject)Instantiate(pellet, BarrelExit.position, BarrelExit.rotation) as GameObject;
            Destroy(p, LifeTime);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * pelletFireVel);
            i++;  
        }
    }


}
