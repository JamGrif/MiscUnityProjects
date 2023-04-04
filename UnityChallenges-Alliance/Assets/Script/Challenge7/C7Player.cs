using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class C7Player : MonoBehaviour
{
    public Text GemsLeft;
    private int Gems = 3;

    private bool CheckingForPickup;
    
    void Start()
    {
        GemsLeft.text = "3";
        CheckingForPickup = true;
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (CheckingForPickup == true)
        {
            StartCoroutine(CheckForGemPickup());
            CheckingForPickup = false;
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

}
