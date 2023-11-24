using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLadder : MonoBehaviour
{

    Animator anim;
    public GameObject ladder;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        ladder.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", true);
            ladder.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", false);
            ladder.SetActive(false);
        }
    }
}
