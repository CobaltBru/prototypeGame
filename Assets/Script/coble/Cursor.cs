using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Cursor : MonoBehaviour
{
    Vector2 mousePosition;
    public Tilemap walltile;
    public Tilemap selectTile;
    public TileBase changeTile;
    public GameObject player;
    int moveCount;
    List<Vector2> clickedList = new List<Vector2>();
    SpriteRenderer sr;

    int[] mx = new int[4] { 1, 0, -1, 0 };
    int[] my = new int[4] { 0, 1, -1, 0 };
    private void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        moveCount = 0;
    }
    void OnEnable()
    {
        moveCount = GetComponent<Player>().moveCount;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        clickedList.Add(mousePosition);
    }

    
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition = new Vector2(Mathf.Round(mousePosition.x), Mathf.Round(mousePosition.y));
        transform.position = mousePosition;
        cursorColorProcess(mousePosition);
        if(Input.GetMouseButtonDown(0))
        {
            tileSelectProcess(mousePosition);
        }
    }

    private void OnDisable()
    {
        foreach(Vector2 pos in clickedList)
        {
            selectTile.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), changeTile);
        }
        clickedList.Clear();
    }

    bool checkTile(Tilemap tilemap,Vector2 pos) //장애물 체크
    {
        Vector3Int ppos = Vector3Int.FloorToInt(pos);
        TileBase tile = tilemap.GetTile(ppos);
        if (tile != null) return true;
        else return false;
    }

    bool checkClicked(Vector2 pos) //이미 클릭했던 좌표라면 true 반환
    {
        for(int i = 1;i<clickedList.Count-1; i++)
        {
            if (clickedList[i] == pos) return true;
        }
        return false;
    }

    bool checkLinkedTile(Vector2 pos) //마지막으로 선택된 좌표와 연결되어 있나
    {
        if (Vector2.Distance(pos, clickedList[^1]) <= 1.5f) return true;
        else return false;
    }

    void tileSelectProcess(Vector2 pos)
    {
        //타일이 선택되는 조건
        //1. 커서가 파란색일것(커서에서 이미 모든 조건을 판별함)
        //2. 선택 가능한 횟수가 남아있을 것
        //ex. 마지막으로 선택된 타일을 한번 더 선택하면 취소될 것 
        if(pos == clickedList[^1])
        {
            selectTile.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), null);
            clickedList.RemoveAt(-1);
            moveCount++;
            return;
        }
        if ((sr.color == Color.blue)) 
        {
            selectTile.SetTile(new Vector3Int((int)pos.x, (int)pos.y, 0), changeTile);
            moveCount--;
        }
    }

    void cursorColorProcess(Vector2 pos)
    {
        //이동이 허용되는 조건
        //1. 빈 공간일것
        //2. 마지막으로 선택된 타일과 연결되어 있을것
        //3. 이미 선택된 타일이 아닐 것
        //4. 선택 가능한 횟수가 남아있을 것
        if ((checkTile(walltile, pos) == false) && checkLinkedTile(pos) &&
            (checkClicked(pos) == false) && (moveCount > 0)) 
        {
            sr.color = Color.blue;
        }
        else
        {
            sr.color = Color.red;
        }
    }
}
