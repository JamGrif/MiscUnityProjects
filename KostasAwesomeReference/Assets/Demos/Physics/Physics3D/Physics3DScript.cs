using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Physics3DScript : MonoBehaviour {

    Rigidbody MyRigid;

    public int BouncesLeft;

    public float AddedForce;

    bool IsTouchingCollider;
    bool IsTouchingTrigger;

    public float SpeedSwapInterval;
    private float CurInterval;
    private float DeltaTime;

    public Material Mat1;
    public Material Mat2;

    public float Health;
    public float PoisonDamage;
    private float CurPoisonInterval;
    public float PoisonInterval;

    // Use this for initialization
    void Start ()
    {
        MyRigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        DeltaTime = Time.deltaTime;	
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (BouncesLeft > 0)
        {
            MyRigid.AddForce(new Vector3(0, AddedForce, 0));
            --BouncesLeft;
        }
        else
        IsTouchingCollider = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        IsTouchingCollider = false;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (IsTouchingCollider)
        {
            CurInterval += DeltaTime;
            if (CurInterval > SpeedSwapInterval)
            {
                Vector3 RandomVec = new Vector3(Random.Range(-8, 8), 0, Random.Range(-8, 8));
                Debug.Log(RandomVec);
                MyRigid.velocity = RandomVec;

                CurInterval = 0;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        GetComponent<MeshRenderer>().sharedMaterial =  Mat1;
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<MeshRenderer>().sharedMaterial = Mat2;
    }

    private void OnTriggerStay(Collider other)
    {
        CurPoisonInterval += Time.deltaTime;
        if(CurPoisonInterval > PoisonInterval)
        {
            Health -= PoisonDamage;
            CurPoisonInterval = 0;
        }
    }
}
