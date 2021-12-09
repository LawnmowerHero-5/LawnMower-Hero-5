using System.Collections;
using UnityEngine;
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
            SprayEffect.Play();
            StartCoroutine(SprayDelay());
        }
    }


    // Update is called once per frame
    /*void Update()
    {
        if (_isHeld && _trackpadButtonDown)
        {
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
