using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAnimation : MonoBehaviour
{
    Animator animation;
    private void Start()
    {
        animation = GetComponent<Animator>();
    }

    public void Walk(bool flag)
    {
        animation.SetBool("isWalk", flag);
    }
    public void Jump(bool flag)
    {
        animation.SetBool("isJump", flag);
    }
}
