using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        Vector3 moveVelocity = new Vector3(x, 0, 0) * moveSpeed * Time.deltaTime;
        this.transform.position += moveVelocity;
    }
    void Jump()
    {

    }
}
