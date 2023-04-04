using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{

    public float MoveSpeed = 6f;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
        /*if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector2(-MoveSpeed * Time.deltaTime, 0));
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector2(MoveSpeed * Time.deltaTime, 0));
        }*/
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("KeyHorizontal") * MoveSpeed, 0);
    }

}
