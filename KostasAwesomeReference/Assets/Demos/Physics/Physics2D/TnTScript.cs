using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TnTScript : MonoBehaviour {

    public float ExplosivePower;
    public float ExplosiveRadius;
    public GameObject Explosion;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var Objects = GameObject.FindObjectsOfType<Rigidbody2D>();
        foreach (Rigidbody2D item in Objects)
        {
            if(Vector2.Distance(item.gameObject.transform.position,transform.gameObject.transform.position) < ExplosivePower)
            {
                Debug.Log(item.gameObject.name);
                item.gameObject.GetComponent<Rigidbody2D>().AddForce((item.gameObject.transform.position - transform.position) * ExplosivePower, ForceMode2D.Impulse);
            }
        }

        Explosion.SetActive(true);
        Explosion.GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);

    }
}
