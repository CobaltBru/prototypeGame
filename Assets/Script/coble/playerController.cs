using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    Rigidbody2D rigid;
    public playerAnimation ani; //�ִϸ��̼� ��ũ��Ʈ
    public attackScript atk; //���� ��Ʈ��Ʈ
    public float moveSpeed = 5; //�̵��ӵ�
    public float gravity = 3.0f; //�߷�

    public float jumpPower = 10; //���� ��
    public int jumpAble = 2;//�� ���� ���� Ƚ��
    int jumpCount = 0; // ���� ���� Ƚ��


    Vector2 movementV = new Vector2(0, 0); //�̵� ��
    bool movement = false; //�̵�����
    bool isJump = false; //��������
    bool onGround = true; //���鿡 ����ִ���

    bool isLadder = false;
    public float ladderSpeed = 1.0f;
    Vector2 currentLadderV;
    float currentLadderS;
    bool firstGroundHit = false; //ó�� �ٴڿ� ������� �ѹ� �ӵ� �ʱ�ȭ

    public Vector2 boxCastSize = new Vector2(0.2f, 0.05f); //boxcast box������
    public float boxCastMaxDistance = 0.625f; //boxcast �Ÿ�

    
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
        if(onGround) //����
        {
            if(rigid.velocity.y<0)//�������϶� ���鿡 ������� ������
            {
                isJump = false;
                ani.Jump(false);
                jumpCount = 0;
            }
            if(!movement && !isJump)//x������� ���� �����ߵ� �ƴϸ� �߷� off
            {
                rigid.gravityScale = 0.0f;
            }
            
        }
        else if(!onGround && !isLadder) //���鿡�� �������� �߷� On
        {
            rigid.gravityScale = gravity;
        }
        if(movementV.x != 0) //x�� �ԷµǸ� �̵�
        {
            movement = true;
            rigid.velocity = new Vector2(movementV.x * moveSpeed, rigid.velocity.y);
            if(movementV.x>0)
            {
                ani.FlipX(false);

            }
            else
            {
                ani.FlipX(true);
            }
            ani.Walk(true);
        }

        if(movement == true) //�̵�->���� ���� 1ȸ
        {
            if(movementV.x == 0 && onGround && !isJump) //�������� �ƴϰ� ���� ��Ұ� x�Է��� ������ ����
            {
                movement = false;
                ani.Walk(false);
                rigid.velocity = new Vector2(0, 0);
            }
            else if(movementV.x == 0) //������ x�� �̵����� ����
            {
                rigid.velocity = new Vector2(rigid.velocity.x * 0.2f, rigid.velocity.y);
            }
        }

        if(Input.GetKeyDown(KeyCode.W) && !isLadder) //����
        {
            
            if(jumpCount<jumpAble) //�������� üũ
            {
                ani.Jump(true);
                movement = true;
                jumpCount++;
                Jump();
            }
            
        }
        if(Input.GetKey(KeyCode.W) && isLadder)
        {
            if (movementV.x == 0) rigid.velocity = new Vector2(0, 0);
            this.transform.position = new Vector2(currentLadderV.x, this.transform.position.y);
            Ladder(movementV.y);
        }
        if (Input.GetKey(KeyCode.S) && isLadder)
        {
            if (movementV.x == 0) rigid.velocity = new Vector2(0, 0);
            this.transform.position = new Vector2(currentLadderV.x, this.transform.position.y);
            Ladder(movementV.y);
        }
        if (Input.GetKeyUp(KeyCode.W) && !onGround && !isLadder) //������ �Է� ����
        {
            if(rigid.velocity.y>0) //�ö󰡴µ��߿���
            {
                JumpDown(); //�������� �ӷ� ����
            }
            
        }

        if(Input.GetMouseButtonDown(0))
        {
            if(onGround)
            {
                atk.normal_attack();
            }
        }
    }

    private void FixedUpdate()
    {
       
    }

    bool GroundCheck() //�ٴ� üũ
    {
        RaycastHit2D[] cols = Physics2D.BoxCastAll(transform.position, boxCastSize, 0f, Vector2.down, boxCastMaxDistance, LayerMask.GetMask("Ground"));
        for (int i = 0;i<cols.Length;i++)//Boxcast�� Layer�� Ground�� ��ü ����
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

    void Ladder(float k)
    {
        if(this.gameObject.layer == 7)
        {
            rigid.gravityScale = 0.0f;
            movement = false;
            onGround = true;
            isJump = false;
            jumpCount = 0;
            if (k>0)
            {
                this.transform.Translate(0, ladderSpeed * Time.deltaTime, 0);
            }
            if (k < 0)
            {
                this.transform.Translate(0, -ladderSpeed * Time.deltaTime, 0);
            }
        }
    }
    void LadderOut()
    {
        Debug.Log("ladder out");
        movement = true;
        onGround = false;
        this.rigid.gravityScale = gravity;
        this.gameObject.layer = 31;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            Debug.Log("inLadder");
            isLadder= true;
            this.gameObject.layer = 7;
            currentLadderV = (Vector2)collision.gameObject.transform.position;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Ladder")
        {
            isLadder= false;
            LadderOut();
        }
    }

}
