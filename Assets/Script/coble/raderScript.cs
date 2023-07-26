using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raderScript : MonoBehaviour
{
    Rigidbody2D rigid;
    public float deg;
    public float radius;
    public GameObject player;
    Vector2 mouse;
    Vector2 dirVec;

    public List<Collider2D> colliders;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        deg = 0;
        radius = 5.0f;
    }

    
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dirVec = mouse - (Vector2)player.transform.position;
        transform.up = dirVec.normalized;
        if(player.transform.localScale.x>0f)
        {
            deg = transform.eulerAngles.z * -1;
        }
        else
        {
            deg = transform.eulerAngles.z;
        }
        
        var rad = Mathf.Deg2Rad * (deg);
        var x = radius * Mathf.Sin(rad);
        var y = radius * Mathf.Cos(rad);
        rigid.transform.localPosition = new Vector3(x, y);
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        colliders.Add(collision);
    }
}
