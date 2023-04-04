using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform Player;

    void FixedUpdate()
    {

        transform.position = new Vector3(Player.position.x, Player.position.y, -1);
        
    }





}
