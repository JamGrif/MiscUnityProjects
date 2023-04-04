using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpkinScript : MonoBehaviour {


    public float BounceStrength;
    Rigidbody2D MyRigid;

	void Start () {
        MyRigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("2dLand"))
        {
            MyRigid.AddForce(new Vector2(0, BounceStrength));
            GetComponent<AudioSource>().Play();
        }
    }
}
