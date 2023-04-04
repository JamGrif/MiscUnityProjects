using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //Player info
    internal float Health = 800;
    internal float MaxHealth = 800;
    public Slider healthbar;

    internal float Mana = 500;
    internal float MaxMana = 500;
    public Slider manabar;

    //Mana regen
    private float ManaMaxTime = 5;
    private float ManaCurrentTime = 0; 

    //Stats
    private float DamageReduction = 0.4f;

    private bool AutoAttackReady = true;
    private float AACooldown = 0;
    private float AttackSpeed = 2.4f;

    private float ChanceToHit = 0.87f;
    private bool DoICrit = false;
    private float ChanceToCrit = 0.11f;
    private float ChanceToSpellCrit = 0.17f;

    private float CritMultiply = 1.5f;
    private float SpellCritMultiply = 1.7f;

    internal float BaseDamage = 58;
    internal float Damage = 0;

    //Castbar
    internal bool Casting = false;
    public Slider castbar;
    private float CurrentCastbarTime = 0;
    private float FinishCastbarTime = 0;

    //Ability information (CT = cast time)
    private float HolyLightCT = 2.5f;
    private float ExorcismCT = 1.5f;

    //Buff / Debuff stuff
    internal bool ArdentDefender = false;
    internal bool Bash = false;
    internal bool BlackfathomHamstring = false;
    internal bool Chilled = false;
    internal bool DivineShield = false;
    internal bool Forbearance = false;
    internal bool FrozenSolid = false;
    internal bool HandOfFreedom = false;

    internal float ArdentDefenderTime = 8;
    internal float ArdentDefenderCurrentTime = 0;

    internal float BashTime = 5;
    internal float BashCurrentTime = 0;

    internal float BlackfathomHamstringTime = 8;
    internal float BlackfathomHamstringCurrentTime = 0;

    internal float ChilledTime = 30;
    internal float ChilledCurrentTime = 0;

    internal float DivineShieldTime = 8;
    internal float DivineShieldCurrentTime = 0;

    internal float ForbearanceTime = 30;
    internal float ForbearanceCurrentTime = 0;

    internal float FrozenSolidTime = 6;
    internal float FrozenSolidCurrentTime = 0;

    internal float HandOfFreedomTime = 8;
    internal float HandOfFreedomCurrentTime = 0;



    private bool CombatMode = false;

    private GameObject TargetUi; 
    internal GameObject PlayerTarget;
    private GameObject[] nearbyenemies;

    private bool Alive = true;
    private float Speed = 4f;
    private float randomnumber = 0;
    private float DivineStormHeal = 0;

    internal bool ConcecrationActive = false;
    


    private string ButtonPressed = "";
    private string savedButtonPressed = "";
    //public bool FacingDown = false;





    //References
    public GameObject ConcecrationGround;
    private GameObject ConcecrationInstan;
    public Rigidbody2D rb;
    private SpriteRenderer sr;
    public Animator animator;
    private GameObject hotbar;
    private GameObject player;

    //private Transform PlayerPosition;


    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        hotbar = GameObject.Find("HotBar");
        TargetUi = GameObject.Find("Target");
        castbar.gameObject.SetActive(false);
        player = GameObject.Find("Player");
    }

    void Start()
    {
        animator.SetBool("FacingDown", true);


    }



    void Update()
    {
        if (Alive)
        {
            //If player dies
            if (Health <= 0)
            {
                StartCoroutine(BackToMenu());
                animator.SetBool("IsDead", true);
                Alive = false;
            }

            //Update players sprite and direction
            SpriteUpdate();

            //Check for combat mode
            CombatUpdate();

            //Check what hotbar button was pressed and update depending on it
            ButtonUpdate();

            //Save the players target reference to be used to do damage
            PlayerTarget = TargetUi.GetComponent<Target>().SelectedTarget;


            //Update any ui elements
            UpdateUi();

            //If an ability is currently being cast then update it 
            if (Casting)
            {
                CastbarStuff();
            }

            //Update players buff / debuffs
            UpdateBuffDebuff();

            //Mana regen
            if (Mana != MaxMana)
            {
                if (Mana < 0) { Mana = 0; }
                ManaRegeneration();
            }

            if (!AutoAttackReady)
            {
                AACooldown += 1 * Time.deltaTime;
                if (AACooldown >= AttackSpeed)
                {
                    AACooldown = 0;
                    AutoAttackReady = true;
                }
            }

            
            //Debug.Log("AA is " + AutoAttackReady);
        }


    }



    void FixedUpdate()
    {
        //Movement
        //Player cant move if casting

        if (!Casting && Alive)
        {
            Vector2 Movement = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
            if (Movement.magnitude > 1) { Movement = Movement.normalized; }

            rb.velocity = Movement * Speed;
        }
        
        
    }


    void SpriteUpdate()
    {
        if (CrossPlatformInputManager.GetAxis("Horizontal") > 0)
        {
            sr.flipX = false;
        }
        else if (CrossPlatformInputManager.GetAxis("Horizontal") < 0)
        {
            sr.flipX = true;
        }

        animator.SetFloat("Speed", Mathf.Abs(CrossPlatformInputManager.GetAxis("Horizontal")));

        if (CrossPlatformInputManager.GetAxis("Vertical") > 0)
        {
            //FacingDown = false;
            animator.SetBool("FacingDown", false);
        }
        else if (CrossPlatformInputManager.GetAxis("Vertical") < 0)
        {
            //FacingDown = true;
            animator.SetBool("FacingDown", true);
        }

        //Update animation depending if player has target or not
        if (PlayerTarget != null)
        {
            animator.SetBool("HasTarget", true);
        }
        else
        {
            animator.SetBool("HasTarget", false);
        }
    }

    void CombatUpdate()
    {
        if (CombatMode)
        {
            //Debug.Log("Combatmode on");
        }
        if (!CombatMode)
        {
            //Debug.Log("Combatmode off");
        }
    }

    void ButtonUpdate() //Perform an ability
    {
        ButtonPressed = hotbar.GetComponent<Hotbar>().SelectedButton;
        switch (ButtonPressed)
        {
            //Retribution
            case "CrusaderStrike":
                Debug.Log("doing crusader strike...");
                animator.SetBool("IsAttacking", true);
                StartCoroutine(StopMeleeAnimation());
                if (DoIHit() == true)
                {
                    DoDamage(BaseDamage * 1.4f, true);
                }
                ButtonPressed = "";
                break;

            case "HammerOfJustice":
                Debug.Log("doing hammer of justice...");
                if (DoIHit() == true)
                {
                    StunTarget();
                }

                ButtonPressed = "";
                break;

            case "DivineStorm":
                Debug.Log("doing divine storm...");
                animator.SetBool("IsAttacking", true);
                StartCoroutine(StopMeleeAnimation());
                //Find all enemies within nearby range
                //Start with Aquamancer
                nearbyenemies = GameObject.FindGameObjectsWithTag("Aquamancer");
                //Apply approiate damage to them
                foreach (GameObject target in nearbyenemies)
                {
                    float distance = Vector3.Distance(target.transform.position, transform.position);
                    if (distance < 15)
                    {
                        target.GetComponent<Aquamancer>().RecieveDamage(BaseDamage, true);
                        DivineStormHeal += BaseDamage;
                    }
                }
                //Then do Myrmidon
                nearbyenemies = GameObject.FindGameObjectsWithTag("Myrmidon");
                foreach (GameObject target in nearbyenemies)
                {
                    float distance = Vector3.Distance(target.transform.position, transform.position);
                    if (distance < 15)
                    {
                        target.GetComponent<Myrmidon>().RecieveDamage(BaseDamage, true);
                        DivineStormHeal += BaseDamage;
                    }
                }
                //Heal for 20% of total damage
                DivineStormHeal = DivineStormHeal * 0.2f;
                RecieveHealing(DivineStormHeal,false);

                DivineStormHeal = 0;
                ButtonPressed = "";
                break;

            case "Judgement":
                Debug.Log("doing judgement...");
                animator.SetBool("IsAttacking", true);
                StartCoroutine(StopMeleeAnimation());
                if (DoIHit() == true)
                {
                    Damage = BaseDamage * 0.2f;
                    DoDamage(Damage,false);
                }
                ButtonPressed = "";
                break;

            //Holy
            case "HolyLight":
                animator.SetBool("IsCasting", true);
                FinishCastbarTime = HolyLightCT;
                Casting = true;
                savedButtonPressed = ButtonPressed;
                break;

            case "Exorcism":
                animator.SetBool("IsCasting", true);
                FinishCastbarTime = ExorcismCT;
                Casting = true;
                savedButtonPressed = ButtonPressed;
                break;

            case "Concecration":
                Debug.Log("doing concecration...");
                animator.SetBool("IsCasting", true);
                StartCoroutine(StopCastAnimation());
                ConcecrationActive = true;
                ConcecrationInstan = Instantiate(ConcecrationGround, transform.position, Quaternion.identity);
                StartCoroutine(ConcecrationDuration());


                ButtonPressed = "";
                break;

            case "LayOnHands":
                Debug.Log("doing lay on hands...");
                animator.SetBool("IsCasting", true);
                StartCoroutine(StopCastAnimation());
                ApplyBuffDebuff(ref Forbearance, ref ForbearanceCurrentTime, ref ForbearanceTime);
                RecieveHealing(MaxHealth, false);
                Mana = 0;

                ButtonPressed = "";
                break;

            //Protection
            case "DivineShield":
                Debug.Log("doing divine shield...");
                animator.SetBool("IsCasting", true);
                StartCoroutine(StopCastAnimation());
                ApplyBuffDebuff(ref DivineShield, ref DivineShieldCurrentTime, ref DivineShieldTime);

                ButtonPressed = "";
                break;

            case "HandOfFreedom":
                Debug.Log("doing hand of freedom...");
                animator.SetBool("IsCasting", true);
                StartCoroutine(StopCastAnimation());
                ApplyBuffDebuff(ref HandOfFreedom, ref HandOfFreedomCurrentTime, ref HandOfFreedomTime);


                ButtonPressed = "";
                break;

            case "ArdentDefender":
                Debug.Log("doing ardent defender...");
                animator.SetBool("IsCasting", true);
                StartCoroutine(StopCastAnimation());
                ApplyBuffDebuff(ref ArdentDefender, ref ArdentDefenderCurrentTime, ref ArdentDefenderTime);


                ButtonPressed = "";
                break;

            case "LightOfTheProtector":
                animator.SetBool("IsCasting", true);
                StartCoroutine(StopCastAnimation());
                Debug.Log("doing light of the protector...");

                ButtonPressed = "";
                break;

            
        }
        


        //ButtonPressed = "";
    }


    public void ToggleCombatMode()
    {
        CombatMode = CombatMode == true ? false : true;
    }


    public void UpdateUi()
    {
        healthbar.value = Health / MaxHealth;
        
        manabar.value = Mana / MaxMana;
        
        

    }

    public void StunTarget()
    {
        if (PlayerTarget.tag == "Myrmidon")
        {
            PlayerTarget.GetComponent<Myrmidon>().GetStunned(6);
        }
        if (PlayerTarget.tag == "Aquamancer")
        {
            PlayerTarget.GetComponent<Aquamancer>().GetStunned(6);
        }
    }


    public void CastbarStuff()
    {
        if (CurrentCastbarTime < FinishCastbarTime)
        {
            castbar.gameObject.SetActive(true);
            CurrentCastbarTime += Time.deltaTime;
            castbar.value = CurrentCastbarTime / FinishCastbarTime;
        }
        else
        {
            animator.SetBool("IsCasting", false);
            castbar.gameObject.SetActive(false);

            Casting = false;
            CurrentCastbarTime = 0;
            
            //Perform the ability that had a castbar
            if (savedButtonPressed == "Exorcism")
            {
                Debug.Log("doing exorcism...");
                if (DoIHit() == true)
                {
                    Damage = 0;
                    Damage = Random.Range(75, 125);
                    DoDamage(Damage, false);
                }
            }
            if (savedButtonPressed == "HolyLight")
            {
                Debug.Log("doing holy light...");
                randomnumber = Random.Range(130, 191);
                RecieveHealing(randomnumber, true);
            }
            savedButtonPressed = "";
        }



        //Once ability has been cast then clear the saved button
    }
    
    public void ManaRegeneration()
    {
        ManaCurrentTime += 1 * Time.deltaTime;
        if (ManaCurrentTime >= ManaMaxTime)
        {
            ManaCurrentTime = 0;
            if (!CombatMode && !Casting)
            {
                Mana += 34;
            }
            else if (CombatMode && !Casting)
            {
                Mana += 15;
            }
            else if (Casting)
            {
                Mana += 6;
            }
            if (Mana > MaxMana) { Mana = MaxMana; }
        }
    }

    public void ApplyBuffDebuff(ref bool Name, ref float NameCurrentTime, ref float NameMaxTime)
    {
        Name = true;
        NameCurrentTime = NameMaxTime;
    }

    public void UpdateBuffDebuff()
    {
        if (ArdentDefender)
        {
            ArdentDefenderCurrentTime -= 1 * Time.deltaTime;
            if (ArdentDefenderCurrentTime <= 0)
            {
                ArdentDefender = false;
            }
        }
        if (Bash)
        {
            BashCurrentTime -= 1 * Time.deltaTime;
            if (BashCurrentTime <= 0)
            {
                Bash = false;
            }
        }
        if (BlackfathomHamstring)
        {
            BlackfathomHamstringCurrentTime -= 1 * Time.deltaTime;
            if (BlackfathomHamstringCurrentTime <= 0)
            {
                BlackfathomHamstring = false;
            }
        }
        if (Chilled)
        {
            ChilledCurrentTime -= 1 * Time.deltaTime;
            if (ChilledCurrentTime <= 0)
            {
                Chilled = false;
            }
        }
        if (DivineShield)
        {
            DivineShieldCurrentTime -= 1 * Time.deltaTime;
            if (DivineShieldCurrentTime <= 0)
            {
                DivineShield = false;
            }
        }
        if (Forbearance)
        {
            ForbearanceCurrentTime -= 1 * Time.deltaTime;
            if (ForbearanceCurrentTime <= 0)
            {
                Forbearance = false;
            }
        }
        if (FrozenSolid)
        {
            FrozenSolidCurrentTime -= 1 * Time.deltaTime;
            if (FrozenSolidCurrentTime <= 0)
            {
                FrozenSolid = false;
            }
        }
        if (HandOfFreedom)
        {
            HandOfFreedomCurrentTime -= 1 * Time.deltaTime;
            if (HandOfFreedomCurrentTime <= 0)
            {
                HandOfFreedom = false;
            }
        }

    }


    public bool DoIHit()
    {
        randomnumber = Random.Range(0.0f, 1.0f);
        if (randomnumber <= ChanceToHit)
        {
            Debug.Log("i hit");
            return true;
        }
        else
        {
            Debug.Log("i missed");
            return false;
        }
    }

    public void DoDamage(float DamageToDo, bool Physical)
    {
        //Roll for crit
        randomnumber = Random.Range(0.0f, 1.0f);
        if (Physical)
        {
            if (randomnumber <= ChanceToCrit)
            {
                DoICrit = true;
            }
            else
            {
                DoICrit = false;
            }
        }
        else
        {
            if (randomnumber <= ChanceToSpellCrit)
            {
                DoICrit = true;
            }
            else
            {
                DoICrit = false;
            }
        }
        

        //If crit then increase damage
        if (DoICrit)
        {
            Debug.Log("Critical hit");
            //If damage is physical then use normal multiplier
            if (Physical)
            {
                DamageToDo = DamageToDo * CritMultiply;
            }
            //Otherwise use spell multiplier
            else
            {
                DamageToDo = DamageToDo * SpellCritMultiply;
            }
        }

        //Send damage to targets script
        if (PlayerTarget.tag == "Myrmidon")
        {
            PlayerTarget.GetComponent<Myrmidon>().RecieveDamage(DamageToDo,Physical);
        }
        if (PlayerTarget.tag == "Aquamancer")
        {
            PlayerTarget.GetComponent<Aquamancer>().RecieveDamage(DamageToDo, Physical);
        }

        DoICrit = false;

    }

    public void RecieveDamage(float Damage, bool Physical)
    {
        //If damage is physical then reduce by reduction amount
        if (Physical)
        {
            Damage = Damage * (1 - DamageReduction);
        }
        if (!DivineShield) //Cant recieve damage if under divine shield buff
        {
            if (ArdentDefender) //Recieve 20% less damage if under ardent defender
            {
                Health -= (Damage * 0.8f);
                if (Health <= 0) //If ardent defender is active and you would die, then heal health to 12% of max
                {
                    Health = (MaxHealth * 0.12f);
                }
            }
            else
            {
                //Recieve damage
                Health -= Damage;
            }
            
        }
        
    }

    public void RecieveHealing(float Heal, bool HolyLight)
    {
        //Roll for crit heal
        randomnumber = Random.Range(0.0f, 1.0f);
        if (randomnumber <= ChanceToCrit)
        {
            if (!HolyLight)
            {
                Heal = Heal * CritMultiply;
            }
            else
            {
                Heal = Heal * 2;
            }
            
        }
        Health += Heal;
        if (Health > MaxHealth) { Health = MaxHealth; }
    }


    IEnumerator ConcecrationDuration()
    {
        yield return new WaitForSeconds(6);
        ConcecrationActive = false;
        Object.Destroy(ConcecrationInstan);
    }

    IEnumerator StopCastAnimation()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("IsCasting", false);
    }

    IEnumerator StopMeleeAnimation()
    {
        yield return new WaitForSeconds(1);
        animator.SetBool("IsAttacking", false);
    }

    IEnumerator BackToMenu()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("MainMenu");
    }


    public void OnTriggerStay2D(Collider2D collision)
    {
        if (AutoAttackReady && CombatMode)
        {
            if (collision.gameObject.tag == "Myrmidon")
            {
                animator.SetBool("IsAttacking", true);
                StartCoroutine(StopMeleeAnimation());
                AutoAttackReady = false;
                collision.gameObject.GetComponent<Myrmidon>().RecieveDamage(BaseDamage, true);
                
            }
            if (collision.gameObject.tag == "Aquamancer")
            {
                animator.SetBool("IsAttacking", true);
                StartCoroutine(StopMeleeAnimation());
                AutoAttackReady = false;
                collision.gameObject.GetComponent<Aquamancer>().RecieveDamage(BaseDamage, true);

            }
        }
        
    }



}
