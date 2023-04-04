using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefDemo : MonoBehaviour {


    static int Cubes = 0;
    static int Spheres = 0;
    static int Capsules = 0;

    public GameObject DraggedObject;
    public bool DetachThemAll;

    public enum DemoType
    {
        FindObjectsOfType,
        FindObjectsWithTag,
        Find,
        AddComponent,
        FindAndAdd,
        GetComponent,
        FindChild,
        AttachingParents
    }

    public DemoType MyDemo;

    void Start ()
    {

        switch (MyDemo)
        {
            case DemoType.FindObjectsOfType:
                FindObjectsOfTypeDemo();
                break;
            case DemoType.FindObjectsWithTag:
                FindObjectsWithTagDemo();
                break;
            case DemoType.Find:
                FindDemo();
                break;
            case DemoType.AddComponent:
                AddComponentDemo();
                break;
            case DemoType.FindAndAdd:
                FindAndAddDemo();
                break;
            case DemoType.GetComponent:
                GetComponentDemo();
                break;
            case DemoType.FindChild:
                FindChildDemo();
                break;
            case DemoType.AttachingParents:
                AttachingParentsDemo(DetachThemAll);
                break;
            default:
                break;
        }
    }

    private void AttachingParentsDemo(bool DetachAll = true)
    {
        GameObject ParentGO = GameObject.Find("ParentCube");

        if (DetachAll)
        {
            ParentGO.transform.DetachChildren();
            Debug.Log("Now the parent doesn't have children");
        }
        else
        {
            ParentGO.GetComponent<RotateAround>().RotSpeed *= 5;
            ParentGO.transform.GetChild(2).parent = null;
            ParentGO.transform.GetChild(0).parent = null;
            Debug.Log("Now the parent has lost 2 children");
            StartCoroutine(AttachAfter2Sec());
        }
    }

    private IEnumerator AttachAfter2Sec()
    {
        yield return new WaitForSeconds(2);
        GameObject ParentGO = GameObject.Find("ParentCube");
        DraggedObject.transform.parent = ParentGO.transform;
        Debug.Log("Now the parent has a new child!");

    }

    private void FindChildDemo()
    {
        GameObject ParentGO = GameObject.Find("ParentCube");

        Debug.Log(ParentGO.name + " has " + ParentGO.transform.childCount + " Children!");

        Transform Child_1 = ParentGO.transform.GetChild(0);
        Debug.Log("The first Child's name is " + Child_1.gameObject.name);

        Transform Child_2 = ParentGO.transform.Find("Sphere_Child_3");
        Debug.Log("The third Child has " + Child_2.childCount + " children!");

        Transform Child_3 = ParentGO.transform.Find("Sphere_Child_3").GetChild(1);
        Debug.Log("The second Child of the third child is " + Child_3.name);

        Debug.Log("The parent of that child is: " + Child_3.transform.parent.name + " and its' grandfather is: " + Child_3.transform.parent.parent.name);

    }

    private static void GetComponentDemo()
    {
        Debug.Log("The Hidden Value is: " + GameObject.Find("Capsule_Child_1").GetComponent<HiddenScript>().HiddenValue);
        GameObject.Find("Capsule_Child_1").GetComponent<HiddenScript>().SetHidden(2);
        Debug.Log("The Hidden Value now is: " + GameObject.Find("Capsule_Child_1").GetComponent<HiddenScript>().HiddenValue);
    }

    private static void FindAndAddDemo()
    {
        GameObject.Find("SoloSphere").AddComponent<Rigidbody>();
        Debug.Log("SoloSphere now has a rigidbody!");
    }

    private void AddComponentDemo()
    {
        DraggedObject.AddComponent<Rigidbody>();
        Debug.Log(DraggedObject.name + " now has a rigidbody!");
    }

    private static void FindDemo()
    {
        GameObject TheGO = GameObject.Find("SoloSphere");
        Debug.Log("We found " + TheGO.name + " which has the tag: " + TheGO.tag);
    }

    private void FindObjectsWithTagDemo()
    {
        Cubes = GameObject.FindGameObjectsWithTag("Cubes").Length;
        Spheres = GameObject.FindGameObjectsWithTag("Spheres").Length;
        Capsules = GameObject.FindGameObjectsWithTag("Capsules").Length;
        Debug.Log("We have:\n" + Cubes + " Cubes, " + Spheres + " Spheres, " + Capsules + " Capsules!");
        StartCoroutine(DestroySpheresAfter2(GameObject.FindGameObjectsWithTag("Spheres")));

    }

    private IEnumerator DestroySpheresAfter2(GameObject[] Spheres)
    {
        yield return new WaitForSeconds(2);

        for (int i = 0; i < Spheres.Length; i++)
        {
            Destroy(Spheres[i]);
        }

        Debug.Log("And all spheres have died!");
    }

    private void FindObjectsOfTypeDemo()
    {
        var Items = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject item in Items)
        {
            Debug.Log(item.name);
        }

        foreach (GameObject item in Items)
        {
            if (item.CompareTag("Cubes"))
                Cubes++;

            if (item.CompareTag("Spheres"))
                Spheres++;

            if (item.CompareTag("Capsules"))
                Capsules++;

        }

        var Items2 = GameObject.FindObjectsOfType<MeshRenderer>();
        

        Debug.Log("We have:\n" + Cubes + " Cubes, " + Spheres + " Spheres, " + Capsules + " Capsules!");
        Debug.Log("We have " + Items2.Length + " MeshRenderers Found!");
    }
    
}
