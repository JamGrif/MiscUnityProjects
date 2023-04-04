using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathDemo : MonoBehaviour {


    public float ValueA;
    public float ValueB;
    public float ValueC;

    public float PingPongSpeed = 1;

    // animate the game object from -1 to +1 and back
    public float minimum = -1.0F;
    public float maximum = 1.0F;
    bool bIsMovingRight = true;

    // starting value for the Lerp
    static float interp = 0.0f;

    private GameObject LerpObj;

    List<Light> LightComps = new List<Light>(); 
	
	void Start ()
    {
        print("Mathf Clamp for Value " + ValueA + " between Inclusives [" + ValueB + "," + ValueC + "] = " + Mathf.Clamp(ValueA,ValueB,ValueC));

        print("Absolute values are always positive! So Mathf.Abs(-" + ValueA + ") and Mathf.Abs(" + ValueA + ") return " + Mathf.Abs(-ValueA) + " and " + Mathf.Abs(ValueA) + " respectively!");

        print("Mathf.Floor rounds the value downwards, while Mathf.Ceil rounds the value upwards.\nMathf.Round does rounding as we know it.");
        print("Mathf.Floor(17.4) returns " + Mathf.Floor(17.4f) + " and Mathf.Ceil(17.4) returns " + Mathf.Ceil(17.4f)+".\nMathf.Round(17.4) returns " + Mathf.Round(17.4f) + " because it's closer to 17 rather than 18.");

        var items = GameObject.FindObjectsOfType<Light>();
        foreach (Light light in items)
        {
            LightComps.Add(light);
        }
        
        LerpObj = GameObject.Find("Lerper");

    }
	
	
	void Update ()
    {
        // Pingponging
        float T = Mathf.PingPong(Time.time * PingPongSpeed, ValueA);
        foreach (var item in LightComps)
        {
            item.intensity = T;
        }
        
        // Lerping for X and using Trigonometry for Y
        LerpObj.transform.position = new Vector3(Mathf.Lerp(minimum, maximum, interp), Mathf.Sin(Time.time * 2), -4.34f);

        //Increasing the interpolator (Important for Lerp!)
        interp += 0.25f * Time.deltaTime;

        if(interp>1)
        {
            Swap(ref maximum, ref minimum);

            interp = 0;
        }

    }

    private void Swap(ref float X, ref float Y)
    {
        // Swapping 2 Variables without a temp. LEARN THIS BY HEART!
        X = X + Y;
        Y = X - Y;
        X = X - Y;
    }
}
