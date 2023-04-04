using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RedPlayer : MonoBehaviour
{

    private int HorizSpeed = 3;
    private int VertSpeed = 3;
    
    void Start()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "BluePlayer")
        {
            SceneManager.LoadScene("Challenge_3");
        }
    }


    void Update()
    {
        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * HorizSpeed * Time.deltaTime,
                                        Input.GetAxis("Vertical") * VertSpeed * Time.deltaTime, 0));

    }
}
