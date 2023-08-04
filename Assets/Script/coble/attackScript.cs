using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class attackScript : MonoBehaviour
{
    public GameObject tmpBox; //공격범위표시용 임시 박스
    public playerAnimation ani; //애니메이션
    float normal_attack_timer; //기본공격 쿨타임 타이머
    float normal_attack_cooltime = 0.1f; //기본공격 쿨타임

    
    public float normal_radius; //기본공격 사거리
    public Vector2 normal_boxSize; //기본공격 범위

    Vector2 wayVec; //임시 벡터
    void Start()
    {
        
    }
    private void Update()
    {
        if(normal_attack_timer> 0) //타이머가 세팅되면 타이머 시작
        {
            normal_attack_timer -= Time.deltaTime;
        }
    }

    public void normal_attack() //기본공격 트리거
    {
        if(normal_attack_timer<=0)
        {
            normal_attack_timer = normal_attack_cooltime; //타이머 세팅
            na(); 
        }
    }
    private void na() //기본공격
    {
        makeOverlapBox(normal_radius, normal_boxSize); //공격
        ani.NormalAttack(); //공격 애니메이션
    }

    private void makeOverlapBox(float radius, Vector2 boxSize) //범용 공격 함수
    {
        //마우스를 찍는 방향으로 공격을 하도록 마우스 포인터 좌표를 받아 처리
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float deg = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        if((deg>0 && deg<90) || (deg<0&&deg>-90)) //마우스 포인터에 따라 캐릭터 방향 결정
        {
            ani.FlipX(false);
        }
        else
        {
            ani.FlipX(true);
        }
        var rad = Mathf.Deg2Rad * deg;
        wayVec.x = radius * Mathf.Cos(rad);
        wayVec.y = radius * Mathf.Sin(rad);
        Collider2D[] cols = Physics2D.OverlapBoxAll((Vector2)transform.position + wayVec, boxSize, deg); //센서
        tmpBox.transform.position = (Vector2)transform.position + wayVec; //임시박스이동
        tmpBox.transform.rotation = Quaternion.Euler(0, 0, deg);
        foreach (Collider2D col in cols) //공격 처리
        {
            Debug.Log(col.tag);
            //col.gameObject.SetActive(false);
        }
    }

    //private void OnDrawGizmos() //기즈모
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireCube(wayVec+ (Vector2)transform.position, normal_boxSize);
    //}
}
