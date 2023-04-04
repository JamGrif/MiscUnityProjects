using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 playerPosition;
    private Transform player;
    private int Speed = 10;

    public Rigidbody2D rb;

    void Start()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        rb.AddForce(((player.position - transform.position)*Speed));

        //Used to turn arrow to face players saved position
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(playerPosition.y - transform.position.y, playerPosition.x - transform.position.x) * Mathf.Rad2Deg - 270);

    }



}

