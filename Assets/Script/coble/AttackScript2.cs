using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript2 : MonoBehaviour
{
    Rigidbody2D rigid;

    Vector2 mouse;
    Vector2 start;
    Vector2 degVec;
    float dist;
    public float power = 10;
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            clicked();
        }
    }

    private void clicked()
    {
        rigid.velocity = new Vector2(0, 0); //현재 운동 초기화
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //마우스 좌표
        Debug.Log(mouse);
        start = (Vector2)transform.position; //캐릭터 좌표
        dist = Vector2.Distance(start, mouse); //마우스와 캐릭터 거리
        degVec = (mouse - start).normalized; //마우스->캐릭터 벡터를 구하고 정규화
        rigid.AddForce(degVec*power,ForceMode2D.Impulse); // 이동
    }

}
