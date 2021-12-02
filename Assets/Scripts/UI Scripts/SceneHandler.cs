using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

public class SceneHandler : MonoBehaviour
{
    public SteamVR_LaserPointer laserPointer;

    public Slider _slider;

    void Awake()
    {
        laserPointer.PointerIn += PointerInside;
        laserPointer.PointerOut += PointerOutside;
        laserPointer.PointerClick += PointerClick;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was clicked");
        } else if (e.target.name == "Button")
        {
            Debug.Log("Button was clicked");
            e.target.GetComponent<Button>().onClick.Invoke();
          
        }
        else if (e.target.name == "Slider")
        {
            print("slider was pressed");
            _slider.value = _slider.maxValue /laserPointer.laserHitPosition.x;
            print(_slider.value);
        }
    }

    public void PointerInside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was entered");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was entered");
            e.target.GetComponent<Image>().color = e.target.GetComponent<Button>().colors.highlightedColor;
        }
        else if (e.target.name == "Slider")
        {
            print("slider was entered");
        }
    }

    public void PointerOutside(object sender, PointerEventArgs e)
    {
        if (e.target.name == "Cube")
        {
            Debug.Log("Cube was exited");
        }
        else if (e.target.name == "Button")
        {
            Debug.Log("Button was exited");
            e.target.GetComponent<Image>().color = e.target.GetComponent<Button>().colors.normalColor;
        }
        else if (e.target.name == "Slider")
        {
            print("slider was exited");
        }
    }
}