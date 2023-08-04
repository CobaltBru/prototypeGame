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

    public void Walk(bool flag) //�ȱ�
    {
        animation.SetBool("isWalk", flag);
    }
    public void Jump(bool flag) //����
    {
        animation.SetBool("isJump", flag);
    }
    public void NormalAttack() //�⺻����
    {
        animation.SetTrigger("isAttack");
    }
    public void FlipX(bool flag) //������ȯ
    {
        spriteRenderer.flipX = flag;
    }
}
