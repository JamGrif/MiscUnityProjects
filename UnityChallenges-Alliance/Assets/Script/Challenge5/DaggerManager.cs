using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerManager : MonoBehaviour
{
    public GameObject Dagger;
    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            Vector2 position = new Vector2(Random.Range(7.5f, -7.5f), Random.Range(3.5f, -0.5f));
            Instantiate(Dagger, position, Quaternion.identity);
        }
        
    }

    
    void Update()
    {
        
    }
}
