using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScriptCh4 : MonoBehaviour
{
    private float MoveSpeed = 8f;
    private float JumpForce = 250f;

    public Rigidbody2D rb;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            bool IsGrounded=false;
            if(Physics2D.Raycast(transform.position,Vector2.down, 0.5f))
            {
                IsGrounded = true;
            }

            if(IsGrounded)
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.AddForce(new Vector2(-MoveSpeed, 0));
            //transform.Translate(new Vector2(-MoveSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.AddForce(new Vector2(MoveSpeed, 0));
            //transform.Translate(new Vector2(MoveSpeed * Time.deltaTime, 0));
        }
    }
}
