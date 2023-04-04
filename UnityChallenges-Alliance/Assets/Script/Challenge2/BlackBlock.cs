using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBlock : MonoBehaviour
{
    public GameObject Particles;

    

    void OnMouseDown()
    {
        //add particle system
        Particles = Instantiate(Particles, this.gameObject.transform);
        Particles.transform.parent = null;
        Destroy(gameObject);
    }




}
