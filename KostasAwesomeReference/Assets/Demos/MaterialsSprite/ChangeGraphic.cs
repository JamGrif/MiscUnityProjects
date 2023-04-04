using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGraphic : MonoBehaviour {
    
    private MeshRenderer Our3DRenderer;
    private SpriteRenderer Our2DRenderer;

    public Sprite Sp1;
    public Sprite Sp2;

    public Material Mat1;
    public Material Mat2;

    // Use this for initialization
    void Start ()
    {
        Our3DRenderer = GameObject.Find("LeMesh").GetComponent<MeshRenderer>();
        Our2DRenderer = GameObject.Find("LeSprite").GetComponent<SpriteRenderer>();

        Debug.Log("Press LMB to change the Sprite and RMB to change Material to Cube.");
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Our2DRenderer.sprite = (Our2DRenderer.sprite == Sp1) ? Sp2 : Sp1;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Our3DRenderer.sharedMaterial = (Our3DRenderer.sharedMaterial == Mat1) ? Mat2 : Mat1;
        }

        if (FlippingCondition())
        {
            Vector3 Vec = Our2DRenderer.gameObject.transform.localScale;
            Our2DRenderer.gameObject.transform.localScale = new Vector3(-Vec.x, Vec.y, Vec.z);
        }
    }

    private bool FlippingCondition()
    {
        return (Input.GetAxis("Horizontal") > 0) && (Our2DRenderer.gameObject.transform.localScale.x < 0) ||
               (Input.GetAxis("Horizontal") < 0) && (Our2DRenderer.gameObject.transform.localScale.x > 0);
    }
}
