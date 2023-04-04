using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffDebuff : MonoBehaviour
{
    public GameObject ArdentDefender;
    public GameObject Bash;
    public GameObject BlackfathomHamstring;
    public GameObject Chilled;
    public GameObject DivineShield;
    public GameObject Forbearance;
    public GameObject FrozenSolid;
    public GameObject HandOfFreedom;

    public Text ArdentDefenderTime;
    public Text BashTime;
    public Text BlackfathomHamstringTime;
    public Text ChilledTime;
    public Text DivineShieldTime;
    public Text ForbearanceTime;
    public Text FrozenSolidTime;
    public Text HandOfFreedomTime;

    private GameObject player;
    void Awake()
    {
        player = GameObject.Find("Player");
    }

    void Start()
    {
        ArdentDefender.SetActive(false);
        Bash.SetActive(false);
        BlackfathomHamstring.SetActive(false);
        Chilled.SetActive(false);
        DivineShield.SetActive(false);
        Forbearance.SetActive(false);
        FrozenSolid.SetActive(false);
        HandOfFreedom.SetActive(false);
    }


    //Update checks the players script to see what buffs / debuffs are active and how long is remaining on them
    void Update()
    {
        if (player.GetComponent<Player>().ArdentDefender == true)
        {
            ArdentDefender.SetActive(true);
            ArdentDefenderTime.text = player.GetComponent<Player>().ArdentDefenderCurrentTime.ToString("00");
        }
        else
        {
            ArdentDefender.SetActive(false);
        }
        if (player.GetComponent<Player>().Bash == true)
        {
            Bash.SetActive(true);
            BashTime.text = player.GetComponent<Player>().BashCurrentTime.ToString("00");
        }
        else
        {
            Bash.SetActive(false);
        }
        if (player.GetComponent<Player>().BlackfathomHamstring == true)
        {
            BlackfathomHamstring.SetActive(true);
            BlackfathomHamstringTime.text = player.GetComponent<Player>().BlackfathomHamstringCurrentTime.ToString("00");
        }
        else
        {
            BlackfathomHamstring.SetActive(false);
        }
        if (player.GetComponent<Player>().Chilled == true)
        {
            Chilled.SetActive(true);
            ChilledTime.text = player.GetComponent<Player>().ChilledCurrentTime.ToString("00");
        }
        else
        {
            Chilled.SetActive(false);
        }
        if (player.GetComponent<Player>().DivineShield == true)
        {
            DivineShield.SetActive(true);
            DivineShieldTime.text = player.GetComponent<Player>().DivineShieldCurrentTime.ToString("00");
        }
        else
        {
            DivineShield.SetActive(false);
        }
        if (player.GetComponent<Player>().Forbearance == true)
        {
            Forbearance.SetActive(true);
            ForbearanceTime.text = player.GetComponent<Player>().ForbearanceCurrentTime.ToString("00");
        }
        else
        {
            Forbearance.SetActive(false);
        }
        if (player.GetComponent<Player>().FrozenSolid == true)
        {
            FrozenSolid.SetActive(true);
            FrozenSolidTime.text = player.GetComponent<Player>().FrozenSolidCurrentTime.ToString("00");
        }
        else
        {
            FrozenSolid.SetActive(false);
        }
        if (player.GetComponent<Player>().HandOfFreedom == true)
        {
            HandOfFreedom.SetActive(true);
            HandOfFreedomTime.text = player.GetComponent<Player>().HandOfFreedomCurrentTime.ToString("00");
        }
        else
        {
            HandOfFreedom.SetActive(false);
        }








    }
}
