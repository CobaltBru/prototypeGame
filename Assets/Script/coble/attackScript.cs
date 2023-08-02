using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class attackScript : MonoBehaviour
{
    public playerAnimation ani;
    float normal_attack_timer;
    float normal_attack_cooltime = 0.1f;

    
    public float normal_radius;
    public Vector2 normal_boxSize;

    Vector2 wayVec;
    void Start()
    {
        
    }
    private void Update()
    {
        if(normal_attack_timer> 0)
        {
            normal_attack_timer -= Time.deltaTime;
        }
    }

    public void normal_attack()
    {
        if(normal_attack_timer<=0)
        {
            normal_attack_timer = normal_attack_cooltime;
            na();
        }
    }
    private void na()
    {
        makeOverlapBox(normal_radius, normal_boxSize);
        ani.NormalAttack();
    }

    private void makeOverlapBox(float radius, Vector2 boxSize)
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 degVec = (mouse - (Vector2)transform.position).normalized;
        float deg = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90.0f;
        var rad = Mathf.Deg2Rad * -(deg);
        wayVec.x = radius * Mathf.Sin(rad);
        wayVec.y = radius * Mathf.Cos(rad);
        Collider2D[] cols = Physics2D.OverlapBoxAll(wayVec+(Vector2)transform.position, boxSize, rad * -1);
        foreach(Collider2D col in cols)
        {
            Debug.Log(col.tag);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wayVec+ (Vector2)transform.position, normal_boxSize);
    }
}
