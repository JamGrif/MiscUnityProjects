using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aquamancer : MonoBehaviour
{
    internal float Health = 250;
    internal float MaxHealth = 250;

    internal float Mana = 550;
    internal float MaxMana = 550;

    private float DamageReduction = 0.2f;
    private float AttackSpeed = 2.3f;

    private float ChanceToHit = 0.7f;
    private float ChanceToCrit = 0.05f;
    //private float ChanceToSpellCrit = 0.18f;

    private bool AttackReady = true;
    private float AttackCooldown = 0;

    private bool DoICrit = false;
    internal bool Stunned = false;
    internal float StunDuration = 0;

    private float ConcecrationTick = 0;

    internal bool Alive = true;
    private float randomnumber = 0;
    private GameObject player;

    public Sprite deadsprite;

    internal float SpellRange = 3f;
    internal bool WithinSpellRange = false;

    void Start()
    {
        player = GameObject.Find("Player");
        //Move object to enable collision
        transform.Translate(Vector2.up * 0.2f);
    }

    void Update()
    {
        //If stunned then update time left
        if (Stunned)
        {
            if (StunDuration > 0)
            {
                StunDuration -= 1 * Time.deltaTime;
            }
            else
            {
                Stunned = false;
            }
        }

        //Check if creature is alive or not
        if (Health <= 0)
        {
            //animator.SetBool("IsDead", true);
            GetComponent<SpriteRenderer>().sprite = deadsprite;
            Alive = false;
        }

        if (Alive)
        {
            //Check if within spell range
            if (transform.position.x > player.transform.position.x + SpellRange || transform.position.x < player.transform.position.x - SpellRange || transform.position.y > player.transform.position.y + SpellRange || transform.position.y < player.transform.position.y - SpellRange)
            {
                WithinSpellRange = false;
            }
            else
            {
                WithinSpellRange = true;
            }


            if (!AttackReady)
            {
                AttackCooldown += 1 * Time.deltaTime;
                if (AttackCooldown >= AttackSpeed)
                {
                    AttackCooldown = 0;
                    AttackReady = true;
                }
            }

            if (!Stunned)
            {
                //If within spell range then do attack player ability and can do next ability 
                if (WithinSpellRange && AttackReady)
                {
                    randomnumber = Random.Range(20, 60);
                    DoDamage(randomnumber);
                    AttackReady = false;
                }
            }
        }
        



    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ConcecrationGround")
        {
            Debug.Log(ConcecrationTick);
            ConcecrationTick += 1 * Time.deltaTime;
            if (ConcecrationTick >= 2)
            {
                ConcecrationTick = 0;
                RecieveDamage(220, false);
            }
        }
    }


    public bool DoIHit()
    {
        randomnumber = Random.Range(0.0f, 1.0f);
        if (randomnumber <= ChanceToHit)
        {
            Debug.Log("myrmidon hit");
            return true;
        }
        else
        {
            Debug.Log("myrmidon missed");
            return false;
        }
    }

    public void DoDamage(float DamageToDo)
    {
        //Roll for crit
        randomnumber = Random.Range(0.0f, 1.0f);
        if (randomnumber <= ChanceToCrit)
        {
            DoICrit = true;
        }
        else
        {
            DoICrit = false;
        }

        //If crit then increase damage
        if (DoICrit)
        {
            Debug.Log("Critical hit");
            DamageToDo = DamageToDo * 1.4f;
        }

        player.GetComponent<Player>().RecieveDamage(DamageToDo, false);

        DoICrit = false;

    }

    public void RecieveDamage(float Damage, bool Physical)
    {
        //If damage is physical then reduce by reduction amount
        if (Physical)
        {
            Damage = Damage * (1 - DamageReduction);
        }

        //Minus Health by damage
        Health -= Damage;
    }

    public void GetStunned(float StunTime)
    {
        Stunned = true;
        StunDuration = StunTime;
    }



}
