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
    public float gravity = 3.0f;

    public float jumpPower = 10; //���� ��
    public int jumpAble = 2;//�� ���� ���� Ƚ��
    int jumpCount = 0; // ���� ���� Ƚ��


    Vector2 movementV = new Vector2(0, 0);
    Vector2 stopPosition;
    bool movement = false;
    bool isJump = false;
    bool comeDown = true;
    bool onGround = true;

    bool firstGroundHit = false; //ó�� �ٴڿ� ������� �ѹ� �ӵ� �ʱ�ȭ

    public Vector2 boxCastSize = new Vector2(0.2f, 0.05f);
    public float boxCastMaxDistance = 0.6f;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        stopPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        onGround = GroundCheck();
        movementV.x = Input.GetAxisRaw("Horizontal");
        movementV.y = Input.GetAxisRaw("Vertical");
        Debug.Log(onGround + " " + movement + " " + isJump);
        if(onGround) //����
        {
            if(rigid.velocity.y<0)//�������϶� ���鿡 ������� ������
            {
                isJump = false;
            }
            if(!movement && !isJump)//x������� ���� �����ߵ� �ƴϸ� �߷� off
            {
                rigid.gravityScale = 0.0f;
            }
            
        }
        else
        {
            rigid.gravityScale = gravity;
        }
        if(movementV.x != 0) //x�� �ԷµǸ� �̵�
        {
            movement = true;
            rigid.velocity = new Vector2(movementV.x * moveSpeed, rigid.velocity.y);
        }

        if(movement == true) //�̵�->���� ���� 1ȸ
        {
            if(movementV.x == 0 && onGround && !isJump) //�������� �ƴϰ� ���� ��Ұ� x�Է��� ������ ����
            {
                movement = false;
                rigid.velocity = new Vector2(0, 0);
            }
            else if(movementV.x == 0) //������ x�� �̵����� ����
            {
                rigid.velocity = new Vector2(rigid.velocity.x * 0.2f, rigid.velocity.y);
            }
        }

        if(Input.GetKeyDown(KeyCode.W)) //����
        {
            movement = true;
            Jump();
        }
        if(Input.GetKeyUp(KeyCode.W) && !onGround) //������ �Է� ����
        {
            if(rigid.velocity.y>0) //�ö󰡴µ��߿���
            {
                JumpDown(); //�������� �ӷ� ����
            }
            
        }
    }

    private void FixedUpdate()
    {

    }

    bool GroundCheck() //�ٴ� üũ
    {
        RaycastHit2D[] cols = Physics2D.BoxCastAll(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Ground"));
        for (int i = 0;i<cols.Length;i++)
        {
            if (cols[i].collider.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }


    void Jump() //����
    {
        isJump = true;
        rigid.velocity = new Vector2(0,0);
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }
    void JumpDown() //����
    {
        rigid.velocity = new Vector2(rigid.velocity.x, 0);
    }

    void OnDrawGizmos() //����Ȯ��
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
