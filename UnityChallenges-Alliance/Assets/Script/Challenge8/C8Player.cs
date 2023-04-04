using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class C8Player : MonoBehaviour
{
    public Text GemsLeft;
    private int Gems = 3;

    private bool CheckingForPickup;
    private bool ToggleSnapShot = true;

    public AudioMixerSnapshot MusicLouder;
    public AudioMixerSnapshot AmbientLouder;
    
    void Start()
    {
        GemsLeft.text = "3";
        CheckingForPickup = true;
    }

    
    void Update()
    {
        if (CheckingForPickup == true)
        {
            StartCoroutine(CheckForGemPickup());
            CheckingForPickup = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WallParent")
        {
            Debug.Log("Touched wall!");
            
           
            if (ToggleSnapShot == true)
            {
                ToggleSnapShot = false;
                MusicLouder.TransitionTo(2f);
            }
            else
            {
                ToggleSnapShot = true;
                AmbientLouder.TransitionTo(2f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gem")
        {
            StartCoroutine(UpdateGemAmount());
            GameObject.Find("GemStuff").GetComponent<Animator>().SetBool("MoveOut", false);
            CheckingForPickup = true;
        }
    }

    IEnumerator CheckForGemPickup()
    {
        yield return new WaitForSeconds(2);
        if (CheckingForPickup == false)
        {
            GameObject.Find("GemStuff").GetComponent<Animator>().SetBool("MoveOut", true);
        }
    }

    IEnumerator UpdateGemAmount()
    {
        yield return new WaitForSeconds(1);
        Gems--;
        GemsLeft.text = Gems.ToString("0");
    }

    void FootStep()
    {
        //Debug.Log("Footstep!");
    }

}
