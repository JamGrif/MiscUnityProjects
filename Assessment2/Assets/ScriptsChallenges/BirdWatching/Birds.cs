using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
    //enum BirdColour {red, green, blue, white};
    
    //Communicating with other scripts and deciding what colour bird is
    private int PlayerChosenColour;
    private float RandomColour;
    private int Colour;

    //Movement
    private Vector2 Position;
    private bool MovingRight;
    private int Speed;

    public Rigidbody2D rb;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        GameObject birdspawner = GameObject.Find("BirdManager");

        //Random Y height between range
        Position.y = Random.Range(-2.5f, 3.5f);

        //Decide if spawning on left or right
        int RandomXPosition = Random.Range(0, 2);
        if (RandomXPosition == 0)
        {
            Position.x = -7.5f;
            MovingRight = true;
        }
        else
        {
            Position.x = 7.5f;
            MovingRight = false;
        }

        //Move Bird to position
        this.transform.Translate(Position);

        //Random Speed
        Speed = Random.Range(4, 8);

        //Pick random colour red, green, blue or white
        float RandomNumber = Random.Range(0.0f, 10.0f);

        if (RandomNumber >= 0.0 && RandomNumber < 3.1) //Red
        {
            RandomColour = 0;
        }
        else if (RandomNumber >= 3.2 && RandomNumber < 6.3) //Green
        {
            RandomColour = 1;
        }
        else if (RandomNumber >= 6.4 && RandomNumber < 9.4) //Blue
        {
            RandomColour = 2;
        }
        else //White
        {
            RandomColour = 3;
        }


        //If birds colour is same as players chosen colour then increase AmountOfChosenBirds in player script
        PlayerChosenColour = player.GetComponent<Player>().ColourChoice; //Get players chosen colour

        if (RandomColour == 3)
        {
            player.GetComponent<Player>().AmountOfChosenBirds += 5;
        }

        if (PlayerChosenColour == RandomColour)
        {
            player.GetComponent<Player>().AmountOfChosenBirds++;
        }

        //Set birds colour to RandomColour
        if (RandomColour == 0) //Red
        {
            this.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if (RandomColour == 1) //Green
        {
            this.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (RandomColour == 2) //Blue
        {
            this.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else //White
        {
            this.GetComponent<SpriteRenderer>().color = Color.white;
        }


        //Make sures bird sprite is facing the correct way
        if (MovingRight)
        {
            this.GetComponent<SpriteRenderer>().flipX = true;
        }

    }

    void FixedUpdate() //Movement
    {
        if (MovingRight)
        {
            //rb.AddForce(new Vector2(Speed, 0));
            this.transform.Translate(new Vector2(Speed, 0) * Time.deltaTime);
        }
        else
        {
            //rb.AddForce(new Vector2(-Speed, 0));
            this.transform.Translate(new Vector2(-Speed, 0) * Time.deltaTime);
        }
    }

   



}
