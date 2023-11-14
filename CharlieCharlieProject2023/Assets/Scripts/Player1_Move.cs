using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    public float jumpPower;
    public float maxSpeed;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //점프 구현, Player 1은 wasd로 이동
        if (Input.GetKeyDown(KeyCode.W))
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);

        //미끄러짐 방지
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.7f, rigid.velocity.y);
    }

    void FixedUpdate()
    {
        //좌우 이동 구현, Player 1은 wasd로 이동
        if (Input.GetKey(KeyCode.A))
        {
            rigid.AddForce(Vector2.left, ForceMode2D.Impulse);

            if (rigid.velocity.x < maxSpeed * (1))
                SetMaxSpeed(KeyCode.A);
        }

        else if (Input.GetKey(KeyCode.D))
        {
            rigid.AddForce(Vector2.right, ForceMode2D.Impulse);

            if (rigid.velocity.x > maxSpeed)
                SetMaxSpeed(KeyCode.D);
        }

    }

    void SetMaxSpeed(KeyCode key) //이동방향에 따른 최대속도 설정
    {
        if (key == KeyCode.A)
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);
        else if (key == KeyCode.D)
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
    }


void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy")
        {
            //Attack
            if ((collision.transform.position.y < transform.position.y) &&
									 rigid.velocity.y < 0)
            {
                //Enemy Die
                OnAttack(collision.transform);
            }
        }

    }

    void OnAttack(Transform enemy)
    {
        //EnemyAttack
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
    }
}