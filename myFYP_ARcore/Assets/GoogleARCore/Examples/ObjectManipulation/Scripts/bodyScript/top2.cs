﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class top2 : MonoBehaviour
{
    public Texture[] textures;
    public int currentTexture;
    public int stone;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void topTexture2()
    {

        currentTexture++;
        currentTexture %= textures.Length;
        GetComponent<Renderer>().material.mainTexture = textures[stone];
    }
}
