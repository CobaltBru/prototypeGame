using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.Tilemaps;

public class CS_Cursor : MonoBehaviour
{
  // Start is called before the first frame update

  public Vector3 worldPos;
  public Vector3Int cellPos;
  public Tilemap tileMap;

  public delegate void MoveDelegate(Vector3Int vector);
  public MoveDelegate moveDelegate;

  Renderer sr;
  private void Awake()
  {
    Debug.Log("CursorInit");

    sr = gameObject.GetComponent<Renderer>();

  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      moveDelegate(cellPos);
    }


    worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    cellPos = tileMap.WorldToCell(worldPos); 


    transform.position = cellPos;
  }

  private void OnTriggerStay2D(Collider2D collision)
  {
    Debug.Log("Triggered!");
    if (collision.gameObject.tag == "wall")
    {
      sr.material.color = Color.red;

    }
    else if (collision.gameObject.tag == "background")
    {
      sr.material.color = Color.blue;
    }
  }
  
}
