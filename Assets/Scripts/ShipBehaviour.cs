using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{

    [Header("Prefab Association")]
    public Cannonball cannonball;

    [Header("Settings")]
    public int maxHealth;
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;
    public float cannonRange;
    public float cannonballSpeed;
    public int cannonDamage;
    public float cannonCooldownTime;

    [System.NonSerialized]
    public int health;
    public float speed;
    [System.NonSerialized]
    public bool frontCannonActive = true;
    [System.NonSerialized]
    public bool leftCannonActive = true;
    [System.NonSerialized]
    public bool rightCannonActive = true;
    [System.NonSerialized]
    public Transform frontCannon;
    [System.NonSerialized]
    public Transform[] leftCannons = new Transform[3];
    [System.NonSerialized]
    public Transform[] rightCannons = new Transform[3];

    // Start is called before the first frame update
    public void Start()
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

    }

    public void ReceiveDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
    }

    public void FireFrontCannon()
    {
        frontCannonActive = false;
        StartCoroutine(FrontCannonCooldown());
        Cannonball cb = Instantiate(cannonball, frontCannon.position, frontCannon.rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
    }

    public void FireLeftCannons()
    {
        leftCannonActive = false;
        StartCoroutine(LeftCannonCooldown());
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

    public void FireRightCannons()
    {
        rightCannonActive = false;
        StartCoroutine(RightCannonCooldown());
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
