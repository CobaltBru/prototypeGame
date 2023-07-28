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
        rigid.velocity = new Vector2(0, 0); //���� � �ʱ�ȭ
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition); //���콺 ��ǥ
        Debug.Log(mouse);
        start = (Vector2)transform.position; //ĳ���� ��ǥ
        dist = Vector2.Distance(start, mouse); //���콺�� ĳ���� �Ÿ�
        degVec = (mouse - start).normalized; //���콺->ĳ���� ���͸� ���ϰ� ����ȭ
        rigid.AddForce(degVec*power,ForceMode2D.Impulse); // �̵�
    }

}
