using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster3DScript : MonoBehaviour {

    public float Range;
    public float Score;
    public enum MyRaycastType
    {
        Single,
        Multi
    }

    public MyRaycastType RayType;

	// Update is called once per frame
	void Update ()
    {
        RaycastHit Hit;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            switch (RayType)
            {
                case MyRaycastType.Single:

                    if (Physics.Raycast(transform.position, transform.forward, out Hit, Range))
                    {
                        if (Hit.transform.name.CompareTo("Target") == 0)
                        {

                            Score += ((1 / Vector3.Distance(Hit.point, Hit.transform.position)) * 10) * Vector3.Distance(transform.position, Hit.transform.position);

                            Hit.transform.gameObject.SetActive(false);
                        }
                    }

                    break;
                case MyRaycastType.Multi:

                    var Hits = Physics.RaycastAll(transform.position, transform.forward, Range);
                    if (Hits.Length > 0)
                    {
                        foreach (var item in Hits)
                        {
                            if (string.Equals(item.transform.name, "Target"))
                            {

                                Score += ((1/Vector3.Distance(item.point, item.transform.position)) * 10) * Vector3.Distance(transform.position, item.transform.position);
                                

                                item.transform.gameObject.SetActive(false);
                            }
                        }
                    }

                    break;
                default:
                    break;
            }
            

            
        }
	}
}
