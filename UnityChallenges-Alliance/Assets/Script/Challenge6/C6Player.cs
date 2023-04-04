using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C6Player : MonoBehaviour
{
    private bool IsLeverPressed = false;
    public GameObject CloudParticle;

    private void SpawnCloud()
    {
        GameObject cloud = Instantiate(CloudParticle, transform);
        cloud.transform.localPosition = new Vector2(0f, -0.75f);
        Destroy(cloud, 0.5f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Lever" && Input.GetKeyDown(KeyCode.E) && IsLeverPressed == false) //Pulling the lever
        {
            IsLeverPressed = true;
            //Play animations on lever and door
            GameObject.Find("Lever_Handle").GetComponent<Animation>().Play("Lever_Opening");
            GameObject.Find("Openable Door").GetComponent<Animation>().Play("Door_Opening");
        }
    }

    



}
