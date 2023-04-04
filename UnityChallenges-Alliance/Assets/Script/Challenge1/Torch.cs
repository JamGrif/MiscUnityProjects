using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Torch : MonoBehaviour
{
    public Sprite unlit;
    public Sprite lit;
    

    int BlueAmount = 0;
    int BlackAmount = 0;
    public GameObject[] BlueBlock;
    public GameObject[] BlackBlock;

    void Start()
    {
        BlueBlock = GameObject.FindGameObjectsWithTag("BlueBlock");
        BlackBlock = GameObject.FindGameObjectsWithTag("BlackBlock");
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = lit;
            //TorchLit = true;

            foreach (GameObject BlueBlock in BlueBlock)
            {
                BlueAmount++;
                BlueBlock.AddComponent<Rigidbody2D>();
            }
            foreach (GameObject BlueBlock in BlackBlock)
            {
                BlackAmount++;
            }

            StartCoroutine(BlockAmount());
            StartCoroutine(NextScene());
            
        }
    }


    IEnumerator BlockAmount()
    {
        yield return new WaitForSeconds(1);
        Debug.Log("There are " + BlueAmount + " blue tiles and " + BlackAmount + " black tiles!");

    }

    IEnumerator NextScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Challenge_2");
    }

}
