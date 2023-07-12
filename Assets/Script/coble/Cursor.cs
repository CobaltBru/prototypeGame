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

    bool checkTile(Tilemap tilemap,Vector2 pos) //��ֹ� üũ
    {
        Vector3Int ppos = Vector3Int.FloorToInt(pos);
        TileBase tile = tilemap.GetTile(ppos);
        if (tile != null) return true;
        else return false;
    }

    bool checkClicked(Vector2 pos) //�̹� Ŭ���ߴ� ��ǥ��� true ��ȯ
    {
        for(int i = 1;i<clickedList.Count-1; i++)
        {
            if (clickedList[i] == pos) return true;
        }
        return false;
    }

    bool checkLinkedTile(Vector2 pos) //���������� ���õ� ��ǥ�� ����Ǿ� �ֳ�
    {
        if (Vector2.Distance(pos, clickedList[^1]) <= 1.5f) return true;
        else return false;
    }

    void tileSelectProcess(Vector2 pos)
    {
        //Ÿ���� ���õǴ� ����
        //1. Ŀ���� �Ķ����ϰ�(Ŀ������ �̹� ��� ������ �Ǻ���)
        //2. ���� ������ Ƚ���� �������� ��
        //ex. ���������� ���õ� Ÿ���� �ѹ� �� �����ϸ� ��ҵ� �� 
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
        //�̵��� ���Ǵ� ����
        //1. �� �����ϰ�
        //2. ���������� ���õ� Ÿ�ϰ� ����Ǿ� ������
        //3. �̹� ���õ� Ÿ���� �ƴ� ��
        //4. ���� ������ Ƚ���� �������� ��
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
