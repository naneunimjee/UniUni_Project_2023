using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHit : MonoBehaviour
{

    Animator anim;
    public MovingPlatform movingPlatform;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 frontVec = new Vector2(transform.position.x + 0.3f, transform.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D frontHitButton = Physics2D.Raycast(frontVec, Vector2.up, 1, LayerMask.GetMask("Player"));

        Vector2 backVec = new Vector2(transform.position.x - 0.3f, transform.position.y);
        Debug.DrawRay(backVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D backHitButton = Physics2D.Raycast(backVec, Vector2.up, 1, LayerMask.GetMask("Player"));

        if ((frontHitButton.collider != null || backHitButton.collider != null) && (frontHitButton.collider.CompareTag("Player") || backHitButton.collider.CompareTag("Player")))
        {
            anim.SetBool("ButtonHit", true);

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
}
