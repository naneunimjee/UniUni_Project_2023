using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    SpriteRenderer spriteRenderer; //플레이어 방향 전환
    Player2_Move Player2;

    public GameManager gameManager;
    public float jumpPower;
    public float maxSpeed;
    public float maxPosition; //낙하데미지 최대 위치
    public bool isclear;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        //점프 구현 및 무한점프 방지, Player 1은 wasd로 이동
        if (Input.GetKeyDown(KeyCode.W) && !animator.GetBool("P1_isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("P1_isJumping", true);
        }

        //낙하데미지 구현
        if (!animator.GetBool("P1_isJumping")) //땅에 있을 때
        {
            if (maxPosition - transform.position.y > 10)
            {
                OnDamaged(rigid.transform.position);
                maxPosition = 0;
            }
        }
        else
        {
            if(rigid.velocity.y < 0 && maxPosition < transform.position.y){
                maxPosition = transform.position.y;
            }
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

        //점프 착지 확인용 레이캐스트 구현
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null) //플랫폼이 레이캐스트에 닿으면 (땅에 인접하면)
            {
                if (rayHit.distance < 0.5f)
                {
                    animator.SetBool("P1_isJumping", false);
                }
            }
        }

        //도착점 벗어났는지 여부 확인용 레이캐스트 구현
        if (isclear)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 0, 1));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 0, LayerMask.GetMask("Finish"));
            if (rayHit.collider == null) //Finish 지점을 벗어나면
            {
                isclear = false;
                Debug.Log("P1 도착점 이탈");
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
            if ((collision.transform.position.y < transform.position.y) && rigid.velocity.y < 0)
            {
                //Enemy Die
                OnAttack(collision.transform);
            }
            else {
                //몬스터의 머리를 밟은 게 아니면 데미지 받음
                OnDamaged(collision.transform.position);
            }

        }

        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "P1_obstacle")
        {
            //장애물, P1_장애물과 태그됐을 때 데미지 받음
            OnDamaged(collision.transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "P1_Finish") //P1 Finish에 닿았을 때 
        {
            Debug.Log("P1 도착");
            isclear = true;
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        //체력 감소
        gameManager.P1_HealthDown();

        //무적상태 레이어로 변경
        gameObject.layer = 12;

        //무적상태 변환시 투명도 조절
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);

        //팅겨나가는 방향 조절
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1)*7, ForceMode2D.Impulse);

        // 피격 애니메이션
        animator.SetTrigger("DoDamaged");

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

        //몬스터 밟고 반발력 발생
        rigid.AddForce(Vector2.up * 3, ForceMode2D.Impulse);

        //몬스터 처치 점수
        gameManager.stagePoint += 100;
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
        rigid.velocity=Vector2.zero;
    }

}