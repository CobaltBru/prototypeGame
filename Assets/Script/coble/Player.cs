using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    Vector2 mousePosition;
    //public Tilemap wallTile;
    public GameObject cursor;
    public int moveCount; // �� ĳ������ �̵� ����
    bool selectPlayer;


    void Start()
    {
        selectPlayer = false;
        moveCount = 0;
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
                if (selectPlayer  && cursor.GetComponent<SpriteRenderer>().color==Color.blue)
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
            return true;
        }
        else return false;
    }
    //Ŀ���� ������ �̵� ������ �Ǻ������ν� ���̻� �ʿ� ����
    //bool checkTile(Tilemap tilemap, Vector2 pos)
    //{
    //    Vector3Int ppos = Vector3Int.FloorToInt(pos);
    //    TileBase tile = tilemap.GetTile(ppos);
    //    if (tile == null) return true;
    //    else return false;
    //}

    
}
