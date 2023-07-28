using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackScript : MonoBehaviour
{
    Rigidbody2D rigid;
    CharacterController2D controller;
    Vector2 target;
    Vector2 mouse;
    public float power;

    public float continueTime = 0.1f;
    float timeCounter;
    public float coolTime = 0.2f;
    float coolCounter;

    bool attacked = false;

    public float xpower;
    public float ypower;

    int clickCount = 1;
    void Start()
    {
        rigid= GetComponent<Rigidbody2D>();
        controller = this.GetComponent<CharacterController2D>();
        coolCounter = coolTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (coolCounter <= 0))  
        {
            if (controller.isGround()) clickCount = 1;
            else clickCount++;
            attacked = true;
            timeCounter = continueTime;
            coolCounter = coolTime;
            mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //커서의 현재위치 월드좌표
            target = mouse - (Vector2)transform.position; //캐릭터->마우스의 벡터
            rigid.velocity = new Vector2(0, 0);//현재 움직임을 0으로 초기화
            //Debug.Log(target.normalized);
        }
        
    }

    void FixedUpdate()
    {
        Debug.Log(timeCounter);
        if(coolCounter>0) coolCounter -= Time.deltaTime;
        if (attacked)
        {
            
            if (timeCounter > 0)
            {
                //Debug.Log(timeCounter);
                timeCounter -= Time.deltaTime;
                AttackMove();
            }
            else if (timeCounter <= 0)
            {
                rigid.velocity = new Vector2(target.normalized.x * xpower, target.normalized.y * ypower);
                attacked = false;
            }
        }
        
    }

    void Attack()
    {

    }

    void AttackMove()
    {

        rigid.velocity = new Vector2(target.normalized.x * power / (float)clickCount, target.normalized.y * power / (float)clickCount);
    }
}
