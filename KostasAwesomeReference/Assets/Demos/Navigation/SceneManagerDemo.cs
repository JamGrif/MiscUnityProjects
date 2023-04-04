using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// You need this
using UnityEngine.SceneManagement;

public class SceneManagerDemo : MonoBehaviour {

    // Name of new scene. Don't forget to add to build settings!
    public string scene;
    
    private void Start()
    {
        Debug.Log("Loaded Scene " + SceneManager.GetActiveScene().name);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            LoadScene(scene);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            ReloadScene();
        }
    }

    // Load the new scene (String Overload)
    public void LoadScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
    }

    // Load the new scene (Int Overload)
    public void LoadScene(int Index)
    {
        SceneManager.LoadScene(Index);
    }

    // Reload the current scene
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
