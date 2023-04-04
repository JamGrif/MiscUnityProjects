using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //enum BirdColourChoice {red,green,blue,white};
    
    
    //Text at top telling player what colour to look for
    public Text BirdColour;
    public Text Choice;

    //Used for counting players choice of birds and comparing
    internal int AmountOfChosenBirds = 0;
    private int PlayersGuess = 0;
    internal int ColourChoice;

    //Stops the answer being changed at the end of the game
    private bool DoFunctionOnce = true;
    private bool CantChangeAnswer = false;

    //Ending Text
    public Text WinOrLose;
    public Text CorrectBirds;
    public GameObject EndingText;

    void Start()
    {
        EndingText.SetActive(false);
        ColourChoice = Random.Range(0, 3);
        if (ColourChoice == 0) //red
        {
            BirdColour.text = "<color=red>Red</color>";
        }
        else if (ColourChoice == 1) //green
        {
            BirdColour.text = "<color=green>Green</color>";
        }
        else //blue
        {
            BirdColour.text = "<color=blue>Blue</color>";
        }

        GameObject.Find("BirdManager").GetComponent<BirdSpawner>().WaitingToSpawn = true;
        
    }

    private void Update()
    {
        //Debug.Log("There have been " + AmountOfChosenBirds + " birds with the same colour.");
        //Debug.Log(PlayersGuess);
        Choice.text = PlayersGuess.ToString("00");

        if (GameObject.Find("BirdManager").GetComponent<BirdSpawner>().FinishedSpawning && DoFunctionOnce)
        {
            DoFunctionOnce = false;
            StartCoroutine(End());
        }
    }

    //Increases players choice
    public void IncreaseGuess()
    {
        if (PlayersGuess < 99 && !CantChangeAnswer)
        {
            PlayersGuess++;
        }
    }

    //Decreases players choice
    public void DecreaseGuess()
    {
        if (PlayersGuess > 0 && !CantChangeAnswer)
        {
            PlayersGuess--;
        } 
    }

    //Runs at the end of the game to tell the player if they win or lose
    IEnumerator End()
    {
        yield return new WaitForSeconds(3);
        EndingText.SetActive(true);
        CantChangeAnswer = true;
        CorrectBirds.text = AmountOfChosenBirds.ToString("00");
        if (PlayersGuess == AmountOfChosenBirds)
        {
            //Debug.Log("Player wins!");
            WinOrLose.text = "win!";
        }
        else
        {
            //Debug.Log("Player loses");
            WinOrLose.text = "lose!";
        }

    }

}
