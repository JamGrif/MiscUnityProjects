using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject Bird;

    private int BirdWaitTime = 10;
    private int BirdSpawningTime = 30;

    //Used before the birds have started spawning
    internal bool WaitingToSpawn = false;

    //Used when birds are spawning
    private bool NeedToSpawnBird = false;
    internal bool SpawningBirds = false;

    //Used when birds have finished spawning
    internal bool FinishedSpawning = false;

    void Update()
    {
        if (WaitingToSpawn) //Wait 10 seconds before spawning birds
        {
            //Debug.Log("Waiting to spawn birds");
            WaitingToSpawn = false;
            StartCoroutine(WaitToSpawn());
        }

        if (SpawningBirds) //Spawn birds for 30 seconds
        {
            if (NeedToSpawnBird) //Bird spawns every 0.5 to 1.8 seconds
            {
                StartCoroutine(SpawnBird());
            }
        }
    }

    //10 seconds before the birds start spawning
    IEnumerator WaitToSpawn()
    {
        yield return new WaitForSeconds(BirdWaitTime);
        NeedToSpawnBird = true;
        SpawningBirds = true;
        StartCoroutine(StopSpawning()); //Stops spawning after 30 seconds
    }

    //30 seconds during the birds are spawning
    IEnumerator SpawnBird()
    {
        NeedToSpawnBird = false;
        float spawn = Random.Range(0.5f, 1.8f);
        yield return new WaitForSeconds(spawn);
        if (SpawningBirds)
        {
            GameObject SpawnedBird = Instantiate(Bird, transform.parent);
            Destroy(SpawnedBird, 10f);
            //Debug.Log("Spawned a bird");
            NeedToSpawnBird = true;
        }
    }

    //Birds have finished spawning
    IEnumerator StopSpawning()
    {
        yield return new WaitForSeconds(BirdSpawningTime);
        SpawningBirds = false;
        FinishedSpawning = true;
        //Debug.Log("Stop spawning birds");
    }

    //Deletes birds when they leave the screen
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bird")
        {
            Destroy(collision.gameObject);
        }
    }

}
