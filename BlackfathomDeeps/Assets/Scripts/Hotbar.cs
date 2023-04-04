using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hotbar : MonoBehaviour
{
    private GameObject player;
    private float playermana = 0;
    private float ManaCalculation = 0;

    public GameObject Retribution;
    public GameObject Holy;
    public GameObject Protection;

    internal string SelectedButton = "";

    private Vector3 FirstTouchPos;   //First touch position
    private Vector3 LastTouchPos;   //Last touch position
    private int ScreenPercentageForSwipe = 15;
    private float dragDistance;  //minimum distance for a swipe to be registered

    //Used to display if ability is on cooldown or not
    //Retribution
    public GameObject CrusaderStrikeRed;
    public GameObject HammerOfJusticeRed;
    public GameObject DivineStormRed;
    public GameObject JudgementRed;

    private bool CrusaderStrikeCD = false;
    private bool HammerOfJusticeCD = false;
    private bool DivineStormCD = false;
    private bool JudgementCD = false;

    private float CrusaderStrikeTime = 4.5f;
    private float CrusaderStrikeCurrentTime = 0;

    private float HammerOfJusticeTime = 60f;
    private float HammerOfJusticeCurrentTime = 0;

    private float DivineStormTime = 10f;
    private float DivineStormCurrentTime = 0;

    private float JudgementTime = 8f;
    private float JudgementCurrentTime = 0;

    public Text CrusaderStrikeTimeDisplay;
    public Text CrusaderStrikeSecondMinuteDisplay;

    public Text HammerOfJusticeTimeDisplay;
    public Text HammerOfJusticeSecondMinuteDisplay;

    public Text DivineStormTimeDisplay;
    public Text DivineStormSecondMinuteDisplay;

    public Text JudgementTimeDisplay;
    public Text JudgementSecondMinuteDisplay;

    //Holy
    public GameObject HolyLightRed;
    public GameObject ExorcismRed;
    public GameObject ConcecrationRed;
    public GameObject LayOnHandsRed;

    private bool HolyLightCD = false;
    private bool ExorcismCD = false;
    private bool ConcecrationCD = false;
    private bool LayOnHandsCD = false;

    private float HolyLightTime = 2.5f;
    private float HolyLightCurrentTime = 0;

    private float ExorcismTime = 15;
    private float ExorcismCurrentTime = 0;

    private float ConcecrationTime = 0;
    private float ConcecrationCurrentTime = 0;

    private float LayOnHandsTime = 600f;
    private float LayOnHandsCurrentTime = 0;

    public Text HolyLightTimeDisplay;
    public Text HolyLightSecondMinuteDisplay;

    public Text ExorcismTimeDisplay;
    public Text ExorcismSecondMinuteDisplay;

    public Text ConcecrationTimeDisplay;
    public Text ConcecrationSecondMinuteDisplay;

    public Text LayOnHandsTimeDisplay;
    public Text LayOnHandsSecondMinuteDisplay;

    //Protection
    public GameObject DivineShieldRed;
    public GameObject HandOfFreedomRed;
    public GameObject ArdentDefenderRed;
    public GameObject LightOfTheProtectorRed;

    private bool DivineShieldCD = false;
    private bool HandOfFreedomCD = false;
    private bool ArdentDefenderCD = false;
    private bool LightOfTheProtectorCD = false;

    private float DivineShieldTime = 300f;
    private float DivineShieldCurrentTime = 0;

    private float HandOfFreedomTime = 25;
    private float HandOfFreedomCurrentTime = 0;

    private float ArdentDefenderTime = 60;
    private float ArdentDefenderCurrentTime = 0;

    private float LightOfTheProtectorTime = 30f;
    private float LightOfTheProtectorCurrentTime = 0;

    public Text DivineShieldTimeDisplay;
    public Text DivineShieldSecondMinuteDisplay;

    public Text HandOfFreedomTimeDisplay;
    public Text HandOfFreedomSecondMinuteDisplay;

    public Text ArdentDefenderTimeDisplay;
    public Text ArdentDefenderSecondMinuteDisplay;

    public Text LightOfTheProtectorTimeDisplay;
    public Text LightOfTheProtectorSecondMinuteDisplay;

    void Start()
    {
        Holy.SetActive(false);
        Protection.SetActive(false);

        dragDistance = Screen.height * ScreenPercentageForSwipe / 100; //dragDistance is N% height of the screen

        player = GameObject.Find("Player");
    }


    void Update()
    {
        //Update cooldown displays
        CooldownDisplay(ref CrusaderStrikeCD, ref CrusaderStrikeRed, ref CrusaderStrikeCurrentTime, ref CrusaderStrikeSecondMinuteDisplay, ref CrusaderStrikeTimeDisplay);
        CooldownDisplay(ref HammerOfJusticeCD, ref HammerOfJusticeRed, ref HammerOfJusticeCurrentTime, ref HammerOfJusticeSecondMinuteDisplay, ref HammerOfJusticeTimeDisplay);
        CooldownDisplay(ref DivineStormCD, ref DivineStormRed, ref DivineStormCurrentTime, ref DivineStormSecondMinuteDisplay, ref DivineStormTimeDisplay);
        CooldownDisplay(ref JudgementCD, ref JudgementRed, ref JudgementCurrentTime, ref JudgementSecondMinuteDisplay, ref JudgementTimeDisplay);

        CooldownDisplay(ref HolyLightCD, ref HolyLightRed, ref HolyLightCurrentTime, ref HolyLightSecondMinuteDisplay, ref HolyLightTimeDisplay);
        CooldownDisplay(ref ExorcismCD, ref ExorcismRed, ref ExorcismCurrentTime, ref ExorcismSecondMinuteDisplay, ref ExorcismTimeDisplay);
        CooldownDisplay(ref ConcecrationCD, ref ConcecrationRed, ref ConcecrationCurrentTime, ref ConcecrationSecondMinuteDisplay, ref ConcecrationTimeDisplay);
        CooldownDisplay(ref LayOnHandsCD, ref LayOnHandsRed, ref LayOnHandsCurrentTime, ref LayOnHandsSecondMinuteDisplay, ref LayOnHandsTimeDisplay);

        CooldownDisplay(ref DivineShieldCD, ref DivineShieldRed, ref DivineShieldCurrentTime, ref DivineShieldSecondMinuteDisplay, ref DivineShieldTimeDisplay);
        CooldownDisplay(ref HandOfFreedomCD, ref HandOfFreedomRed, ref HandOfFreedomCurrentTime, ref HandOfFreedomSecondMinuteDisplay, ref HandOfFreedomTimeDisplay);
        CooldownDisplay(ref ArdentDefenderCD, ref ArdentDefenderRed, ref ArdentDefenderCurrentTime, ref ArdentDefenderSecondMinuteDisplay, ref ArdentDefenderTimeDisplay);
        CooldownDisplay(ref LightOfTheProtectorCD, ref LightOfTheProtectorRed, ref LightOfTheProtectorCurrentTime, ref LightOfTheProtectorSecondMinuteDisplay, ref LightOfTheProtectorTimeDisplay);

        //Check for swipes to change sets
        if (Input.touchCount == 1) // user is touching the screen with a single touch
        {
            Touch touch = Input.GetTouch(0); // get the touch
            if (touch.phase == TouchPhase.Began) //check for the first touch
            {
                FirstTouchPos = touch.position;
                LastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved) // update the last position based on where they moved
            {
                LastTouchPos = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended) //check if the finger is removed from the screen
            {
                LastTouchPos = touch.position;  //last touch position. Ommitted if you use list

                //Check if drag distance is greater than N% of the screen height
                //It's a drag
                if (Mathf.Abs(LastTouchPos.x - FirstTouchPos.x) > dragDistance || Mathf.Abs(LastTouchPos.y - FirstTouchPos.y) > dragDistance)
                {
                    //check if the drag is vertical or horizontal
                    if (Mathf.Abs(LastTouchPos.x - FirstTouchPos.x) > Mathf.Abs(LastTouchPos.y - FirstTouchPos.y))
                    {

                        //If the horizontal movement is greater than the vertical movement...
                        if ((LastTouchPos.x > FirstTouchPos.x))  //If the movement was to the right
                        {
                            //Right swipe
                            Retribution.SetActive(true);
                            Holy.SetActive(false);
                            Protection.SetActive(false);
                        }
                        else
                        {
                            //Left swipe
                            Retribution.SetActive(false);
                            Holy.SetActive(true);
                            Protection.SetActive(false);
                        }
                    }
                    else
                    {
                        //the vertical movement is greater than the horizontal movement
                        if (LastTouchPos.y > FirstTouchPos.y)  //If the movement was up
                        {
                            //Up swipe
                            Retribution.SetActive(false);
                            Holy.SetActive(false);
                            Protection.SetActive(true);
                        }
                    }
                }
            }
        }
    }


    void LateUpdate()
    {
        SelectedButton = "";
    }


    void CooldownDisplay(ref bool AbilityCD, ref GameObject AbilityRed, ref float AbilityCurrentTime, ref Text AbilitySecondMinuteDisplay, ref Text AbilityTimeDisplay)
    {
        if (AbilityCD) //abilty on cooldown so show red picture and update and check time
        {
            AbilityRed.SetActive(true);
            
            if (AbilityCurrentTime > 0)
            {
                //Work out if time remaining needs to be shown in second or minutes
                if (AbilityCurrentTime > 60)
                {
                    AbilitySecondMinuteDisplay.text = "M";
                    AbilityTimeDisplay.text = (AbilityCurrentTime / 60).ToString("00");
                }
                else
                {
                    AbilitySecondMinuteDisplay.text = "S";
                    AbilityTimeDisplay.text = AbilityCurrentTime.ToString("00");
                }
                //Reduce ability cooldown time by one second
                AbilityCurrentTime -= 1 * Time.deltaTime;
            }
            else
            {
                AbilityCD = false;
            }
        }
        else
        {
            AbilityRed.SetActive(false);
        }
    }

    //All abilities require the player is not currently casting in order to work
    //Retribution
    public void CrusaderStrike()
    {
        if (player.GetComponent<Player>().PlayerTarget != null)
        {
            //Requires 10% of mana and enemy target
            if (!CrusaderStrikeCD && !player.GetComponent<Player>().Casting && (player.GetComponent<Player>().PlayerTarget.tag == "Myrmidon" || player.GetComponent<Player>().PlayerTarget.tag == "Aquamancer"))
            {
                playermana = player.GetComponent<Player>().Mana;
                ManaCalculation = ((500 / 100) * 10);
                if (ManaCalculation <= playermana)
                {
                    player.GetComponent<Player>().Mana -= ManaCalculation;
                    SelectedButton = "CrusaderStrike";
                    CrusaderStrikeCD = true;
                    CrusaderStrikeCurrentTime = CrusaderStrikeTime;
                }
            }
        }
    }
    public void HammerOfJustice()
    {
        //Requires 3.5% of mana and enemy target
        if (player.GetComponent<Player>().PlayerTarget != null)
        {
            if (!HammerOfJusticeCD && !player.GetComponent<Player>().Casting && (player.GetComponent<Player>().PlayerTarget.tag == "Myrmidon" || player.GetComponent<Player>().PlayerTarget.tag == "Aquamancer"))
            {
                playermana = player.GetComponent<Player>().Mana;
                ManaCalculation = ((500 / 100) * 3.5f);
                if (ManaCalculation <= playermana)
                {
                    player.GetComponent<Player>().Mana -= ManaCalculation;
                    SelectedButton = "HammerOfJustice";
                    HammerOfJusticeCD = true;
                    HammerOfJusticeCurrentTime = HammerOfJusticeTime;
                }
            }
        }
        
    }
    public void DivineStorm()
    {
        //Requires 5% of mana 
        if (!DivineStormCD && !player.GetComponent<Player>().Casting)
        {
            playermana = player.GetComponent<Player>().Mana;
            ManaCalculation = ((500 / 100) * 5f);
            if (ManaCalculation <= playermana)
            {
                player.GetComponent<Player>().Mana -= ManaCalculation;
                SelectedButton = "DivineStorm";
                DivineStormCD = true;
                DivineStormCurrentTime = DivineStormTime;
            }
        }
    }
    public void Judgement()
    {
        //Requires 3% of mana and enemy target
        if (player.GetComponent<Player>().PlayerTarget != null)
        {
            if (!JudgementCD && !player.GetComponent<Player>().Casting && (player.GetComponent<Player>().PlayerTarget.tag == "Myrmidon" || player.GetComponent<Player>().PlayerTarget.tag == "Aquamancer"))
            {
                playermana = player.GetComponent<Player>().Mana;
                ManaCalculation = ((500 / 100) * 5f);
                if (ManaCalculation <= playermana)
                {
                    player.GetComponent<Player>().Mana -= ManaCalculation;
                    SelectedButton = "Judgement";
                    JudgementCD = true;
                    JudgementCurrentTime = JudgementTime;
                }
            }
        }
    }

    //Holy
    public void HolyLight()
    {
        //Requires 13% of mana
        if (!HolyLightCD && !player.GetComponent<Player>().Casting)
        {
            playermana = player.GetComponent<Player>().Mana;
            ManaCalculation = ((500 / 100) * 13f);
            if (ManaCalculation <= playermana)
            {
                player.GetComponent<Player>().Mana -= ManaCalculation;
                SelectedButton = "HolyLight";
                HolyLightCD = true;
                HolyLightCurrentTime = HolyLightTime;
            }
        }
    }
    public void Exorcism()
    {
        //Requires 8% of mana and enemy target
        if (player.GetComponent<Player>().PlayerTarget != null)
        {
            if (player.GetComponent<Player>().PlayerTarget != null)
            {
                if (!ExorcismCD && !player.GetComponent<Player>().Casting && (player.GetComponent<Player>().PlayerTarget.tag == "Myrmidon" || player.GetComponent<Player>().PlayerTarget.tag == "Aquamancer"))
                {
                    playermana = player.GetComponent<Player>().Mana;
                    ManaCalculation = ((500 / 100) * 8f);
                    if (ManaCalculation <= playermana)
                    {
                        player.GetComponent<Player>().Mana -= ManaCalculation;
                        SelectedButton = "Exorcism";
                        ExorcismCD = true;
                        ExorcismCurrentTime = ExorcismTime;
                    }
                }
            }
        }
    }
    public void Concecration()
    {
        //Requires 15% of mana and Concecration not currently active
        if (!ConcecrationCD && !player.GetComponent<Player>().Casting && !player.GetComponent<Player>().ConcecrationActive)
        {
            playermana = player.GetComponent<Player>().Mana;
            ManaCalculation = ((500 / 100) * 15f);
            if (ManaCalculation <= playermana)
            {
                player.GetComponent<Player>().Mana -= ManaCalculation;
                SelectedButton = "Concecration";
                ConcecrationCD = true;
                ConcecrationCurrentTime = ConcecrationTime;
            }
        }
    }
    public void LayOnHands()
    {
        //Requires 100% of mana and NO forbearance debuff
        if (!LayOnHandsCD && !player.GetComponent<Player>().Casting && !player.GetComponent<Player>().Forbearance)
        {
            playermana = player.GetComponent<Player>().Mana;
            ManaCalculation = ((500 / 100) * 100f);
            if (ManaCalculation <= playermana)
            {
                player.GetComponent<Player>().Mana -= ManaCalculation;
                SelectedButton = "LayOnHands";
                LayOnHandsCD = true;
                LayOnHandsCurrentTime = LayOnHandsTime;
            }
        }
    }

    //Protection
    public void DivineShield()
    {
        //-
        if (!DivineShieldCD && !player.GetComponent<Player>().Casting)
        {
            SelectedButton = "DivineShield";
            DivineShieldCD = true;
            DivineShieldCurrentTime = DivineShieldTime;
        }
    }
    public void HandOfFreedom()
    {
        //Requires 15% of mana
        if (!HandOfFreedomCD && !player.GetComponent<Player>().Casting)
        {
            playermana = player.GetComponent<Player>().Mana;
            ManaCalculation = ((500 / 100) * 15f);
            if (ManaCalculation <= playermana)
            {
                player.GetComponent<Player>().Mana -= ManaCalculation;
                SelectedButton = "HandOfFreedom";
                HandOfFreedomCD = true;
                HandOfFreedomCurrentTime = HandOfFreedomTime;
            }
        }
    }
    public void ArdentDefender()
    {
        //-
        if (!ArdentDefenderCD && !player.GetComponent<Player>().Casting)
        {
            SelectedButton = "ArdentDefender";
            ArdentDefenderCD = true;
            ArdentDefenderCurrentTime = ArdentDefenderTime;
        }
    }
    public void LightOfTheProtector()
    {
        //-
        if (!LightOfTheProtectorCD && !player.GetComponent<Player>().Casting)
        {
            SelectedButton = "LightOfTheProtector";
            LightOfTheProtectorCD = true;
            LightOfTheProtectorCurrentTime = LightOfTheProtectorTime;
        }
    }

    




}
