using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Move : MonoBehaviour
{

    Rigidbody2D rigid;
    Animator animator;
    public float jumpPower;
    public float maxSpeed;
    public bool isLadder;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update() //점프 구현, Player 2는 방향키로 이동
    {
        float j = Input.GetAxisRaw("Vertical");
        if (Input.GetButtonDown("Vertical"))
            rigid.AddForce(Vector2.up * j * jumpPower, ForceMode2D.Impulse);

        //미끄러짐 방지
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.7f, rigid.velocity.y);
        }
    }

    void FixedUpdate() //좌우 이동 구현, Player 2는 방향키로 이동
    {
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //오른쪽 최대속도 설정
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        else if (rigid.velocity.x < maxSpeed * (-1)) //왼쪽 최대속도 설정
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        if (isLadder) //사다리를 타고 있을 때
        {
            float ver = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(rigid.velocity.x , ver * maxSpeed);
        }
        else if (!isLadder) //사다리에서 나왔을 때
            rigid.gravityScale = 4;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            //Attack
            if ((collision.transform.position.y < transform.position.y) && rigid.velocity.y < 0)
            {
                //Enemy Die
                OnAttack(collision.transform);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
            isLadder = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
            isLadder = false;
    }

    void OnAttack(Transform enemy)
    {
        //EnemyAttack
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
    }

}