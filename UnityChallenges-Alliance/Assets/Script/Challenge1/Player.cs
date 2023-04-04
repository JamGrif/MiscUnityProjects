using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    
    bool BatAttached = false;
    public GameObject Bat;

    void Start()
    {
        Bat = GameObject.FindGameObjectWithTag("Bat");
    }

    
    void Update()
    {
        //PlayerX = transform.position.x;
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (BatAttached == false)
            {
                BatAttached = true;
                float BatY = Bat.transform.position.y;
                Bat.transform.parent = this.transform;
                Bat.transform.localPosition = new Vector2(0,Bat.transform.localPosition.y);
                
            }
            else if(BatAttached == true)
            {
                BatAttached = false;
                Bat.transform.parent = null;
            }
        }

    }

    



}
