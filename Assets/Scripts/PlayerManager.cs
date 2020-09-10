using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Prefab Association")]
    [SerializeField]
    Cannonball cannonball;

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
    float cannonRange;
    [SerializeField]
    float cannonballSpeed;
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
    Transform frontCannon;
    Transform[] leftCannons = new Transform[3];
    Transform[] rightCannons = new Transform[3];

    // Start is called before the first frame update
    void Start()
    {
        frontCannon = transform.Find("Cannons").Find("FrontCannon");
        leftCannons[0] = transform.Find("Cannons").Find("LeftFrontCannon");
        leftCannons[1] = transform.Find("Cannons").Find("LeftCenterCannon");
        leftCannons[2] = transform.Find("Cannons").Find("LeftBackCannon");
        rightCannons[0] = transform.Find("Cannons").Find("RightFrontCannon");
        rightCannons[1] = transform.Find("Cannons").Find("RightCenterCannon");
        rightCannons[2] = transform.Find("Cannons").Find("RightBackCannon");
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
            transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * GetComponent<Rigidbody2D>().velocity.magnitude * -Input.GetAxis("Horizontal")));
        }
        if (Input.GetButton("Fire1") && frontCannonActive)
        {
            FireFrontCannon();
            frontCannonActive = false;
            StartCoroutine(FrontCannonCooldown());
        }
        if (Input.GetButton("Fire2") && leftCannonActive)
        {
            FireLeftCannons();
            leftCannonActive = false;
            StartCoroutine(LeftCannonCooldown());
        }
        if (Input.GetButton("Fire3") && rightCannonActive)
        {
            FireRightCannons();
            rightCannonActive = false;
            StartCoroutine(RightCannonCooldown());
        }
    }

    void FireFrontCannon()
    {
        Cannonball cb = Instantiate(cannonball, frontCannon.position, frontCannon.rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
    }

    void FireLeftCannons()
    {
        Cannonball cb;
        cb = Instantiate(cannonball, leftCannons[0].position, leftCannons[0].rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
        cb = Instantiate(cannonball, leftCannons[1].position, leftCannons[1].rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
        cb = Instantiate(cannonball, leftCannons[2].position, leftCannons[2].rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
    }

    void FireRightCannons()
    {
        Cannonball cb;
        cb = Instantiate(cannonball, rightCannons[0].position, rightCannons[0].rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
        cb = Instantiate(cannonball, rightCannons[1].position, rightCannons[1].rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
        cb = Instantiate(cannonball, rightCannons[2].position, rightCannons[2].rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
    }

    IEnumerator FrontCannonCooldown()
    {
        yield return new WaitForSeconds(cannonCooldownTime);
        frontCannonActive = true;
    }

    IEnumerator LeftCannonCooldown()
    {
        yield return new WaitForSeconds(cannonCooldownTime);
        leftCannonActive = true;
    }

    IEnumerator RightCannonCooldown()
    {
        yield return new WaitForSeconds(cannonCooldownTime);
        rightCannonActive = true;
    }
}
