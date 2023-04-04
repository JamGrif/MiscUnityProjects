using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rouge : MonoBehaviour
{
    private GameObject PressurePlate;
    public GameObject Dagger;

    private void OnEnable()
    {
        PressurePlate = GameObject.Find("Pressure_Plate");
        if (PressurePlate.gameObject.GetComponent<PressurePlate>().IsRougeAwoken == true)
        {
            Instantiate(Dagger, new Vector2(-5.81f,-3.47f), Quaternion.Euler(new Vector3(0,0,270)));
        }
    }


}
