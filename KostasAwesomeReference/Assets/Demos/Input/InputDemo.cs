using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDemo : MonoBehaviour {


    public float HorizSpeed;
    public float VertSpeed;
	
	void Start ()
    {
		
	}
	
	
	void Update ()
    {

        if (Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("PRESSED Z");
        }
        if (Input.GetKey(KeyCode.Z))
        {
            Debug.Log("IS PRESSING Z");
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Debug.Log("RELEASED Z");
        }

        // CUSTOM INPUT
        // EDIT --> PROJECT SETTINGS --> INPUT
        if (Input.GetButtonDown("SolentFire"))
        {
            Debug.Log("PRESSED SOLENT FIRE");
        }
        if (Input.GetButton("SolentFire"))
        {
            Debug.Log("IS PRESSING SOLENT FIRE");
        }
        if (Input.GetButtonUp("SolentFire"))
        {
            Debug.Log("RELEASED SOLENT FIRE");
        }


        // CHECK HORIZ AND VERT IN SETTINGS
        transform.Translate(new Vector3(Input.GetAxis("Horizontal") * HorizSpeed * Time.deltaTime, 
                                        Input.GetAxis("Vertical") * VertSpeed * Time.deltaTime, 0));
        

    }
}
