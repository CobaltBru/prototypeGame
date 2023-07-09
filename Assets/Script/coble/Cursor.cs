using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    Vector2 mousePosition;
    Renderer sr;
    void Start()
    {
        sr = gameObject.GetComponent<Renderer>();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
    }

    
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("fdsa");
        if (collision.gameObject.tag == "wall")
        {
            sr.material.color = Color.red;
            
        }
        else if(collision.gameObject.tag == "background")
        {
            sr.material.color = Color.blue;
        }
    }
}
