﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarField : MonoBehaviour
{
    public float speed = 2;
    ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>() as ParticleSystem;
    }

    // Update is called once per frame
    void Update()
    {
        ps.playbackSpeed = speed;
    }

    public void Speed (float newstarspeed)
    {
        ps.playbackSpeed = 30;
    }
}
