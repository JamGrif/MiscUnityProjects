using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WyvernSpawn : MonoBehaviour
{
    private int CurrentWave = 0;
    private int WaveOneAmount = 2;
    private int WaveTwoAmount = 5;
    private int WaveThreeAmount = 8;

    internal int AliveWyvern = 0;

    //private bool NextWave = true;
    private bool NeedMoreWyverns = true;

    public GameObject WyvernPrefab;
    private GameObject wyvern;

    public GameObject YouWin;
    private bool DoOnce = false;

    void Start()
    {
        //Start the first wave of wyverns
        CurrentWave = 1;
        YouWin.SetActive(false);
    }

    
    void Update()
    {
        if (CurrentWave == 1)
        {
            //Debug.Log("Currentwave is 1");
            if (NeedMoreWyverns)
            {
                SpawnWyvern(WaveOneAmount);
            }
            CheckWyvern();
            

        }
        else if (CurrentWave == 2)
        {
            //Debug.Log("Currentwave is 2");
            if (NeedMoreWyverns)
            {
                SpawnWyvern(WaveTwoAmount);
            }
            CheckWyvern();

        }
        else if (CurrentWave == 3)
        {
            //Debug.Log("Currentwave is 3");
            if (NeedMoreWyverns)
            {
                SpawnWyvern(WaveThreeAmount);
            }
            CheckWyvern();

        }
        else if (CurrentWave == 4 && !DoOnce) //Game finished
        {
            DoOnce = true;
            //Debug.Log("Currentwave is 4");
            YouWin.SetActive(false);
        }



    }

    void SpawnWyvern(int SpawnableWyverns)
    {
        NeedMoreWyverns = false;
        AliveWyvern = 0;
        for (int i = AliveWyvern; i < SpawnableWyverns; i++)
        {
            //Debug.Log("wyvern spawned");
            AliveWyvern++;
            //Spawn wyverns here
            wyvern = Instantiate(WyvernPrefab, transform.parent);
        }
    }

    void CheckWyvern()
    {
        if (AliveWyvern == 0) //All Wyverns are dead so next round
        {
            NeedMoreWyverns = true;
            CurrentWave++;

        }
    }

    

}
