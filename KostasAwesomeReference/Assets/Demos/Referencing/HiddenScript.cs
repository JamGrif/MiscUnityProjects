using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenScript : MonoBehaviour {

    private int hiddenValue = 10;

    internal int HiddenValue
    {
        get
        {
            return hiddenValue;
        }
    }

    internal void SetHidden(int Val)
    {
        hiddenValue = Val;
    }
}
