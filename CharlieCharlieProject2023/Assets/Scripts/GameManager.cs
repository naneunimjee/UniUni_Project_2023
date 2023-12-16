using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public bool IsPause=false;
    public int stageIndex;
    public static int P1_HP;
    public static int P2_HP;
    public int GetItem;
    public Player1_Move Player1;
    public Player2_Move Player2;
    public AudioManager audioManager;

    //UI 관련 변수 생성
    public Image[] UIP1_HP;
    public Image[] UIP2_HP;
    public Image[] UIItem;
    public GameObject UIRestartBtn;
    public GameObject UIClearBtn;
    public TextMeshProUGUI ItemMSG;
    public GameObject EndingMent;

    private void Update()
    {

        if (Player1.isclear && Player2.isclear) //P1과 P2가 전부 피니쉬 라인 도착
        {
            if (GetItem != 3) //아이템을 다 안 먹었을 시 안내 팝업창, 다 먹었으면 다음 스테이지
            {
                ItemMSG.text="You must get all item";
            }
            else
            {
                Player1.isclear = false;
                Player2.isclear = false;
                GetItem = 0;
                NextStage();
            }
        }
        else
        {//누군가 피니쉬 라인을 벗어나면 아이템 미획득 안내창 삭제
            ItemMSG.text = "";
        }

        if (Input.GetKeyDown(KeyCode.Escape) && IsPause == false)
        {
            Time.timeScale = 0;
            UIRestartBtn.SetActive(true);
            IsPause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && IsPause == true)
        {
            Time.timeScale = 1;
            UIRestartBtn.SetActive(false);
            IsPause = false;
        }
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            P1_HP = P2_HP = 3;
        }
        else if (SceneManager.GetActiveScene().buildIndex > 2)
        {
            UIupdate();
        }
    }

    public void NextStage()
    {
        stageIndex = SceneManager.GetActiveScene().buildIndex;

        if(stageIndex < SceneManager.sceneCountInBuildSettings -1)
        {
            if (stageIndex % 2 == 1)
            {
                if (P1_HP < 3)
                {
                    P1_HP++;
                }
                if (P2_HP < 3)
                {
                    P2_HP++;
                }
            }
            stageIndex++;
            audioManager.PlaySound("StageClear");
            SceneManager.LoadScene(stageIndex);
        }
        else //게임 클리어
        {
            //시간 정지
            Time.timeScale = 0;
            //결과 화면 UI
            Debug.Log("게임 클리어!");
            //재시작 버튼 UI
            audioManager.PlaySound("GameClear");

            UIRestartBtn.SetActive(true);
            EndingMent.SetActive(true);
            UIClearBtn.SetActive(true);

        }
    }

    public void P1_HealthDown() //플레이어 1 체력 수치 및 UI 관리, 죽음시 dead 애니메이션과 restart 버튼 호출
    {
        if (P1_HP > 1)
        {
            P1_HP--;
            UIP1_HP[P1_HP].color = new Color(1,1,1,0.2f);
        }
        else
        {
            UIP1_HP[0].color = new Color(1, 1, 1, 0.2f);
            Dead();
        }
    }

    public void P2_HealthDown() //플레이어 2 체력 수치 및 UI 관리, 죽음시 dead 애니메이션과 restart 버튼 호출
    {
        if (P2_HP > 1)
        {
            P2_HP--;
            UIP2_HP[P2_HP].color = new Color(1, 1, 1, 0.2f);
        }
        else
        {
            UIP2_HP[0].color = new Color(1, 1, 1, 0.2f);
            Dead();
        }
    }

    public void isGetitem() //아이템 UI 관리
    {
        UIItem[GetItem-1].color = new Color(1, 1, 1, 1);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        stageIndex = 0;
        SceneManager.LoadScene(stageIndex);
    }

    public void Dead()
    {
        Player1.OnDie();
        Player2.OnDie();
        audioManager.PlaySound("GameFail");
        UIRestartBtn.SetActive(true);
    }

    public void UIupdate()
    {
        if (P1_HP < 2)
            UIP1_HP[1].color = new Color(1, 1, 1, 0.2f);
        if (P1_HP < 3)
            UIP1_HP[2].color = new Color(1, 1, 1, 0.2f);
        if (P2_HP < 2)
            UIP2_HP[1].color = new Color(1, 1, 1, 0.2f);
        if (P2_HP < 3)
            UIP2_HP[2].color = new Color(1, 1, 1, 0.2f);

    }
}
