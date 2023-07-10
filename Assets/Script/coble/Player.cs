using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Vector2 mousePosition;
    public Tilemap wallTile;
    public GameObject cursor;
    bool selectPlayer;
    void Start()
    {
        selectPlayer = false;
    }

    
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        if(Input.GetMouseButtonDown(0))
        {
            
            if (checkPlayer(mousePosition))
            {
                selectPlayer = !selectPlayer;
                cursor.SetActive(selectPlayer);
            }
            else
            {
                if(selectPlayer && checkTile(wallTile,mousePosition))
                {
                    selectPlayer = false;
                    cursor.SetActive(selectPlayer);
                    transform.position = mousePosition;
                }
            }
        }
    }

    bool checkPlayer(Vector2 pos)
    {
        if (transform.position == (Vector3)pos)
        {
            Debug.Log(transform.position.ToString() +" "+(Vector3)pos);
            return true;
        }
        else return false;
    }
    bool checkTile(Tilemap tilemap, Vector2 pos)
    {
        Vector3Int ppos = Vector3Int.FloorToInt(pos);
        TileBase tile = tilemap.GetTile(ppos);
        if (tile == null) return true;
        else return false;
    }
}
