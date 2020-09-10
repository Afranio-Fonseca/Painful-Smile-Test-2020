using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    [Header("Settings")]
    [SerializeField]
    int maxHealth;
    [SerializeField]
    float acceleration;
    [SerializeField]
    float deceleration;
    [SerializeField]
    float maxSpeed;
    [SerializeField]
    float rotationSpeed;
    [SerializeField]
    int cannonDamage;
    [SerializeField]
    float cannonCooldownTime;

    [System.NonSerialized]
    public int health;
    float speed;
    bool frontCannonActive = true;
    bool leftCannonActive = true;
    bool rightCannonActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") > 0.4)
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration);
            if(GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector3(0, speed - GetComponent<Rigidbody2D>().velocity.magnitude, 0));
            }
        } else
        {
            speed -= deceleration * Time.deltaTime;
            if (speed < 0) speed = 0;
        }
        if(Mathf.Abs(Input.GetAxis("Horizontal")) > 0.2f)
        {
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * GetComponent<Rigidbody2D>().velocity.magnitude * Input.GetAxis("Horizontal")));
        }
    }
}
