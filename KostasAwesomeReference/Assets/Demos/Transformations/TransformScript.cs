using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformScript : MonoBehaviour {


    public float RotateSpeed;
    public float TranslateSpeed;
    public Space LeSpace;
    
	void Update ()
    {
        float RotSpeed = RotateSpeed * Time.deltaTime;
        float TranSpeed = TranslateSpeed * Time.deltaTime;

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            LeSpace = (LeSpace == Space.World) ? Space.Self : Space.World;
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Rotate(RotSpeed, 0, 0, LeSpace);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Rotate(-RotSpeed, 0, 0, LeSpace);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(0,RotSpeed, 0,LeSpace);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0,-RotSpeed, 0, LeSpace);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(0, TranSpeed, 0, LeSpace);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(0, -TranSpeed, 0, LeSpace);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-TranSpeed, 0, 0, LeSpace);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(TranSpeed, 0, 0, LeSpace);
        }
    }
}
