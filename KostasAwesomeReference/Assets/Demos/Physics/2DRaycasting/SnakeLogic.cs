using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeLogic : MonoBehaviour {

    Rigidbody2D MyRigid;
    GameObject MyRayStart;
    SpriteRenderer My2DRenderer;

    public float Range;

    public float SnakeSpeed;

    public bool Spotted;

    public float TurnInterval;
    private float CurTurnInterval;

    private void Start()
    {
        MyRayStart = transform.GetChild(0).gameObject;

        My2DRenderer = GetComponent<SpriteRenderer>();
        MyRigid = GetComponent<Rigidbody2D>();
        MyRigid.velocity = new Vector2(-SnakeSpeed, 0);
    }

    void Update()
    {
        Raycasting();


        CurTurnInterval += Time.deltaTime;
        if (CurTurnInterval>TurnInterval)
        {
            CurTurnInterval = 0;


            MyRigid.velocity *= -1;
            Vector3 Vec = My2DRenderer.gameObject.transform.localScale;
            My2DRenderer.gameObject.transform.localScale = new Vector3(-Vec.x, Vec.y, Vec.z);
        }
    }

    private void Raycasting()
    {
        Vector3 EndPosition = MyRayStart.transform.position + new Vector3(-Range, 0, 0) * transform.localScale.x;
        Debug.DrawLine(MyRayStart.transform.position,EndPosition, Color.green);
        Spotted = Physics2D.Linecast(MyRayStart.transform.position, EndPosition, 1 << LayerMask.NameToLayer("Player"));

        if (Spotted) Debug.Log("I SEE U!");

    }
    

}
    

    
