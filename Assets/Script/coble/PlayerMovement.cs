using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public controllerScript controller;
    int way;
    bool jump = false;
    void Awake()
    {
        controller = this.GetComponent<controllerScript>();
        way = 1;
    }
    void Update()
    {
        if(Input.GetKey(KeyCode.D))
        {
            way = 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            way = -1;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            way = 0;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            way = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            jump = false;
        }
    }

    void FixedUpdate()
    {
        controller.Move(way);
        controller.Jump(jump);
        jump = false;
        
    }
}
