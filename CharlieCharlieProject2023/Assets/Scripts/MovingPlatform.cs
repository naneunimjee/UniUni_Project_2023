using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPos;  // ���� ��ġ
    public Transform endPos;    // �� ��ġ
    public Transform desPos;    // �������� Transform

    float speed = 2;         // ���� �ӵ� ����


    bool isMoving = false;

    void Start()
    {
        transform.position = startPos.position;  // ������ startPos
        desPos = endPos;  // �������� endPos
    }

    void FixedUpdate()
    {
        if (isMoving)
        {
            // ���� ���� ����Ʈ�� �̵�, MoveTowards(������ġ, ��ǥ��ġ, �ӵ�)
            transform.position = Vector2.MoveTowards(transform.position, desPos.position, Time.deltaTime * speed);

            // ������ �������� �����ϸ� ������� �������� ����
            if (Vector2.Distance(transform.position, desPos.position) <= 0.05f)
            {
                if (desPos == endPos)
                    desPos = startPos;
                else
                    desPos = endPos;
            }
        }

    }

    // ���� ������
    public void StartMoving()
    {
        isMoving = true;
    }

    // ���� ������ ����
    public void StopMoving()
    {
        isMoving = false;
    }

    // ������ �����ӿ� ���� �÷��̾ �����̰� �ϱ�
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // ������ �±װ� Player�϶�
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            // Player�� ������ �ڽ� �±׷� �����
            collision.transform.SetParent(transform);
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ������ �±װ� Player�϶�
        if (collision.transform.CompareTag("Player1") || collision.transform.CompareTag("Player2"))
        {
            // Player�� ������ �ڽ� �±׿��� Ż��
            collision.transform.SetParent(null);
        }
    }
}
