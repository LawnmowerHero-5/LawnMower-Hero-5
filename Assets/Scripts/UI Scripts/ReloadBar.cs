using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class ReloadBar : MonoBehaviour
{

    public Slider slider;
    public static float CurrentReload;
    [SerializeField] private TMP_Text reloadText;
    
    [Header("Reload % per second")]
    [SerializeField] private int reloadSpeed = 25;
    
    
    

    private void Start()
    {
        CurrentReload = 100;
    }

    private void SetReload(float reload)
    {
        slider.value = reload;
    }

    private void Shoot()
    {
        CurrentReload = 0;
    }
    private void FixedUpdate()
    {
        if (CurrentReload >=100)
        {
            return;
        }
        CurrentReload += reloadSpeed * Time.deltaTime;
        SetReload(CurrentReload);
        reloadText.text = Mathf.RoundToInt(CurrentReload) + "%";
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

        if (Keyboard.current.kKey.wasPressedThisFrame)
        {
            Shoot();
        }
    }
}