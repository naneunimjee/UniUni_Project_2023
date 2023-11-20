using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMoving : MonoBehaviour
{

    Animator anim;
    public MovingPlatform movingPlatform;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", true);

            if (movingPlatform != null)
            {
                movingPlatform.StartMoving();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", false);

            if (movingPlatform != null)
            {
                movingPlatform.StopMoving();
            }
        }
    }
}
