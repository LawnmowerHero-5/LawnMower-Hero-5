using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpratActive : MonoBehaviour
{
    public GameObject _obj;
    

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.sKey.isPressed)
        {
            StartCoroutine(SprayDelay());
        }
        else 
        {
            _obj.SetActive(false);
        }
        
    }

    private IEnumerator SprayDelay()
    {
        yield return new WaitForSeconds(1);
        _obj.SetActive(true);
    }
}
