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
    public float sensorDistance = 10.0f; 
    public List<Collider2D> colliders;
    Vector2 target;
    float targetDistance=9999999f;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        deg = 0;
        radius = 5.0f;
    }

    
    void Update()
    {
        mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        deg = Quaternion.FromToRotation(Vector2.up, mouse - (Vector2)transform.position).eulerAngles.z;
        if (player.transform.localScale.x < 0f)
        {
            deg *= -1;
        }
        var rad = Mathf.Deg2Rad * (deg);
        var x = radius * Mathf.Sin(rad);
        var y = radius * Mathf.Cos(rad);
        RaycastHit2D[] raycastHits = Physics2D.BoxCastAll(transform.position, transform.lossyScale,deg, mouse, sensorDistance, LayerMask.GetMask("Enemy"));
        Debug.DrawRay(transform.position, mouse - (Vector2)transform.position, Color.red);
        if(raycastHits.Length == 0)
        {
            Debug.Log("null");
        }
        else
        {
            for (int i = 0; i < raycastHits.Length; i++)
            {
                float disttmp = Vector2.Distance(transform.position, raycastHits[i].transform.position);
                if (disttmp < targetDistance)
                {
                    target = raycastHits[i].transform.position;
                    targetDistance = disttmp;
                }
            }
            Debug.Log(target);
        }
    }

}
