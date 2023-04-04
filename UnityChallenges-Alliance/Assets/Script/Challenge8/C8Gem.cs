using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class C8Gem : MonoBehaviour
{
    public GameObject GemParticles;
    public GameObject GemSound;

    //public AudioClip GemCollected;
    //public AudioSource SoundSource;

    private Vector2 GemPosition;
    private void Start()
    {
        //SoundSource.clip = GemCollected;
        GemPosition = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameObject gemsound = Instantiate(GemSound, transform.parent);
            GameObject gem = Instantiate(GemParticles, transform.parent);
            gem.transform.localPosition = GemPosition;
            Destroy(gemsound, 1);
            Destroy(gem, 1);
            this.gameObject.SetActive(false);
        }
    }

}
