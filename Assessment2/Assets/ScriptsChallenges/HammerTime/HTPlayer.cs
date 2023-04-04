using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class HTPlayer : MonoBehaviour
{
    //Player info
    private int Health = 35;
    private float Speed = 4f;
    public Rigidbody2D rb;
    internal bool PlayerAlive = true;

    //Ability info
    private int AbilityCooldownTime = 10;
    private bool AbilityReady = true;

    //Thunderbolt
    public GameObject ThunderboltSprite;
    private SpriteRenderer TB;
    private bool ThunderboltActive = false;

    //Shocking Explosion
    public GameObject ShockingExplosionSprite;
    private SpriteRenderer SE;
    private bool ExplosionActive = false;

    //Hammer info
    internal bool HoldingHammer = true;
    public GameObject HammerPrefab;
    public GameObject HammerInHand;
    private GameObject hammer;
    private int HammerSpeed = 4;

    public GameObject YouLose;

    

    void Start()
    {
        ThunderboltSprite.SetActive(false);
        ShockingExplosionSprite.SetActive(false);
        YouLose.SetActive(false);
    }

    void FixedUpdate()
    {
        Vector2 Movement = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
        if (Movement.magnitude > 1) { Movement = Movement.normalized; }

        rb.velocity = Movement * Speed;

        if (HoldingHammer) 
        {
            HammerInHand.SetActive(true);   
            if (Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal_F")) > 0 ||
               Mathf.Abs(CrossPlatformInputManager.GetAxis("Vertical_F")) > 0)
            {
                hammer = Instantiate(HammerPrefab, transform.position, Quaternion.identity);
                
                hammer.GetComponent<Rigidbody2D>().velocity = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal_F"), CrossPlatformInputManager.GetAxis("Vertical_F")).normalized * HammerSpeed;
                HoldingHammer = false;
                
            }

            

        }
        else
        {
            HammerInHand.SetActive(false);
        }
        
    }

    void Update()
    {
        if (ThunderboltActive) //Show Thunderbolt and constantly raycast checking to see if hit wyverns
        {
            ThunderboltSprite.SetActive(true);
        }
        else
        {
            ThunderboltSprite.SetActive(false);
        }

        if (ExplosionActive) //Show Explosion 
        {
            ShockingExplosionSprite.SetActive(true);
        }

        if (Health <= 0) //Player has died
        {
            YouLose.SetActive(true);
            PlayerAlive = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Gryphon_1").GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Find("Hammer").GetComponent<SpriteRenderer>().enabled = false;
            Destroy(this);
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Arrow")
        {
            Health -= Random.Range(15, 26);
            Debug.Log("Player is on " + Health + " health");
        }
        
    }


    public void ShockingExplosion()
    {
        if (AbilityReady)
        {
            StartCoroutine(AbilityCooldown());
            StartCoroutine(HideExplosion());
            Debug.Log("Shocking explosion used!");
            AbilityReady = false;
            ExplosionActive = true;
            ShockingExplosionSprite.SetActive(true);



        }
        else
        {
            Debug.Log("Ability on cooldown!");
        }
    }

    //Thunderbolt isnt done by raycast. wyverns just look for the Thunderbolt trigger from the object
    public void Thunderbolt()
    {
        if (AbilityReady)
        {
            StartCoroutine(AbilityCooldown());
            Debug.Log("Thunderbolt used!");
            AbilityReady = false;
            ThunderboltActive = true;
            ThunderboltSprite.SetActive(true);


        }
        else
        {
            Debug.Log("Ability on cooldown!");
        }
    }

    IEnumerator AbilityCooldown()
    {
        yield return new WaitForSeconds(AbilityCooldownTime);
        AbilityReady = true;
        ThunderboltSprite.SetActive(false);
        ThunderboltActive = false;
    }

    IEnumerator HideExplosion()
    {
        yield return new WaitForSeconds(1);
        ExplosionActive = false;
        ShockingExplosionSprite.SetActive(false);
    }



}
