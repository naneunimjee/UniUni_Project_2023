using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    public float jumpPower;
    public float maxSpeed;
    public bool isLadder;
    private float ver; //사다리를 오를 때, w = 1, s = -1를 저장해두기 위한 변수

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

        //사다리 타고 올라가기
        else if (isLadder)
        {   
            float ver = Input.GetAxisRaw("VerticalWASD");
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, ver * maxSpeed);
        }

        else if (!isLadder) //사다리에서 나왔을 때
            rigid.gravityScale = 4;
        

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

    void OnTriggerEnter2D(Collider2D collision)
    {
        //사다리
        if (collision.CompareTag("Ladder"))
            isLadder = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
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