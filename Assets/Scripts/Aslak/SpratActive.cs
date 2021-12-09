using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.VFX;


[RequireComponent(typeof(InteractAble))]
public class SpratActive : MonoBehaviour
{
    public GameObject _obj;
    public VisualEffect SprayEffect;

    private bool _isHeld;
    //private bool _trackpadButtonDown;

    private void OnHeldByHandChanged(InteractAble.Hand heldByHand)
    {
        _isHeld = heldByHand.GameObject != null;
    }

    private void Start()
    {
        SprayEffect.Stop();
    }

    private void OnTrackpadButtonChanged(bool trackpadButtonState)
    {
        //_trackpadButtonDown = trackpadButtonState;

        if (!trackpadButtonState)
        {
            SprayEffect.Stop();
            _obj.SetActive(false);
        }
        else if (_isHeld)
        {
            Music.PlayLoop("SFX/bugspray", transform);
            SprayEffect.Play();
            StartCoroutine(SprayDelay());
        }
    }


    //"only" used for testing"
    /*void Update()
    {
        if (Keyboard.current.aKey.isPressed)
        {
            Music.PlayLoop("SFX/bugspray");
            SprayEffect.Play();
            StartCoroutine(SprayDelay());
        }
        else 
        {
            SprayEffect.Stop();
            _obj.SetActive(false);
        }
        
    }*/

    private IEnumerator SprayDelay()
    {
        yield return new WaitForSeconds(1);
        _obj.SetActive(true);
    }
}
