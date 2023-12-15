using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    SpriteRenderer spriteRenderer; //플레이어 방향 전환

    public GameManager gameManager;
    public AudioManager audioManager;
    public float jumpPower;
    public float maxSpeed;
    public float maxPosition; //낙하데미지 최대 위치
    public bool isfall; //낙하 여부 확인
    public bool isclear; //클리어 지점 도착 여부 확인용 
    public bool isLadder;
    private float ver; //사다리를 오를 때, w = 1, s = -1를 저장해두기 위한 변수

    void Awake()
    {
        isclear = false;
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    void Start()
    {
        maxPosition = transform.position.y;
        isfall = false;
    }

    void Update()
    {
        //점프 구현, Player 1은 wasd로 이동, 무한점프 방지
        if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("P1_isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("P1_isJumping", true);
            audioManager.PlaySound("Jump");
        }
        
        //높은 곳에서 떨어질때 모션 구현
        if (rigid.velocity.y<0)
        {
            animator.SetBool("P1_isJumping", true);
        }

        //미끄러짐 방지
        if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.7f, rigid.velocity.y);

        //방향 전환
        if (Input.GetKey(KeyCode.A))
        {
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            spriteRenderer.flipX = false;
        }

        //걷는 애니메이션 작동
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            animator.SetBool("P1_isWalking", false);
        }
        else
        {
            animator.SetBool("P1_isWalking", true);
        }

        //낙하데미지 구현
        if (!animator.GetBool("P1_isJumping")) //땅에 있을 때
        {
            if (isfall == true)
            {
                OnDamaged(rigid.transform.position);
                maxPosition = transform.position.y;
                isfall = false;
            }
            if (rigid.velocity.y == 0)
            {
                maxPosition = transform.position.y;
            }
        }

        if (maxPosition > transform.position.y && Math.Abs(maxPosition - transform.position.y) > 10)
        {
            isfall = true;
        }

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


        if (isLadder) //사다리를 타고 있을 때
        {
            animator.SetBool("P1_onLadder", true);
            float ver = Input.GetAxisRaw("VerticalWASD");
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, ver * maxSpeed);
            if (Mathf.Approximately(rigid.velocity.y, 0))
            {
                animator.SetBool("P1_LadderStop", true);
            }
            else
                animator.SetBool("P1_LadderStop", false);
        }
        else if (!isLadder) //사다리에서 나왔을 때
        {
            animator.SetBool("P1_onLadder", false);
            animator.SetBool("P1_LadderStop", false);
            rigid.gravityScale = 4;
        }

            //점프 착지 확인용 레이캐스트 구현
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 3, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)    //플랫폼이 레이캐스트에 닿으면 (땅에 인접하면)
            {
                if (rayHit.distance < 1)
                {
                    animator.SetBool("P1_isJumping", false);
                }
            }
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
            else
            {
                //몬스터의 머리를 밟은 게 아니면 데미지 받음
                OnDamaged(collision.transform.position);
            }
        }

        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "P1_Obstacle")
        {
            //장애물, P1_장애물과 태그됐을 때 데미지 받음
            OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        //사다리
        if (collision.CompareTag("Ladder"))
            isLadder = true;

        //P1 Finish에 닿았을 때 클리어 여부 확인
        if (collision.CompareTag("P1_Finish")) 
        {
            Debug.Log("P1 도착");
            isclear = true;
        }

        //아이템 획득
        if (collision.CompareTag("Item"))
        {
            gameManager.GetItem++;
            collision.gameObject.SetActive(false);
            gameManager.isGetitem();
            audioManager.PlaySound("Item");
        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {
        //사다리
        if (collision.CompareTag("Ladder"))
            isLadder = false;

        //P1 Finish에 벗어났을 때 클리어 여부 확인
        if (collision.CompareTag("P1_Finish"))
        {
            Debug.Log("P1 이탈");
            isclear = false;
        }
    }

    void OnDamaged(Vector2 targetPos)
    {
        //체력 감소
        gameManager.P1_HealthDown();

        //무적상태 레이어로 변경
        gameObject.layer = 8;

        //무적상태 변환시 투명도 조절
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //팅겨나가는 방향 조절
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 7, ForceMode2D.Impulse);

        // 피격 애니메이션
        animator.SetTrigger("DoDamaged");

        //Damaged Sound
        audioManager.PlaySound("Damaged");

        //무적상태는 3초만 유지
        Invoke("OffDamaged", 3);
    }

    void OffDamaged() //무적상태 해제
    {
        gameObject.layer = 6;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    void OnAttack(Transform enemy)
    {
        //EnemyAttack
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);
        audioManager.PlaySound("Attack");
    }

    public void OnDie()
    {
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //flipY
        spriteRenderer.flipY = true;
        //Color Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        //BoxCollider Enabled
        capsuleCollider.enabled = false;
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}