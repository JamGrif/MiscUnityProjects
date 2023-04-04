using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScriptCh6 : MonoBehaviour
{
    private float MoveSpeed = 3f;
    private float JumpForce = 250;
    public Animator animator;

    private float HorizontalMove = 0f;
    private bool FacingRight = true;

    private float ClimbingSpeed = 3f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Player entered ladder
        if (collision.gameObject.tag == "Ladder")
        {
            animator.SetBool("IsClimbing", true);
            if (Input.GetKey(KeyCode.UpArrow))
            {
                this.GetComponent<Rigidbody2D>().gravityScale = 0;
                transform.Translate(new Vector2(0, ClimbingSpeed * Time.deltaTime));
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                this.GetComponent<Rigidbody2D>().gravityScale = 0;
                transform.Translate(new Vector2(0, -ClimbingSpeed * Time.deltaTime));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Player left ladder
        if (collision.gameObject.tag == "Ladder")
        {
            animator.SetBool("IsClimbing", false);
            this.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        //Move left / right
        HorizontalMove = Input.GetAxis("KeyHorizontal") * Time.deltaTime * MoveSpeed;
        transform.Translate(new Vector2(HorizontalMove, 0));

        //Flip character to face correct way
        if (HorizontalMove > 0 && FacingRight == false)
        {
            FacingRight = true;
            this.GetComponent<SpriteRenderer>().flipX = false;
        }
        else if (HorizontalMove < 0 && FacingRight == true)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
            FacingRight = false;
        }

        //Set animation
        animator.SetFloat("Speed", Mathf.Abs(HorizontalMove));

        //Check if player is on the ground
        if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f))
        {
            animator.SetBool("IsJumping", false);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Z))
        {
            bool IsGrounded = false;
            if (Physics2D.Raycast(transform.position, Vector2.down, 0.5f))
            {
                IsGrounded = true;
            }

            if (IsGrounded)
            {
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
                animator.SetBool("IsJumping", true);
            }
                
        }
    }
}
