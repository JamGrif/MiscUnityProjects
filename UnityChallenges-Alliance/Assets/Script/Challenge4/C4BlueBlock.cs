using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class C4BlueBlock : MonoBehaviour
{
    public GameObject RayStart;
 
    private bool IsFalling;

    private float Range = 4.5f;

    private bool InRange;


    void Start()
    {
        RayStart = transform.GetChild(0).gameObject;
        IsFalling = false;
        InRange = false;
    }

    void Update()
    {
        RayCasting();

    }

    private void RayCasting()
    {
        Vector3 EndPosition = RayStart.transform.position + new Vector3(0, -Range, 0) * transform.localScale.x;
        Debug.DrawLine(RayStart.transform.position, EndPosition, Color.green);
        InRange = Physics2D.Linecast(RayStart.transform.position, EndPosition, 1 << LayerMask.NameToLayer("Player"));

        if (InRange)
        {
            if (IsFalling == false)
            {
                IsFalling = true;
                this.gameObject.GetComponent<FallingRock>().ToggleShake();
                StartCoroutine(Falling());
            }
            
        }
    }

    IEnumerator Falling()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.GetComponent<FallingRock>().ToggleShake();
        this.gameObject.AddComponent<Rigidbody2D>();
        
    }

}
