using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonobehaviorDemo : MonoBehaviour {


    void Awake()
    {
        Debug.Log("AWAKE");
    }

    void Start ()
    {
        Debug.Log("START");
    }


    void FixedUpdate()
    {
        Debug.Log("FIXED");
    }

    void Update ()
    {
        Debug.Log("UPDATE");
    }

    void LateUpdate()
    {
        Debug.Log("LATE UPDATE");
    }

    private void OnDestroy()
    {
        Debug.Log("DESTROY");
    }
}
