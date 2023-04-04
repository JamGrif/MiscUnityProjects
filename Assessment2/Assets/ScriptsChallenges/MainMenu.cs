using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void BirdWatching()
    {
        SceneManager.LoadScene("BirdWatching");
    }

    public void HammerTime()
    {
        SceneManager.LoadScene("HammerTime");
    }

    public void Optimization()
    {
        SceneManager.LoadScene("UnoptimizedScene");
    }



}
