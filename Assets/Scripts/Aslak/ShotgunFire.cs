using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    void Awake()
    {
        pellets = new List<Quaternion>(new Quaternion[pelletCount]);
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.wKey.wasPressedThisFrame && canFire)
        {
            print("I AM THE GOD OF HELLFIRE AND I BRING YOU");
            StartCoroutine(CantFireTimer());


        }
    }

    private IEnumerator CantFireTimer()
    {
        Fire();
        canFire = false;
        yield return new WaitForSeconds(3);
        canFire = true;
    }
        
    void Fire()
    {
       
        GameObject p = new GameObject();
        for (int i = 0; i  <pelletCount ; i ++)
        {
            pellets[i] = Random.rotation;
            p = (GameObject)Instantiate(pellet, BarrelExit.position, BarrelExit.rotation) as GameObject;
            Destroy(p, LifeTime);
            p.transform.rotation = Quaternion.RotateTowards(p.transform.rotation, pellets[i], spreadAngle);
            p.GetComponent<Rigidbody>().AddForce(p.transform.forward * pelletFireVel);
            i++;  
        }
    }
}
