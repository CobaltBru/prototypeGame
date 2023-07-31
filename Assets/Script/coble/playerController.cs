using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rigid;
    public float moveSpeed = 5; //이동속도
    public float gravity = 3.0f; //중력

    public float jumpPower = 10; //점프 힘
    public int jumpAble = 2;//총 점프 가능 횟수
    int jumpCount = 0; // 현재 점프 횟수


    Vector2 movementV = new Vector2(0, 0); //이동 힘
    bool movement = false; //이동여부
    bool isJump = false; //점프여부
    bool onGround = true; //지면에 닿아있는지

    bool firstGroundHit = false; //처음 바닥에 닿았을때 한번 속도 초기화

    public Vector2 boxCastSize = new Vector2(0.2f, 0.05f); //boxcast box사이즈
    public float boxCastMaxDistance = 0.625f; //boxcast 거리
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        onGround = GroundCheck();
        movementV.x = Input.GetAxisRaw("Horizontal");
        movementV.y = Input.GetAxisRaw("Vertical");
        if(onGround) //지면
        {
            if(rigid.velocity.y<0)//낙하중일때 지면에 닿을경우 점프끝
            {
                isJump = false;
                jumpCount = 0;
            }
            if(!movement && !isJump)//x축움직임 없고 점프중도 아니면 중력 off
            {
                rigid.gravityScale = 0.0f;
            }
            
        }
        else //지면에서 떨어지면 중력 On
        {
            rigid.gravityScale = gravity;
        }
        if(movementV.x != 0) //x가 입력되면 이동
        {
            movement = true;
            rigid.velocity = new Vector2(movementV.x * moveSpeed, rigid.velocity.y);
        }

        if(movement == true) //이동->멈춤 순간 1회
        {
            if(movementV.x == 0 && onGround && !isJump) //점프중이 아니고 땅에 닿았고 x입력이 없으면 정지
            {
                movement = false;
                rigid.velocity = new Vector2(0, 0);
            }
            else if(movementV.x == 0) //점프중 x축 이동관성 감소
            {
                rigid.velocity = new Vector2(rigid.velocity.x * 0.2f, rigid.velocity.y);
            }
        }

        if(Input.GetKeyDown(KeyCode.W)) //점프
        {
            
            if(jumpCount<jumpAble) //다중점프 체크
            {
                movement = true;
                jumpCount++;
                Jump();
            }
            
        }
        if(Input.GetKeyUp(KeyCode.W) && !onGround) //점프중 입력 해제
        {
            if(rigid.velocity.y>0) //올라가는도중에만
            {
                JumpDown(); //떨어지는 속력 증가
            }
            
        }
    }

    private void FixedUpdate()
    {

    }

    bool GroundCheck() //바닥 체크
    {
        RaycastHit2D[] cols = Physics2D.BoxCastAll(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Ground"));
        for (int i = 0;i<cols.Length;i++)//Boxcast로 Layer가 Ground인 물체 감지
        {
            if (cols[i].collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }


    void Jump() //점프
    {
        isJump = true;
        rigid.velocity = new Vector2(0,0);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    void JumpDown() //낙하
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
    }

    void OnDrawGizmos() //레이확인
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Ground"));

        Gizmos.color = Color.red;
        if (raycastHit.collider != null)
        {
            Gizmos.DrawRay(transform.position, Vector2.down * raycastHit.distance);
            Gizmos.DrawWireCube(transform.position + Vector3.down * raycastHit.distance, boxCastSize);
        }
        else
        {
            Gizmos.DrawRay(transform.position, Vector2.down * boxCastMaxDistance);
        }
    }
}
