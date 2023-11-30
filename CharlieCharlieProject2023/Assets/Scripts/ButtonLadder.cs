using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonLadder : MonoBehaviour
{

    Animator anim;
    public GameObject ladder;

    bool player1_OnButton = false;
    bool player2_OnButton = false;
    
    void Awake()
    {
        anim = GetComponent<Animator>();
        ladder.SetActive(false);
    }

    void SetPlayerButtonState(Collision2D collision, bool state)
    {
        if(collision.gameObject.tag == "Player1")
        {
            player1_OnButton = state;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            player2_OnButton = state;
        }

        UpdateLadderState();
    }

    void UpdateLadderState()
    {
        if (player1_OnButton || player2_OnButton)
        {
            anim.SetBool("ButtonHit", true);
            ladder.SetActive(true);
        }
        else
        {
            anim.SetBool("ButtonHit", false);
            ladder.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            SetPlayerButtonState(collision, true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            SetPlayerButtonState(collision, false);
        }
    }
}
