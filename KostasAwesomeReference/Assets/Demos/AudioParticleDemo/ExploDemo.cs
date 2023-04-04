using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploDemo : MonoBehaviour {


    public GameObject Explo;
    private GameObject SpawnGO;
    
    void Start()
    {
        Debug.Log("LMB to Create an Explosive effect with audio.");
    }

	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if(!SpawnGO)
            {
                SpawnGO = Instantiate(Explo, Vector3.zero, Quaternion.identity);

                SpawnGO.GetComponent<ParticleSystem>().Play();
                SpawnGO.GetComponent<AudioSource>().Play();
            }
        }

        if(SpawnGO)
        {
            if(!SpawnGO.GetComponent<ParticleSystem>().isPlaying && !SpawnGO.GetComponent<AudioSource>().isPlaying)
            {
                Destroy(SpawnGO);
            }
        }
	}
}
