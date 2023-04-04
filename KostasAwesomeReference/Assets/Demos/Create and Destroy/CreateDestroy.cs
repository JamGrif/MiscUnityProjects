using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDestroy : MonoBehaviour
{

    public GameObject Prefab;
    private GameObject CreatedObj;
	
	void Start()
    {
        Debug.Log("Press LMB to Create an Object");
        Debug.Log("Press LMB again to Destroy that Object");
    }
	
	void Update ()
    {
		if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            if(CreatedObj)
            {
                Destroy(CreatedObj);
            }
            else
            {
                CreatedObj = Instantiate(Prefab, Vector3.zero, Quaternion.identity);
            }
        }
	}
}
