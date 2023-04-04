using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScriptCh8 : MonoBehaviour
{
    private float MoveSpeed = 4f;
    private float JumpForce = 250;
    public Animator animator;

    private float HorizontalMove = 0f;
    private bool FacingRight = true;


    public AudioClip JumpSound;
    public AudioSource SoundSource;

    private void Start()
    {
        SoundSource.clip = JumpSound;
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
        if (Physics2D.Raycast(transform.position, Vector2.down, 1))
        {
            animator.SetBool("IsJumping", false);
        }

        //Jump
        if (Input.GetKeyDown(KeyCode.Z))
        {
           
            bool IsGrounded = false;
            if (Physics2D.Raycast(transform.position, Vector2.down, 1))
            {
                IsGrounded = true;
            }

            if (IsGrounded)
            {
                SoundSource.Play();
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, JumpForce));
                animator.SetBool("IsJumping", true);
            }
                
        }
    }
}
