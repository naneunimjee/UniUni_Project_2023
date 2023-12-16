using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonMoving : MonoBehaviour
{

    Animator anim;
    public MovingPlatform movingPlatform;
    public AudioManager audioManager;

    bool Player1_OnButton = false;
    bool Player2_OnButton = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void SetPlayerButtonState(Collision2D collision, bool state)
    {
        if (collision.gameObject.tag == "Player1")
        {
            Player1_OnButton = state;
        }
        else if (collision.gameObject.tag == "Player2")
        {
            Player2_OnButton = state;
        }

        UpdateStoolState();
    }

    void UpdateStoolState()
    {
        if (Player1_OnButton || Player2_OnButton)
        {
            anim.SetBool("ButtonHit", true);
            audioManager.PlaySound("PushBtn");

            if (movingPlatform != null)
            {
                movingPlatform.StartMoving();
            }
        }
        else
        {
            anim.SetBool("ButtonHit", false);
            if (movingPlatform != null)
            {
                movingPlatform.StopMoving();
            }
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
