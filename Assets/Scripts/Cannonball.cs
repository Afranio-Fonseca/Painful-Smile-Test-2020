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
    Vector3 startPosition;
    float distanceTraveled = 0;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        distanceTraveled += Time.deltaTime * speed;
        if (distanceTraveled > range)
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GetComponent<Animator>().Play("Explosion");
        if(collision.gameObject.tag == "Enemy")
        {

        }
        else if (collision.gameObject.tag == "Player")
        {

        }
    }

    public void FinishObject()
    {
        Destroy(this.gameObject);
    }
}
