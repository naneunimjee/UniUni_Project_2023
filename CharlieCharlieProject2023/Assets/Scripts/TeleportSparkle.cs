using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportSparkle : MonoBehaviour
{   

    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            animator.SetTrigger("teleport_sparkle");
        }
    }
}
