using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C3Player : MonoBehaviour
{
    private int Health = 15;
    private int dot_Debuff = 0;

    private float speed = 3f;
    private bool Poisoned = false;

    public float DebuffTick = 0.5f;
    private float TimerRestart = 0;


    private bool EnteredPoison = false;

    private GameObject DungeonDoor;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            
            this.transform.Translate(Vector2.right * speed * Time.deltaTime);
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
        if (Health <= 0)
        {
            
            this.gameObject.GetComponent<UsefulScript>().KillPlayer();
        }
        
        if (EnteredPoison == true)
        {
            TimerRestart += Time.deltaTime;
            if (TimerRestart > DebuffTick)
            {
                dot_Debuff++;
                TimerRestart = 0;
            }
        }
        
        if (dot_Debuff == 10)
        {
            Poisoned = true;
            Debug.Log("POISONED");
            dot_Debuff = 0;
        }

        if (Poisoned == true)
        {
            for (int i = 0; i < 6; i++)
            {
                Health = Health - Random.Range(1, 3);
                //works but needs to do it every tick, not loop through immediatly
            }
            
            Poisoned = false;
        }
       
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Sword")
        {
           
            this.gameObject.GetComponent<UsefulScript>().KillPlayer();

        }

        if (collision.gameObject.tag == "Button")
        {
            DungeonDoor = GameObject.Find("The Freaking Dungeon Door");
            DungeonDoor.GetComponent<JustAnotherScript>().OpenDungeonDoor();
        }

    }


    private void OnTriggerEnter2D(Collider2D TEnter)
    {
        if (TEnter.gameObject.tag == "PoisonTrap")
        {
            Debug.Log("Warning: Defias Brotherhood Toxic Trap");
            EnteredPoison = true;
        }

        if (TEnter.gameObject.tag == "Dagger")
        {
            this.gameObject.GetComponent<UsefulScript>().KillPlayer();
        }
    }
    private void OnTriggerExit2D(Collider2D TExit)
    {
        if (TExit.gameObject.tag == "PoisonTrap")
        {
            EnteredPoison = false;
        }

    }

}
