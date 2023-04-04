using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float MoveSpeed;

    public float JumpStrength;
    Rigidbody2D Myrigid;

    void Start()
    {
        Myrigid = GetComponent<Rigidbody2D>();
    }

	void Update ()
    {
        Myrigid.velocity = new Vector2(Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime,Myrigid.velocity.y);

        Debug.DrawLine(transform.position, transform.position + new Vector3(0, -1.5f, 0), Color.blue);

        if (Input.GetKeyDown(KeyCode.Z))
        {

            if (Physics2D.Linecast(transform.position, transform.position + new Vector3(0, -1.5f, 0), 1 << LayerMask.NameToLayer("Land")))
            {
                Myrigid.AddForce(new Vector2(0, JumpStrength));
            }
        }


        RaycastHit2D Hit = Physics2D.Raycast(transform.position, Vector2.down, 1.5f, 1<<LayerMask.NameToLayer("Enemies"));
        if (Hit)
        {
            Hit.transform.gameObject.SetActive(false);
            Myrigid.velocity = new Vector2(Myrigid.velocity.x, 0);
            Myrigid.AddForce(new Vector2(0, JumpStrength));
        }
    }
}
