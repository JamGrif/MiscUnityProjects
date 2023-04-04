using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDisableScript : MonoBehaviour {

    public GameObject TheSphere;
    public GameObject TheLightSphere;

    private void Start()
    {
        Debug.Log("LMB will Toggle the Sphere\nRMB will Toggle the Light Component of Light Sphere");
    }

    void Update ()
    {
		if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            TheSphere.SetActive(!TheSphere.activeInHierarchy);
        }

        if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            TheLightSphere.GetComponent<Light>().enabled = !TheLightSphere.GetComponent<Light>().enabled;
        }
	}
}
