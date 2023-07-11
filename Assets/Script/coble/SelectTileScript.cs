using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SelectTileScript : MonoBehaviour
{
    public GameObject player;
    public GameObject cursor;
    int moveCount;
    Vector2 mousePosition;
    void Awake()
    {
        moveCount = player.GetComponent<Player>().moveCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
