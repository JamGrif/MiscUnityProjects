using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAround : MonoBehaviour {

    public float RotSpeed;

    public enum EAxis
    {
        X,
        Y,
        Z
    }

    public EAxis MyAxis;

	void Update ()
    {
        switch (MyAxis)
        {
            case EAxis.X:
                transform.Rotate(RotSpeed * Time.deltaTime, 0, 0, Space.World);
                break;
            case EAxis.Y:
                transform.Rotate(0,RotSpeed * Time.deltaTime, 0, Space.World);
                break;
            case EAxis.Z:
                transform.Rotate(0,0,RotSpeed * Time.deltaTime, Space.World);
                break;
            default:
                break;
        }
    }
}
