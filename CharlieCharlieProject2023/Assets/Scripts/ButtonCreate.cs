using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCreate : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    Animator anim;
    bool instantiate = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", true);

            if (!instantiate)
            {
                Instantiate(prefab, new Vector3(4.5f, -10.5f, 0), Quaternion.identity);
                instantiate = true;
            }
        }
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player1" || collision.gameObject.tag == "Player2")
        {
            anim.SetBool("ButtonHit", false);
            if (instantiate)
            {
                Destroy(GameObject.FindGameObjectWithTag("Prefab")); // 태그가 Prefab인 오브젝트를 찾아 삭제
                instantiate = false;
            }
        }
        
    }
}
