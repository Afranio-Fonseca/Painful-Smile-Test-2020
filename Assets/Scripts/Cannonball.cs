using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannonball : MonoBehaviour
{
    [System.NonSerialized]
    public float speed;
    [System.NonSerialized]
    public float range;
    [System.NonSerialized]
    public int damage;
    [System.NonSerialized]
    public ShipBehaviour owner;
    Vector3 startPosition;
    float distanceTraveled = 0;
    bool registeredHit = false;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!registeredHit)
        {
            transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
            distanceTraveled += Time.deltaTime * speed;
            if (distanceTraveled > range)
                Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        ShipBehaviour ship = col.gameObject.GetComponent<ShipBehaviour>();
        if (ship != null && ship != owner) {
            registeredHit = true;
            ship.ReceiveDamage(damage, owner);
            GetComponent<Animator>().SetTrigger("Explode");
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void FinishObject()
    {
        Destroy(this.gameObject);
    }
}
