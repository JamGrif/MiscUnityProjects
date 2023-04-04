using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Techniques : MonoBehaviour {


    private void Awake()
    {
        SetFPS();
        SetTimeScale();
    }

    private void SetTimeScale()
    {
        Time.timeScale = 200; // x200 Speed;

        Time.timeScale = 1; // Normal Speed;

        Time.timeScale = 0.4f; // Slo-mo

        Time.timeScale = 0; // freeze;
    }
    

    void SetFPS()
    {
        // Mobile
        Application.targetFrameRate = 30;

        // PC
        Application.targetFrameRate = 60;
    }
}
