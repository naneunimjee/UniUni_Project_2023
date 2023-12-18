using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2_Move : MonoBehaviour
{
    Rigidbody2D rigid;
    CapsuleCollider2D capsuleCollider;
    Animator animator;
    SpriteRenderer spriteRenderer; //플레이어 방향 전환

    public GameManager gameManager;
    public AudioManager audioManager;
    public float jumpPower;
    public float maxSpeed;
    public float maxPosition; //낙하데미지 최대 위치
    public bool isfall; //낙하 여부 확인
    public bool isclear; //스테이지 이동 여부 확인
    public bool isLadder;
    private float ver; //사다리를 오를 때, w = 1, s = -1를 저장해두기 위한 변수
    public bool isTouchingPlatform;
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

    void Update() //점프 구현, 무한점프 방지, Player 2는 방향키로 이동
    {        
        if (Input.GetKeyDown(KeyCode.UpArrow) && !animator.GetBool("P2_isJumping") && !animator.GetBool("P2_onLadder"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            animator.SetBool("P2_isJumping", true);
            audioManager.PlaySound("Jump");
        }

        //높은 곳에서 떨어질때 모션 구현
        if (rigid.velocity.y < 0)
        {
            animator.SetBool("P2_isJumping", true);
        }

        //미끄러짐 방지
        if (Input.GetButtonUp("Horizontal"))
        {
            rigid.velocity = new Vector2(rigid.velocity.normalized.x * 0.7f, rigid.velocity.y);
        }

        //방향 전환//이거 Horizontal로 바꿀지 생각해봐야함
        if (Input.GetKey(KeyCode.LeftArrow)) 
        {
            spriteRenderer.flipX = true;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            spriteRenderer.flipX = false;
        }

        //걷는 애니메이션 작동
        if (Mathf.Abs(rigid.velocity.x) < 0.3)
        {
            animator.SetBool("P2_isWalking", false);
        }
        else
        {
            animator.SetBool("P2_isWalking", true);
        }

        //낙하데미지 구현
        if (!animator.GetBool("P2_isJumping")) //땅에 있을 때
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

        // 내리막 내려갈 때
        if (isTouchingPlatform && rigid.velocity.y < 0)
        {
            animator.SetBool("P2_isJumping", false);
        }
    }

    void FixedUpdate() 
    {
        //좌우 이동 구현, Player 2는 방향키로 이동
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        if (rigid.velocity.x > maxSpeed) //오른쪽 최대속도 설정
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);

        else if (rigid.velocity.x < maxSpeed * (-1)) //왼쪽 최대속도 설정
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //점프 착지 확인용 레이캐스트 구현
        if (rigid.velocity.y < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayhit = Physics2D.Raycast(rigid.position, Vector3.down, 5, LayerMask.GetMask("Platform"));
            if (rayhit.collider != null)
            {
                if (rayhit.distance < 1)
                    animator.SetBool("P2_isJumping", false);
            }
        }

        if (isLadder) //사다리를 타고 있을 때
        {
            animator.SetBool("P2_onLadder", true);
            float ver = Input.GetAxisRaw("Vertical");
            rigid.gravityScale = 0;
            rigid.velocity = new Vector2(rigid.velocity.x, ver * maxSpeed);
            if (rigid.velocity.y == 0)
            {
                animator.SetBool("P2_LadderStop", true);
            }
            else
                animator.SetBool("P2_LadderStop", false);
        }
        else if (!isLadder) //사다리에서 나왔을 때
        {
            animator.SetBool("P2_onLadder", false);
            animator.SetBool("P2_LadderStop", false);
            rigid.gravityScale = 4;
        }
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
            else
            {
                //몬스터의 머리를 밟은 게 아니면 데미지 받음
                OnDamaged(collision.transform.position);
            }
        }

        if (collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "P2_Obstacle")
        {
            //장애물, P2_장애물과 태그됐을 때 데미지 받음
            OnDamaged(collision.transform.position);
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            isTouchingPlatform = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isTouchingPlatform = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
            isLadder = true;

        if (collision.CompareTag("P2_Finish")) //P2 Finish에 닿았을 때 
        {
            Debug.Log("P2 도착");
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

        if (collision.CompareTag("P2_Obstacle"))
        {
            //장애물, P2_장애물과 태그됐을 때 데미지 받음
            OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Ladder"))
            isLadder = false;

        if (collision.CompareTag("P2_Finish")) //P2 Finish에 떨어졌을 때
        {
            Debug.Log("P2 이탈");
            isclear = false;
        }
    }


    void OnDamaged(Vector2 targetPos)
    {
        //체력 감소
        gameManager.P2_HealthDown();

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
        gameObject.layer = 7;
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
        //capsuleCollider Enabled
        capsuleCollider.enabled = false;
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }
}