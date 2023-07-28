using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerScript : MonoBehaviour
{
    Rigidbody2D rigid;
    public float speed;
    public float jumpPower;
    void Start()
    {
        rigid= GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        
    }

    public void Move(int way) // right = 1, left = -1
    {
        rigid.velocity = new Vector2(way*speed,rigid.velocity.y);
    }

    public void Jump(bool isJump)
    {
        if(isJump)
        {
            rigid.AddForce(new Vector2(rigid.velocity.x, jumpPower),ForceMode2D.Impulse);
        }
        
    }
}
