using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackScript : MonoBehaviour
{
    Rigidbody2D rigid;
    Vector2 target;
    Vector2 mouse;
    public float power;

    public float continueTime = 0.1f;
    float timeCounter;

    bool attacked = false;
    void Start()
    {
        rigid= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            attacked = true;
            timeCounter = continueTime;
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //커서의 현재위치 월드좌표
            target = mouse - (Vector2)transform.position; //캐릭터->마우스의 벡터
            rigid.velocity = new Vector2(0, 0);//현재 움직임을 0으로 초기화
            //Debug.Log(target.normalized);
        }
        
    }

    void FixedUpdate()
    {
        Debug.Log(timeCounter);
        if(attacked)
        {
            if (timeCounter > 0)
            {
                //Debug.Log(timeCounter);
                timeCounter -= Time.deltaTime;
                AttackMove();
            }
            else if (timeCounter <= 0)
            {
                rigid.velocity = new Vector2(Vector2.down.x*0.2f, Vector2.down.y * 0.2f);
                attacked = false;
            }
        }
        
    }

    void Attack()
    {

    }

    void AttackMove()
    {

        rigid.velocity = new Vector2(target.normalized.x * power, target.normalized.y * power);
    }
}
