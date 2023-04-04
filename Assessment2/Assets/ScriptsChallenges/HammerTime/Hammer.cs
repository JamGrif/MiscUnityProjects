using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    private int HammerLifetime = 2;
    internal bool HammerActive = false;

    
    void Start()
    {
        HammerActive = true;
        StartCoroutine(DestroyHammerAuto());
    }

    
    void Update()
    {
        transform.Rotate(0, 0, 15 );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wyvern")
        {
            DestroyHammer();
        }
    }



    IEnumerator DestroyHammerAuto()
    {
        yield return new WaitForSeconds(HammerLifetime);
        DestroyHammer();
    }


    void DestroyHammer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<HTPlayer>().HoldingHammer = true;
        Object.Destroy(gameObject);
    }


}
