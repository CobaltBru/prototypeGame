using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class CS_CameraBoom : MonoBehaviour
{
  public CS_Player PlayerRef;

  private void Start()
  {
    CS_Cursor Cursor = PlayerRef.Cursor;

    if (Cursor != null)
    {
      Cursor.moveDelegate += CameraMove;
      Debug.Log("delegate Added");
    }
  }

  void CameraMove(Vector3Int vector)
  {
    vector.z = -10;
    transform.position = vector;  
  }
}
