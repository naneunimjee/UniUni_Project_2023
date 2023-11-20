using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyButton : MonoBehaviour
{

    Animator anim;
    [SerializeField] GameObject[] doors;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", true);

            foreach (GameObject door in doors)
            {
                door.SetActive(false);
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", false);

            foreach (GameObject door in doors)
            {
                door.SetActive(true);
            }
        }
    }
}