using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{

    private bool IsShaking = false;
    private bool ShakedOnce;
    
    private float Shake = 0.05f;

    private float ShakeInterval= 0.05f;
    private float CurShakeInterval;

    private Vector3 StartingPos;

    private void Awake()
    {
        StartingPos = transform.position;
    }

    private void Update()
    {
        #region Shaking Logic
        /// This is causing the rock to shake every "ShakeInterval", currently a value set within the script.
        /// You may expose these values to the inspector if you wish to see what they do, however this won't have any effect on your mark.
        if (IsShaking)
        {
            CurShakeInterval += Time.deltaTime;
            if (CurShakeInterval > ShakeInterval)
            {
                // =========================================================================================
                // !!! Fix this script so the boulder shakes for 1 sec and afterwards starts dropping !!!
                // =========================================================================================

                if (!ShakedOnce)
                {
                    transform.Translate(new Vector3(Random.Range(-Shake, Shake), Random.Range(-Shake, Shake), 0));
                    ShakedOnce = true;
                }
                else
                {
                    ShakedOnce = false;
                    transform.position = StartingPos;
                }

            }
        }
        #endregion
    }

    public void ToggleShake()
    {
        if (IsShaking == true)
        {
            IsShaking = false;
        }
        else
        {
            IsShaking = true;
        }
        
    }

}
