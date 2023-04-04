using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public GameObject GemParticles;

    private Vector2 GemPosition;
    private void Start()
    {
        GemPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gem = Instantiate(GemParticles, transform.parent);
            gem.transform.localPosition = GemPosition;
            Destroy(gem, 1);
            this.gameObject.SetActive(false);
        }
    }

}
