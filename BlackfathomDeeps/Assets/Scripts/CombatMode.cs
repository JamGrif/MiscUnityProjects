using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatMode : MonoBehaviour
{
    private bool CombatModeOn = false;

    public Sprite On;
    public Sprite Off;

    public void TogglePic()
    {
        if (CombatModeOn)
        {
            CombatModeOn = false;
            GetComponent<Image>().sprite = Off;
        }
        else
        {
            CombatModeOn = true;
            GetComponent<Image>().sprite = On;
        }
    }
}
