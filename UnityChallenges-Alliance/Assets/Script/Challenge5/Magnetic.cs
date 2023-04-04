using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magnetic : MonoBehaviour
{
    public Sprite Awake;
    public Sprite Sleep;

    public Transform Player;

    public GameObject ZZZParticles;
    public GameObject PullingParticles;
    public GameObject PushingParticles;

    private bool IsAsleep;
    private bool StartCountDown;

    void Start()
    {
        this.GetComponent<PointEffector2D>().enabled = false;
        this.gameObject.GetComponent<SpriteRenderer>().sprite = Sleep;
        ZZZParticles.SetActive(true);
        PullingParticles.SetActive(false);
        PushingParticles.SetActive(false);

        IsAsleep = true;
        StartCountDown = true;
    }

    void Update()
    {
        if (StartCountDown == true)
        {
            StartCoroutine(ToggleState());
        }
    }

    IEnumerator ToggleState()
    {
        StartCountDown = false;
        yield return new WaitForSeconds(3);

        if (IsAsleep == true) //sleeping
        {
            PullingParticles.SetActive(false);
            PushingParticles.SetActive(false);
            ZZZParticles.SetActive(true);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Sleep;
            this.GetComponent<PointEffector2D>().enabled = false;
            //this.GetComponent<ParticleSystem>().enableEmission = true;
           
            IsAsleep = false;
        }

        else //awake    
        {
            //Rotate so facing players direction
            if (transform.position.x < Player.transform.position.x) { this.transform.eulerAngles = new Vector3(0, 180, 0); }
            else { this.transform.eulerAngles = new Vector3(0, 360, 0); }

            ZZZParticles.SetActive(false);
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Awake;
            this.GetComponent<PointEffector2D>().enabled = true;
           
            //50/50 chance to pull or push everything
            int RandomNumber = Random.Range(1, 3);
            if (RandomNumber == 1) //Pulling
            {
                this.GetComponent<PointEffector2D>().forceMagnitude=-50;
                PullingParticles.SetActive(true);
                //Particles to say pulling
            }

            else if (RandomNumber == 2) //Pushing
            {
                this.GetComponent<PointEffector2D>().forceMagnitude = 100;
                PushingParticles.SetActive(true);
                //Particles to say pushing               
            }
            
            
         
            IsAsleep = true;
        }
        StartCountDown = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene("Challenge_5");
        }
    }

}
