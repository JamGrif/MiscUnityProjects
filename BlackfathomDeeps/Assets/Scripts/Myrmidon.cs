using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myrmidon : MonoBehaviour
{
    internal float Health = 400;
    internal float MaxHealth = 400;

    internal float Mana = 0;
    internal float MaxMana = 0;


    private float DamageReduction = 0.35f;
    private float AttackSpeed = 2.0f;
    private bool AttackReady = true;
    private float AttackCooldown = 0;


    private bool DoICrit = false;
    private float ChanceToHit = 0.85f;
    private float ChanceToCrit = 0.10f;
    private float CritMultiply = 1.5f;

    internal bool Stunned = false;
    internal float StunDuration = 0;

    private float ConcecrationTick = 0;

    internal bool Alive = true;

    internal float Speed = 0.5f;

    internal float AggroRange = 7f;
    internal bool WithinAggroRange = false;

    internal float MeleeRange = 3f;
    internal bool WithinMeleeRange = false;

    private GameObject player;
    //public Animator animator;
    public Sprite deadsprite;

    private float randomnumber = 0;

    void Start()
    {
        player = GameObject.Find("Player");
        //Move object to enable collision
        transform.Translate(Vector2.up*0.2f);
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
            //Check if within aggro range
            if (transform.position.x > player.transform.position.x + AggroRange || transform.position.x < player.transform.position.x - AggroRange || transform.position.y > player.transform.position.y + AggroRange || transform.position.y < player.transform.position.y - AggroRange)
            {
                WithinAggroRange = false;
                //Debug.Log("not in aggro range");
            }
            else
            {
                WithinAggroRange = true;
            }

            //Check if within melee range
            if (transform.position.x > player.transform.position.x + MeleeRange || transform.position.x < player.transform.position.x - MeleeRange || transform.position.y > player.transform.position.y + MeleeRange || transform.position.y < player.transform.position.y - MeleeRange)
            {
                WithinMeleeRange = false;
                //Debug.Log("not in melee range");
            }
            else
            {
                WithinMeleeRange = true;
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
                if (WithinAggroRange)
                {
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, Speed * Time.deltaTime);
                }

                //If within melee range then do attack player ability and can do next ability 
                if (WithinMeleeRange && AttackReady)
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
            DamageToDo = DamageToDo * CritMultiply;
        }

        player.GetComponent<Player>().RecieveDamage(DamageToDo, true);

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
