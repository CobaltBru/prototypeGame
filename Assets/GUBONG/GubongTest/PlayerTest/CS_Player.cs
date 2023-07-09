using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CS_Player : MonoBehaviour
{
  
  public CS_Cursor Cursor;

  Vector3Int cellPos;

  private void Awake()
  {
    Cursor = gameObject.GetComponentInChildren<CS_Cursor>();

    if (Cursor != null)
    {
      Cursor.moveDelegate += PlayerMove;
      Debug.Log("delegate Added");
    }
    
  }

  private void PlayerMove(Vector3Int vector)
  {
    gameObject.transform.position = vector; 
  }
}
