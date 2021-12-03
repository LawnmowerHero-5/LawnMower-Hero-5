using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ReloadBar : MonoBehaviour
{

    public Slider slider;
    public static float CurrentReload;

    private void Start()
    {
        CurrentReload = 100;
    }

    private void SetReload(float reload)
    {
        slider.value = reload;
    }

    private void FixedUpdate()
    {
        CurrentReload += 1 * Time.deltaTime;
        SetReload(CurrentReload);
    }

    private void Update()
    {
        if (CurrentReload >=100)
        {
            CurrentReload = 100;
        }

        if (CurrentReload <=0)
        {
            CurrentReload = 0;
        }
    }
}