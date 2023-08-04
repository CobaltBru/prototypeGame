using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    Animator animation;
    SpriteRenderer spriteRenderer;
    private void Start()
    {
        animation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Walk(bool flag) //걷기
    {
        animation.SetBool("isWalk", flag);
    }
    public void Jump(bool flag) //점프
    {
        animation.SetBool("isJump", flag);
    }
    public void NormalAttack() //기본공격
    {
        animation.SetTrigger("isAttack");
    }
    public void FlipX(bool flag) //방향전환
    {
        spriteRenderer.flipX = flag;
    }
}
