using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabaraJump : MonoBehaviour
{
    
    Animator animator;
    Rigidbody2D rigid;

    void Awake()
    {
        animator = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();
    }
    

void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Platform")
        {
            animator.SetBool("P2_isJumping", false);
        }
    }
}
