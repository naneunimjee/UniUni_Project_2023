using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    public int nextMove;
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        Invoke("Think", 3);
    }

    void FixedUpdate()
    {
        //이동
        rigid.velocity = new Vector2(nextMove * 2, rigid.velocity.y);

        //낭떠러지 감지
        Vector2 front = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y - 1);
        Debug.DrawRay(front, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(front, Vector3.down, 0.5f, LayerMask.GetMask("Platform"));

        if (rayHit.collider == null)
        {
            Debug.Log("조심해~");
            Turn();
        }
    }

    public void Think()
    {
        nextMove = Random.Range(-1, 2); //이동방향 설정, 2는 포함되지 않음
        Invoke("Think", 3);
    }


    void Turn()
    {
        nextMove *= -1;
        CancelInvoke();
        Invoke("Think", 2);
    }

    public void OnDamaged()
    {
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //flipY
        spriteRenderer.flipY = true;
        //Color Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        //BoxCollider Enabled
        boxCollider.enabled = false;
        //DeActive Delay
        Invoke("DeActive", 3);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }

}