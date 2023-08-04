using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class attackScript : MonoBehaviour
{
    public GameObject tmpBox; //���ݹ���ǥ�ÿ� �ӽ� �ڽ�
    public playerAnimation ani; //�ִϸ��̼�
    float normal_attack_timer; //�⺻���� ��Ÿ�� Ÿ�̸�
    float normal_attack_cooltime = 0.1f; //�⺻���� ��Ÿ��

    
    public float normal_radius; //�⺻���� ��Ÿ�
    public Vector2 normal_boxSize; //�⺻���� ����

    Vector2 wayVec; //�ӽ� ����
    void Start()
    {
        
    }
    private void Update()
    {
        if(normal_attack_timer> 0) //Ÿ�̸Ӱ� ���õǸ� Ÿ�̸� ����
        {
            normal_attack_timer -= Time.deltaTime;
        }
    }

    public void normal_attack() //�⺻���� Ʈ����
    {
        if(normal_attack_timer<=0)
        {
            normal_attack_timer = normal_attack_cooltime; //Ÿ�̸� ����
            na(); 
        }
    }
    private void na() //�⺻����
    {
        makeOverlapBox(normal_radius, normal_boxSize); //����
        ani.NormalAttack(); //���� �ִϸ��̼�
    }

    private void makeOverlapBox(float radius, Vector2 boxSize) //���� ���� �Լ�
    {
        //���콺�� ��� �������� ������ �ϵ��� ���콺 ������ ��ǥ�� �޾� ó��
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float deg = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        if((deg>0 && deg<90) || (deg<0&&deg>-90)) //���콺 �����Ϳ� ���� ĳ���� ���� ����
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
        Collider2D[] cols = Physics2D.OverlapBoxAll((Vector2)transform.position + wayVec, boxSize, deg); //����
        tmpBox.transform.position = (Vector2)transform.position + wayVec; //�ӽùڽ��̵�
        tmpBox.transform.rotation = Quaternion.Euler(0, 0, deg);
        foreach (Collider2D col in cols) //���� ó��
        {
            Debug.Log(col.tag);
            //col.gameObject.SetActive(false);
        }
    }

    //private void OnDrawGizmos() //�����
    //{
    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireCube(wayVec+ (Vector2)transform.position, normal_boxSize);
    //}
}
