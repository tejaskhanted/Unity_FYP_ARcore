﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class top1 : MonoBehaviour
{
    public Texture[] textures;
    public int currentTexture;
    public int ceramic;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void topTexture1()
    {

        currentTexture++;
        currentTexture %= textures.Length;
        GetComponent<Renderer>().material.mainTexture = textures[ceramic];
    }
}
