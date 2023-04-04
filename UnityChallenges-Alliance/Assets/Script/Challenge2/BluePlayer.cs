using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BluePlayer : MonoBehaviour
{
    private float speed = 3f;
    private bool WalkThrough = false;
    private GameObject blueplayer;
    
    void Start()
    {
        blueplayer = GameObject.FindGameObjectWithTag("BluePlayer"); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate (Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.Translate(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.Translate(Vector2.down * speed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (WalkThrough == false)
            {
                WalkThrough = true;
                Debug.Log("Blue block can walk through walls!");
                Destroy(GetComponent<Rigidbody2D>());
                
            }
            else if (WalkThrough == true)
            {
                WalkThrough = false;
                Debug.Log("Blue block can not walk through walls!");
                Rigidbody2D rigid = blueplayer.AddComponent<Rigidbody2D>();
                rigid.gravityScale = 0;
                rigid.freezeRotation = true;
            }
        }





    }
}
