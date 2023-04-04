using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour {


    public float Cooldown = 0.5f;
    private float CurCooldown = 0;

    private Rigidbody OurRigid;

    private void Start()
    {
        OurRigid = GameObject.Find("GO").GetComponent<Rigidbody>();
        OurRigid.velocity = new Vector3(2, 0, 0);
    }

    void Update ()
    {
        CurCooldown += Time.deltaTime;
        if(CurCooldown > Cooldown)
        {
            Debug.Log("Periodic Thing!");
            RevSpeed();
            CurCooldown = 0;
        }
	}

    private void RevSpeed()
    {
        OurRigid.velocity *= -1;
    }
}
