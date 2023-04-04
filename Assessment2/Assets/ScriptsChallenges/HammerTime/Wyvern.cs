using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wyvern : MonoBehaviour
{
    private int Health = 35;
    private int ArrowCooldown = 2;
    private int Speed = 0;

    private GameObject player;
    private Transform playerPosition;

    public GameObject ArrowPrefab;
    private GameObject arrow;
    private bool ThrowAnotherArrow = false;

    private SpriteRenderer sr;

    private int SpawningRandomNumber = 0;
    private Vector3 OutsideMapPosition;
    private Vector3 InsideMapPosition;

    

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Start()
    {
        Speed = Random.Range(7, 13);
        OutsideMapPosition.z = 0;
        InsideMapPosition.z = 0;
        //Needs to spawn in a random position between and x and y range OFF SCREEN
        //First decide if Wyvern will spawn left, above or right of screen
        SpawningRandomNumber = Random.Range(1, 4);

        if (SpawningRandomNumber == 1) //Left
        {
            //Create the spawn point of the wyvern
            OutsideMapPosition.x = -15;
            OutsideMapPosition.y = Random.Range(4f, -2f);

            //Move wyvern to outside the map
            transform.position = OutsideMapPosition;

            //Create the position of the wyvern when inside the map
            InsideMapPosition.x = Random.Range(-6f, -10f);
            InsideMapPosition.y = OutsideMapPosition.y;

            //Move the wyvern into the map
            transform.position = Vector3.MoveTowards(OutsideMapPosition, InsideMapPosition, Speed*Time.deltaTime);
            //transform.position = InsideMapPosition;
            //Debug.Log(InsideMapPosition.x);
        }
        else if (SpawningRandomNumber == 2) //Above
        {
            //Create the spawn point of the wyvern
            OutsideMapPosition.x = Random.Range(-7f, 7f);
            OutsideMapPosition.y = 7;

            //Move wyvern to outside the map
            transform.position = OutsideMapPosition;

            //Create the position of the wyvern when inside the map
            InsideMapPosition.x = OutsideMapPosition.x;
            InsideMapPosition.y = Random.Range(1.5f,4f);

            //Move the wyvern into the map
            transform.position = Vector2.MoveTowards(OutsideMapPosition, InsideMapPosition, Speed * Time.deltaTime);
            //transform.position = InsideMapPosition;


        }
        else //Right
        {
            //Create the spawn point of the wyvern
            OutsideMapPosition.x = 15;
            OutsideMapPosition.y = Random.Range(4f, -2f);

            //Move wyvern to outside the map
            transform.position = OutsideMapPosition;

            //Create the position of the wyvern when inside the map
            InsideMapPosition.x = Random.Range(6f, 10f);
            InsideMapPosition.y = OutsideMapPosition.y;

            
            
        }



        

    }

    
    void Update()
    {
        //If health is 0 or lower then wyvern dies
        if (Health <= 0)
        {
            //Tell WyvernSpawn script that this wyvern has died by reducing AliveWyvern variable by one
            GameObject.Find("WyvernSpawner").GetComponent<WyvernSpawn>().AliveWyvern-=1;
            Object.Destroy(gameObject);
        }

        //Move the wyvern into the map. Runs if the wyverns current position is not set to InsideMapPosition
        if (transform.position != InsideMapPosition)
        {
            transform.position = Vector2.MoveTowards(transform.position, InsideMapPosition, Speed * Time.deltaTime);
        }
        

        //Get players new position
        playerPosition = player.transform;

       //Flips wyverns sprite X direction depending if the player is on the left or right of them
        sr.flipX = playerPosition.position.x < transform.position.x ?  true : false;

        //Flips wyverns sprite Y direction depending if the player is above or below them
        sr.flipY = playerPosition.position.y > transform.position.y ? true : false;

        if (player.GetComponent<HTPlayer>().PlayerAlive == true)
        {
            if (!ThrowAnotherArrow)
            {
                ThrowAnotherArrow = true;
                StartCoroutine(ThrowArrow());
            }
        }
        else
        {
            Destroy(this);
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Hammer")
        {
            Health -= Random.Range(20, 31);
        }

        if (collision.gameObject.tag == "ShockingExplosion")
        {
            Health -= Random.Range(25, 51);
        }

        if (collision.gameObject.tag == "Thunderbolt")
        {
            Health -= Random.Range(30, 41);
        }
    }

    IEnumerator ThrowArrow()
    {
        //Debug.Log("Arrow been thrown");
        //Throw arrow stuff here
        arrow = Instantiate(ArrowPrefab, transform.position, Quaternion.identity);
        Object.Destroy(arrow, 4);
        yield return new WaitForSeconds(ArrowCooldown);
        ThrowAnotherArrow = false;

    }




}
