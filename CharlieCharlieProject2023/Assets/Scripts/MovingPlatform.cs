using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPos;  // 시작 위치
    public Transform endPos;    // 끝 위치
    public Transform desPos;    // 도착지의 Transform
    public float speed;         // 발판 속도 조절

    bool isMoving = false;

    void Start()
    {
        transform.position = startPos.position;  // 발판은 startPos
        desPos = endPos;  // 목적지는 endPos

        //buttonAnim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // 발판 일정 포인트로 이동, MoveTowards(현재위치, 목표위치, 속도)
            transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * speed);

            // 발판이 목적지에 도착하면 출발지를 도착지로 변경
            if (Vector2.Distance(transform.position, desPos.position) <= 0.05f)
            {
                if (desPos == endPos)
                    desPos = startPos;
                else
                    desPos = endPos;
            }
        }

    }

    // 발판 움직임
    public void StartMoving()
    {
        isMoving = true;
    }

    // 발판 움직임 멈춤
    public void StopMoving()
    {
        isMoving = false;
    }

    // 발판의 움직임에 따라 플레이어도 움직이게 하기
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // 접촉한 태그가 Player일때
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            // Player를 발판의 자식 태그로 만들기
            collision.transform.SetParent(transform);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 접촉한 태그가 Player일때
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            // Player를 발판의 자식 태그에서 탈출
            collision.transform.SetParent(null);
        }
    }
}
