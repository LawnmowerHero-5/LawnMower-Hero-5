using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class cutedGrass : MonoBehaviour
{
    [FormerlySerializedAs("tex")] public RenderTexture renderTexture;
    [FormerlySerializedAs("myTexture")] public Texture2D newTexture2D;

    private bool IcantBelieveitsNotTrue;
    private void Awake()
    {
        newTexture2D = ToTexture2D(renderTexture);
    }

    private void Start()
    {
        newTexture2D = ToTexture2D(renderTexture);
    }
    
    Texture2D ToTexture2D(RenderTexture rTex)
    {
        Texture2D tex = new Texture2D(512, 512, TextureFormat.RGB24, false);
        // ReadPixels looks at the active RenderTexture.
        RenderTexture.active = rTex;
        tex.ReadPixels(new Rect(0, 0, rTex.width, rTex.height), 0, 0);
        tex.Apply();
        return tex;
    }

    float ReadTexture2DPixels(Texture2D tex)
    {
        var totalPixels = tex.width * tex.height;
        var colouredPixels = 0f;
        
        for (int x = 0; x < tex.width; x++)
        for (int y = 0; y < tex.height; y++)
            if (tex.GetPixel(x, y).Equals(new Color(0, 0, 0, 1)))
                colouredPixels++;

        //return new Vector2(colored, totoal);
        return colouredPixels / totalPixels * 100f;
    }

    private void Update()
    {
        if (!IcantBelieveitsNotTrue)
        {
            newTexture2D = ToTexture2D(renderTexture);
            IcantBelieveitsNotTrue = true;
        }

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            newTexture2D = ToTexture2D(renderTexture);
        }
        
        print(ReadTexture2DPixels(newTexture2D));
    }
    
    bool IsTransparent(Texture2D tex) {
        for (int x = 0; x < tex.width; x++)
        for (int y = 0; y < tex.height; y++)
            if (tex.GetPixel(x, y).Equals(new Color(1,1,1,1)))
                return false;
        return true;
    }

}
