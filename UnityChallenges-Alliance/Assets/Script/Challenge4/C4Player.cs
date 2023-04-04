using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class C4Player : MonoBehaviour
{
    public GameObject arrow;

    public GameObject EquippedArrow;
    private GameObject FiredArrow;

    public GameObject RayStart;
    public RaycastHit2D hit;

    private Vector3 RayHitPosition;

    private float Range = 5f;
    private bool InRange = false;

    private bool ArrowEquipped;
    void Start()
    {
        ArrowEquipped = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && !arrow)
        {
            if (ArrowEquipped == true && RayCasting() == true)
            {
                ArrowEquipped = false;
                FiredArrow = Instantiate(EquippedArrow, RayHitPosition, Quaternion.Euler(new Vector3(0,0,0)));
                Debug.Log("Arrow fired!");
                
            }
            else if (ArrowEquipped == false)
            {
                Debug.Log("Picked up arrow");
                ArrowEquipped = true;
                Object.Destroy(FiredArrow);
                
            }
        }        

    }

    private bool RayCasting()
    {
        Vector3 EndPosition = RayStart.transform.position + new Vector3(Range, 0, 0) * transform.localScale.x;
        Debug.DrawLine(RayStart.transform.position, EndPosition, Color.green);
        InRange = Physics2D.Linecast(RayStart.transform.position, EndPosition, 1 << LayerMask.NameToLayer("WallBlock"));
        hit = Physics2D.Linecast(RayStart.transform.position, EndPosition, 1 << LayerMask.NameToLayer("WallBlock"));
        RayHitPosition = hit.point;
        

        if (InRange)
        {
            return true;
        }
        return false;
    }
    
    private void OnCollisionEnter2D(Collision2D CEnter)
    {
        if (CEnter.gameObject.tag == "BlueBlock")
        {
            SceneManager.LoadScene("Challenge_4");
        }

        if (CEnter.gameObject.tag == "Arrow")
        {
            Object.Destroy(arrow);
            ArrowEquipped = true;
        }


    }



}
