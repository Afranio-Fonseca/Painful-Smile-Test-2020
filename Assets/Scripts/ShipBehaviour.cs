using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipBehaviour : MonoBehaviour
{

    [Header("Prefab Association")]
    public Cannonball cannonball;
    public HealthBarBehaviour healthBar;

    [Header("Settings")]
    public int maxHealth;
    public float acceleration;
    public float maxSpeed;
    public float rotationSpeed;
    public float cannonRange;
    public float cannonballSpeed;
    public int cannonDamage;
    public float cannonCooldownTime;
    public float timeActiveAfterDeath;

    [System.NonSerialized]
    public int health;
    [System.NonSerialized]
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

    HealthBarBehaviour healthBarInstance;

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
        health = maxHealth;
        healthBarInstance = Instantiate(healthBar);
        healthBarInstance.owner = transform;
        healthBarInstance.hpSlider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReceiveDamage(int damage, ShipBehaviour origin)
    {
        if (health <= 0) return;
        health = Mathf.Max(0, health - damage);
        if (health > 0)
        {
            float percentHp = (float)health / maxHealth;
            healthBarInstance.hpSlider.value = percentHp;
            float red, green;
            if (percentHp < 0.5f)
            {
                red = 255f;
                green = 255f - ((0.5f - percentHp) * 510f);
            }
            else
            {
                red = 255f - ((percentHp - 0.5f) * 510f);
                green = 255f;
            }
            healthBarInstance.sliderImage.color = new Color(red / 255f, green / 255f, 0, 255f);
        }
        GetComponent<Animator>().SetInteger("hp", health);
        if (health <= 0)
        {
            healthBarInstance.sliderImage.color = new Color(0, 0, 0, 0);
            Destroy(healthBarInstance.gameObject);
            GameManager.instance.ShipDestroyed(this, origin);
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(timeActiveAfterDeath);
        GetComponent<Animator>().SetTrigger("Explode");
    }

    public void FireFrontCannon()
    {
        if (!frontCannonActive) return;
        frontCannonActive = false;
        StartCoroutine(FrontCannonCooldown());
        FireCannon(frontCannon);
    }

    public void FireLeftCannons()
    {
        if (!leftCannonActive) return;
        leftCannonActive = false;
        StartCoroutine(LeftCannonCooldown());
        foreach (Transform cannon in leftCannons)
        {
            FireCannon(cannon);
        }
    }

    public void FireRightCannons()
    {
        if (!rightCannonActive) return;
        rightCannonActive = false;
        StartCoroutine(RightCannonCooldown());
        foreach(Transform cannon in rightCannons)
        {
            FireCannon(cannon);
        }
    }

    void FireCannon(Transform cannon)
    {
        Cannonball cb;
        cb = Instantiate(cannonball, cannon.position, cannon.rotation);
        cb.speed = cannonballSpeed;
        cb.range = cannonRange;
        cb.damage = cannonDamage;
        cb.owner = this;
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

    public void MoveForward()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * acceleration);
        if (GetComponent<Rigidbody2D>().velocity.magnitude > maxSpeed)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector3(0, speed - GetComponent<Rigidbody2D>().velocity.magnitude, 0));
        }
    }

    public void Rotate(float rotationAngle)
    {
        transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime * GetComponent<Rigidbody2D>().velocity.magnitude * rotationAngle));
    }

    public void FinishObject()
    {
        Destroy(this.gameObject);
    }
}
