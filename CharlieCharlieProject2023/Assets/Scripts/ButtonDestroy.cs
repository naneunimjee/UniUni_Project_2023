using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{

    Animator anim;
    [SerializeField] GameObject[] doors;

    bool player1OnButton = false;
    bool player2OnButton = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void UpdateDoorsState()
    {
        if (player1OnButton || player2OnButton)
        {
            anim.SetBool("ButtonHit", true);

            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
        else
        {
            anim.SetBool("ButtonHit", false);

            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
    }

    void SetPlayerButtonState(Collision2D collision, bool state)
    {
        if (collision.gameObject.tag == "Player1")
        {
            player1OnButton = state;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            player2OnButton = state;
        }

        UpdateDoorsState();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            SetPlayerButtonState(collision, true);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            SetPlayerButtonState(collision, false);
        }
    }
}
