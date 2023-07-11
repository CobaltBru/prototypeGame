using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour
{
    Vector2 mousePosition;
    public Tilemap walltile;
    public Tilemap SelectTile;
    SpriteRenderer sr;
    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
    }

    
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;
        if(checkTile(walltile, mousePosition)==true)
        {
            sr.color = Color.red;
        }
        else
        {
            sr.color = Color.blue;
        }
    }

    bool checkTile(Tilemap tilemap,Vector2 pos)
    {
        Vector3Int ppos = Vector3Int.FloorToInt(pos);
        TileBase tile = tilemap.GetTile(ppos);
        if (tile != null) return true;
        else return false;
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Debug.Log("fdsa");
    //    if (collision.gameObject.tag == "wall")
    //    {
    //        sr.color = Color.red;
            
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Debug.Log("asdf");
    //    if (collision.gameObject.tag == "wall")
    //    {
    //        sr.color = Color.blue;

    //    }
    //}
}
