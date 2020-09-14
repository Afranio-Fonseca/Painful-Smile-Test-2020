﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ShipBehaviour
{

    public static PlayerManager instance;

    // Start is called before the first frame update
    new void Start()
    {
        if (instance) Destroy(this.gameObject);
        else instance = this;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        // Player movement
        if(Input.GetAxis("Vertical") > 0.2f)
        {
            MoveForward();
        }
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            Rotate(-Input.GetAxis("Horizontal"));
        }
        // Player inputs
        if (Input.GetButton("Fire1"))
        {
            FireFrontCannon();
        }
        if (Input.GetButton("Fire2"))
        {
            FireLeftCannons();
        }
        if (Input.GetButton("Fire3"))
        {
            FireRightCannons();
        }
    }
}
