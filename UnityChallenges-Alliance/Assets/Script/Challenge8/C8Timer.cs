using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class C8Timer : MonoBehaviour
{
    private float Minutes = 01;
    private float Seconds = 10;
   
    public Text MinuteText;
    public Text SecondText;
  
    void Update()
    {
        if (Seconds <= 0 && Minutes == 1)
        {
            Minutes = Minutes - 1;
            Seconds = Seconds + 59;
        }

        Seconds -= 1 * Time.deltaTime;

        MinuteText.text = Minutes.ToString("00");
        SecondText.text = Seconds.ToString("00");

        if (Seconds <= 0 && Minutes <= 0)
        {
            SceneManager.LoadScene("Challenge_8");
        }
    }
}
