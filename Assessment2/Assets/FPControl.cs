using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPControl : MonoBehaviour
{
    public int RayRange;
    private bool doorfound;
    private bool destro;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorfound = false;
        RaycastHit Hit;
        if(Physics.Raycast(transform.position,transform.forward,out Hit,RayRange))
        {
            if(Hit.transform.name == "Door")
            {
                doorfound = true;
                Debug.Log("Hit!");
                GameObject.Find("LeText").GetComponent<Text>().text = "Press [E] to Break Down";
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Destroy(Hit.transform.gameObject);
                    GameObject.Find("LeText").GetComponent<Text>().text = "Door is now destroyed. Good job!";
                    destro = true;
                }
            }
        }

        if(!doorfound && !destro) GameObject.Find("LeText").GetComponent<Text>().text = "";

        //Debug.Log("Test");
    }
}
