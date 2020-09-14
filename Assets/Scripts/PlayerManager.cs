using System.Collections;
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
        if (health <= 0) return;
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
        if (Input.GetButton("FireFront"))
        {
            FireFrontCannon();
        }
        if (Input.GetButton("FireLeft"))
        {
            FireLeftCannons();
        }
        if (Input.GetButton("FireRight"))
        {
            FireRightCannons();
        }
    }
}
