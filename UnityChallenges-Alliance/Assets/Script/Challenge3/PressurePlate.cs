using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{

    public GameObject EarthElem;
    public bool IsRougeAwoken = false;
    void Start()
    {
        EarthElem = GameObject.Find("EarthElem");
        EarthElem.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D Enter2D)
    {
        if (Enter2D.gameObject.tag == "Player")
        {
            IsRougeAwoken = true;
            EarthElem.SetActive(true);
        }
    }


}
