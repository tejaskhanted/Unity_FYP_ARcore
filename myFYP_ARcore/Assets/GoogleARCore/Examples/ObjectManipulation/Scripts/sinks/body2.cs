﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class body2 : MonoBehaviour
{
    public Texture[] textures;
    public int currentTexture;
    public int wood;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void sinkBody2()
    {

        currentTexture++;
        currentTexture %= textures.Length;
        GetComponent<Renderer>().material.mainTexture = textures[wood];
    }
}
