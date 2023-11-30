using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCreate : MonoBehaviour
{
    Animator anim;
    [SerializeField] GameObject prefab;

    bool player1_OnButton = false;
    bool player2_OnButton = false;

    bool instantiate = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void UpdateStoolState()
    {
        if (player1_OnButton || player2_OnButton)
        {
            anim.SetBool("ButtonHit", true);

            if (!instantiate)
            {
                Instantiate(prefab, new Vector3(4.5f, -10.5f, 0), Quaternion.identity);
                instantiate = true;
            }
        }

        else
        {
            anim.SetBool("ButtonHit", false);
            if (instantiate)
            {
                Destroy(GameObject.FindGameObjectWithTag("Prefab")); // 태그가 Prefab인 오브젝트를 찾아 삭제
                instantiate = false;
            }
        }
    }

    void SetPlayerButtonState(Collision2D collision, bool state)
    {
        if (collision.gameObject.tag == "Player1")
        {
            player1_OnButton = state;
        }
        else if(collision.gameObject.tag == "Player2")
        {
            player2_OnButton = state;
        }

        UpdateStoolState();
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
        if(collision.gameObject.tag=="Player1"|| collision.gameObject.tag == "Player2")
        {
            SetPlayerButtonState(collision, false);
        }
    }
}
