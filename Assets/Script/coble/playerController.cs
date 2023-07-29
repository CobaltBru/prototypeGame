using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rigid;
    public Transform groundSensor;
    public Transform ceilSensor;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public float moveSpeed = 10;
    public float jumpPower = 10;
    public int jumpAble = 2;
    int jumpCount = 0;
    bool movement = false;
    bool isJump = false;
    bool onGround = true;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = GroundCheck();
        if(onGround)
        {
            jumpCount = 0;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            movement = true;
        }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            movement = false;
            MoveStop();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log(jumpCount);
            if(jumpCount<jumpAble)
            {
                jumpCount++;
                isJump = true;
                Jump();
            }
            
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            isJump = false;
            JumpDown();
        }
    }

    private void FixedUpdate()
    {
        if(movement)
        {
            Move();
        }
    }

    bool GroundCheck()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(groundSensor.position, groundCheckRadius, groundLayer);
        for(int i = 0;i<cols.Length;i++)
        {
            if (cols[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    private void Move()
    {
        float xmove = Input.GetAxis("Horizontal");
        rigid.velocity = new Vector2(xmove * moveSpeed, rigid.velocity.y);
    }
    private void MoveStop()
    {
        if(onGround)
        {
            rigid.velocity = new Vector2(0,0);
        }
        else
        {
            rigid.velocity = new Vector2(rigid.velocity.x * 0.2f, rigid.velocity.y);
        }
        
    }

    void Jump()
    {
        rigid.velocity = new Vector2(0,0);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    void JumpDown()
    {
        rigid.velocity = new Vector2(rigid.velocity.x, rigid.velocity.y * 0.2f);
    }
}
