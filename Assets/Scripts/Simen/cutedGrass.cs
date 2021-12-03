using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab.ClientModels;
using UnityEngine;

public class cutedGrass : MonoBehaviour
{
    private Texture2D _texture2D;

    private void Start()
    {
        _texture2D = GetComponent<MeshRenderer>().material.mainTexture as Texture2D;
    }

    bool IsTransparent(Texture2D tex) {
        for (int x = 0; x < tex.width; x++)
        for (int y = 0; y < tex.height; y++)
            if (tex.GetPixel(x, y).Equals(new Color(0,0,0,1)))
                return false;
        return true;
    }
    
    float IsTransparentTest(Texture2D tex)
    {
        var totoal = tex.width * tex.height;
        var colored = 0f;
        
        for (int x = 0; x < tex.width; x++)
        for (int y = 0; y < tex.height; y++)
            if (tex.GetPixel(x, y).Equals(new Color(0, 0, 0, 1)))
                colored++;

        //return new Vector2(colored, totoal);
        return colored / totoal * 100f;
    }

    private void Update()
    {
      
        print(IsTransparentTest(_texture2D));
        print(IsTransparent(_texture2D));
    }
}
