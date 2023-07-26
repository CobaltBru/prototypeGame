using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mary_Follow : MonoBehaviour
{
  public GameObject Player;
  public Transform playerTransform; 
  public float followDistance = 1.0f; 
  public float moveSpeed = 8.0f;
  public float smoothTime = 0.1f;

  private Rigidbody2D rb;
  private Vector3 currentVelocity;

  void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    playerTransform = Player.transform;
  }

  // Update is called once per frame
  void Update()
  {
    Vector3 toPlayer = playerTransform.position - transform.position;

    float distanceToPlayer = toPlayer.magnitude;

    if (distanceToPlayer > followDistance)
    {
      Vector3 moveDirection = toPlayer.normalized;
      Vector3 targetVelocity = moveDirection * moveSpeed;
      rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, smoothTime);
    }
    else
    {
      rb.velocity = Vector2.zero;
    }
  }
}
