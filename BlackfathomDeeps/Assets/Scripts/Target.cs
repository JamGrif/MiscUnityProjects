using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target : MonoBehaviour
{
    //public Sprite PlayerPic;
    public Sprite TAPic;
    public Sprite BMPic;
    public Sprite NoPic;

    //internal string SelectedTarget = "";

    public GameObject ObjectClickedOn;

    public GameObject SelectedTarget;

    public GameObject TargetPicBorder;
    public GameObject TargetPic;
    public Slider TargetHealth;
    public Slider TargetMana;

    public GameObject TargetStunned;
    public Text StunTimeLeft;

    private GameObject player;

    private float MaxRange = 9f;

    void Awake()
    {
        ToggleTargetUi(false);
        player = GameObject.Find("Player");
    }

    
    void Update()
    {
        //If player taps screen then find what object they tapped

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //Debug.Log("Clicked on screen");
            Vector3 Pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 TouchPos = new Vector2(Pos.x, Pos.y);

            var Hit = Physics2D.OverlapPoint(TouchPos);

            if (Hit)
            {
                ObjectClickedOn = Hit.gameObject;

                if (ObjectClickedOn.tag == "Myrmidon")
                {
                    if (ObjectClickedOn.GetComponent<Myrmidon>().Alive == true)
                    {
                        Debug.Log("Clicked on Myrmidon");
                        TargetPic.GetComponent<Image>().sprite = BMPic;
                        ToggleTargetUi(true);
                        SelectedTarget = ObjectClickedOn;
                    }
                }
                else if (ObjectClickedOn.tag == "Aquamancer")
                {
                    if (ObjectClickedOn.GetComponent<Aquamancer>().Alive == true)
                    {
                        SelectedTarget = ObjectClickedOn;
                        Debug.Log("Clicked on Aquamancer");
                        TargetPic.GetComponent<Image>().sprite = TAPic;
                        ToggleTargetUi(true);
                    }
                    
                }
                ObjectClickedOn = null;
            }
        }


        if (SelectedTarget != null)
        {
            //Update targets health, mana and other UI
            if (SelectedTarget.tag == "Myrmidon")
            {

                TargetHealth.value = SelectedTarget.GetComponent<Myrmidon>().Health / SelectedTarget.GetComponent<Myrmidon>().MaxHealth;
                TargetMana.value = SelectedTarget.GetComponent<Myrmidon>().Mana / SelectedTarget.GetComponent<Myrmidon>().MaxMana;
                if (SelectedTarget.GetComponent<Myrmidon>().Stunned == true)
                {
                    TargetStunned.SetActive(true);
                    StunTimeLeft.text = SelectedTarget.GetComponent<Myrmidon>().StunDuration.ToString("00");
                }
                else
                {
                    TargetStunned.SetActive(false);
                }

            }
            else if (SelectedTarget.tag == "Aquamancer")
            {
                TargetHealth.value = SelectedTarget.GetComponent<Aquamancer>().Health / SelectedTarget.GetComponent<Aquamancer>().MaxHealth;
                TargetMana.value = SelectedTarget.GetComponent<Aquamancer>().Mana / SelectedTarget.GetComponent<Aquamancer>().MaxMana;
                if (SelectedTarget.GetComponent<Aquamancer>().Stunned == true)
                {
                    TargetStunned.SetActive(true);
                    StunTimeLeft.text = SelectedTarget.GetComponent<Aquamancer>().StunDuration.ToString("00");
                }
                else
                {
                    TargetStunned.SetActive(false);
                }
            }

            //If player goes a certain distance from target then untarget them
            if (player.transform.position.x > SelectedTarget.transform.position.x + MaxRange || player.transform.position.x < SelectedTarget.transform.position.x - MaxRange || player.transform.position.y > SelectedTarget.transform.position.y + MaxRange || player.transform.position.y < SelectedTarget.transform.position.y - MaxRange)
            {
                //Debug.Log("out of range");
                SelectedTarget = null;
                ToggleTargetUi(false);
            }
            else
            {
                //Debug.Log("within range");
            }
        }
    }

    private void ToggleTargetUi(bool show)
    {
        TargetPicBorder.SetActive(show);
        TargetPic.SetActive(show);
        TargetHealth.gameObject.SetActive(show);
        TargetMana.gameObject.SetActive(show);
        TargetStunned.SetActive(show);
    }



}
