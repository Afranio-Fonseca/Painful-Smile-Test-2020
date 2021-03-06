﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserBehaviour : ShipBehaviour
{

    [SerializeField]
    float sightRange = 15;
    [SerializeField]
    int crashDamage = 50;

    // Update is called once per frame
    void Update()
    {
        if (health <= 0) return;
        LayerMask playerLayer = LayerMask.GetMask("Player");
        LayerMask islandLayer = LayerMask.GetMask("Island");
        if (Physics2D.OverlapCircle(transform.position, sightRange, playerLayer) != null)
        {
            RaycastHit2D islandInFront = Physics2D.Raycast(transform.position, transform.up, 5f, islandLayer);

            if (!islandInFront)
            {
                {
                    Vector3 playerDirection = PlayerManager.instance.transform.position - transform.position;
                    Vector3 standarEuler = Quaternion.LookRotation(playerDirection, Vector3.forward).eulerAngles;
                    float zRotation = 0;
                    if (playerDirection.x > 0) zRotation = -standarEuler.x - 90;
                    else zRotation = standarEuler.x + 90;
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, zRotation)), (rotationSpeed / 70) * Time.deltaTime * GetComponent<Rigidbody2D>().velocity.magnitude);
                    MoveForward();
                }
            }
            else
            {
                RaycastHit2D islandToLeft = Physics2D.Raycast(transform.position, -transform.right, islandLayer);
                RaycastHit2D islandToRight = Physics2D.Raycast(transform.position, transform.right, islandLayer);
                if (!islandToLeft) Rotate(2);
                else if (!islandToRight) Rotate(-2);
                else if (Vector3.Distance(islandToRight.transform.position, transform.position) > Vector3.Distance(islandToLeft.transform.position, transform.position)) Rotate(-2);
                else Rotate(2);
                MoveForward();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ShipBehaviour sb = collision.gameObject.GetComponent<ShipBehaviour>();
        if(sb != null && health > 0)
        {
            sb.ReceiveDamage(crashDamage, this);
        }
        this.ReceiveDamage(this.maxHealth, this);
    }
}
