using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScriptCh5 : MonoBehaviour
{
    public float MoveSpeed;
    public float JumpForce;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxisRaw("KeyHorizontal") * MoveSpeed, 0);
    }
}
