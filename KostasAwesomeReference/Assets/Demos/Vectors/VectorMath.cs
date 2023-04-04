using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorMath : MonoBehaviour {

    private GameObject Red;
    private GameObject Green;
    private GameObject Blue;

    public float Speed;

    public enum VectorDemoType
    {
        Distance,
        FindToward,
        Normalize,
        ReflectionAndAngle,
        Angle,
        Dot
    }

    public VectorDemoType CurrentDemo;

	void Start ()
    {

        Red = GameObject.Find("R");
        Green = GameObject.Find("G");
        Blue = GameObject.Find("B");

        switch (CurrentDemo)
        {
            case VectorDemoType.Distance:
                DistanceDemo();
                break;
            case VectorDemoType.FindToward:
                TowardDemo();
                break;
            case VectorDemoType.Dot:
                DotDemo();
                break;
            default:
                break;
        }
    }

    private void TowardDemo()
    {
        Debug.Log("Green will go towards Red");

        // VECTOR TO DESTINATION FROM ORIGIN = DESTINATION - ORIGIN
        Vector3 GreenToRed = Red.transform.position - Green.transform.position;
        Green.AddComponent<Rigidbody>();
        Green.GetComponent<Rigidbody>().useGravity = false;
        Green.GetComponent<Rigidbody>().velocity = GreenToRed;
    }

    private void DistanceDemo()
    {
        float Dist = Vector3.Distance(Red.transform.position, Green.transform.position);
        
        Debug.Log("The Distance between Red and Green is: " + Dist);
    }

    private void DotDemo()
    {
        Vector3 forward = Green.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = Blue.transform.position - Green.transform.position;
        if (Vector3.Dot(forward, toOther) < 0)
            print("Blue is behind Green! Backstab confirmed!");
        else
            print("Blue is not behind Green. Blue is a goner!");

    }

    void Update ()
    {
        switch (CurrentDemo)
        {
           
            case VectorDemoType.Normalize:
                NormalizeDemo();
                break;
            case VectorDemoType.ReflectionAndAngle:
                ReflectDemo();
                break;
            default:
                break;
        }
    }

    private void ReflectDemo()
    {
        GameObject Spot1 = GameObject.Find("Spotlight");
        GameObject Spot2 = GameObject.Find("OtherSpotlight");
        Spot2.transform.rotation = Quaternion.LookRotation(Vector3.Reflect(Spot1.transform.forward, Vector3.right));

        Debug.Log("Angle Between Spotlights: " + Vector3.Angle(Spot1.transform.forward, -Spot2.transform.forward));

    }

    private void NormalizeDemo()
    {
        float Horiz = Input.GetAxis("Horizontal");
        float Vert = Input.GetAxis("Vertical");

        Vector3 Direction = new Vector3(Horiz, Vert, 0);

        Debug.Log("Length of Direction: " + Direction.magnitude + "\n" + "Length of Direction (Normalized): " + Direction.normalized.magnitude);

        if (Mathf.Abs(Horiz) > 0 || Mathf.Abs(Vert) > 0)
        {
            Red.transform.Translate(Direction * Time.deltaTime * Speed);
            Green.transform.Translate(Vector3.Normalize(Direction) * Time.deltaTime * Speed);
        }
    }
}
