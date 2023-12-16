using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    BoxCollider2D boxcollider;
    public GameObject finish;
    public GameObject[] TeleportPos;
    public Player1_Move player1;
    public Player2_Move player2;
    public AudioManager audioManager;

    void Awake()
    {
        boxcollider = GetComponent<BoxCollider2D>();
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        int index = -1;
        Transform collisionPos = null;

        if (collision.gameObject.tag == "Teleport")
        {
            for (int i = 0; i < TeleportPos.Length; i++)
            {
                if (collision.gameObject.name == TeleportPos[i].name)
                {
                    collisionPos = TeleportPos[i].transform;
                    index = i;
                    break;
                }

            }
            audioManager.PlaySound("Teleport");
            if (TeleportPos.Length > 4)
                MoveToInPortalMap(collisionPos, index);
            else
                MoveToInSCKMap(collisionPos, index);
        }
    }

    void MoveToInPortalMap(Transform Pos, int i)
    {
        Vector2 FirstBox = new Vector2(TeleportPos[0].transform.position.x - 1, TeleportPos[0].transform.position.y - 2);
        Vector2 SecondBox = new Vector2((TeleportPos[1].transform.position.x + TeleportPos[2].transform.position.x) / 2,
                                        TeleportPos[1].transform.position.y - 2);
        Vector2 ThirdBox = new Vector2((TeleportPos[3].transform.position.x + TeleportPos[4].transform.position.x) / 2,
                                        TeleportPos[3].transform.position.y - 2);
        Vector2 FourthBox = new Vector2(finish.transform.position.x + 2, finish.transform.position.y + 2);
        Vector2 FifthBox = new Vector2(TeleportPos[5].transform.position.x, TeleportPos[5].transform.position.y - 2);
        Vector2 SixthBox = new Vector2((TeleportPos[6].transform.position.x + TeleportPos[7].transform.position.x) / 2,
                                        TeleportPos[6].transform.position.y - 2);
        Vector2 SeventhBox = new Vector2(TeleportPos[8].transform.position.x + 1, TeleportPos[8].transform.position.y - 2);
        Vector2 EightBox = new Vector2((TeleportPos[9].transform.position.x + TeleportPos[10].transform.position.x) / 2,
                                        TeleportPos[9].transform.position.y - 2);
        switch (i)
        {
            case 0:
                gameObject.transform.position = ThirdBox;

                break;
            case 1:
                gameObject.transform.position = FirstBox;
                break;
            case 2:
                gameObject.transform.position = SixthBox;
                break;
            case 3:
                gameObject.transform.position = SecondBox;
                break;
            case 4:
                gameObject.transform.position = FifthBox;
                break;
            case 5:
                gameObject.transform.position = FourthBox;
                break;
            case 6:
                gameObject.transform.position = FourthBox;
                break;
            case 7:
                gameObject.transform.position = SeventhBox;
                break;
            case 8:
                gameObject.transform.position = EightBox;
                break;
            case 9:
                gameObject.transform.position = SixthBox;
                break;
            case 10:
                gameObject.transform.position = SecondBox;
                break;
        }
        player1.maxPosition = player2.maxPosition = 0;
    }

    void MoveToInSCKMap(Transform Pos, int i)
    {
        switch(i)
        {
            case 0 :
                gameObject.transform.position =
                    new Vector2(TeleportPos[3].transform.position.x - 3, TeleportPos[3].transform.position.y);
                break;
            case 1 :
                gameObject.transform.position =
                    new Vector2(TeleportPos[2].transform.position.x + 3, TeleportPos[2].transform.position.y);
                break;
            case 2 :
                gameObject.transform.position =
                    new Vector2(TeleportPos[1].transform.position.x - 3, TeleportPos[1].transform.position.y);
                break;
            case 3 :
                gameObject.transform.position =
                    new Vector2(TeleportPos[0].transform.position.x + 3, TeleportPos[0].transform.position.y);
                break;
        }
        player1.maxPosition = player2.maxPosition = 0;
    }
}
