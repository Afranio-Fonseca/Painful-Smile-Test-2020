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
        // Player movement
        if(Input.GetAxis("Vertical") > 0.4)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration);
            if(GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0, speed - GetComponent<Rigidbody2D>().velocity.magnitude, 0));
            }
        }
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * GetComponent<Rigidbody2D>().velocity.magnitude * -Input.GetAxis("Horizontal")));
        }
        // Player inputs
        if (Input.GetButton("Fire1") && frontCannonActive)
        {
            FireFrontCannon();
        }
        if (Input.GetButton("Fire2") && leftCannonActive)
        {
            FireLeftCannons();
        }
        if (Input.GetButton("Fire3") && rightCannonActive)
        {
            FireRightCannons();
        }
    }
}
