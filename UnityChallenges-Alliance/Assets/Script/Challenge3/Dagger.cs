using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public Rigidbody2D rb;
    private int speed = 20;
    void Start()
    {
        rb.AddForce(new Vector2(speed, 0));


    }
}
