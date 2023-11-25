using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    public int stageIndex;
    public int P1_HP;
    public int P2_HP;
    public int GetItem;
    public Player1_Move Player1;
    public Player2_Move Player2;
<<<<<<< Updated upstream
    public GameObject[] Stages;
=======
    public UIManager UImanagers;
>>>>>>> Stashed changes

    //UI ���� ���� ����
    public Image[] UIP1_HP;
    public Image[] UIP2_HP;
    public Image[] UIItem;
    public TextMeshProUGUI UIStage;
    public GameObject UIRestartBtn;
    public TextMeshProUGUI ItemMSG;

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            DontDestroyOnLoad(gameObject);
        }

        if (Player1.isclear && Player2.isclear) //P1�� P2�� ���� �ǴϽ� ���� ����
        {
            if (GetItem != 3) //�������� �� �� �Ծ��� �� �ȳ� �˾�â, �� �Ծ����� ���� ��������
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
        {//������ �ǴϽ� ������ ����� ������ ��ȹ�� �ȳ�â ����
            ItemMSG.text = "";
        }
    }

    public void NextStage()
    {
        //���� �������� �̵�
        if(stageIndex < Stages.Length-1)
        {
<<<<<<< Updated upstream
            PlayerReposition();
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);

            UIStage.text = "STAGE " + (stageIndex+1);
=======
            if (stageIndex % 2 == 1)
            {
                if (P1_HP < 3)
                {
                    UIP1_HP[P1_HP].color = new Color(1, 1, 1, 1);
                    P1_HP++;
                }
                if (P2_HP < 3)
                {
                    UIP2_HP[P2_HP].color = new Color(1, 1, 1, 1);
                    P2_HP++;
                }
            }
            stageIndex++;
            Playerreposition();
            SceneManager.LoadScene(stageIndex);
>>>>>>> Stashed changes
        }
        else //���� Ŭ����
        {
            //�ð� ����
            Time.timeScale = 0;
            //��� ȭ�� UI
            Debug.Log("���� Ŭ����!");
            //����� ��ư UI
            TextMeshProUGUI btnText = UIRestartBtn.GetComponentInChildren<TextMeshProUGUI>();
            btnText.text = "Clear!";
            UIRestartBtn.SetActive(true);
        }
    }

    public void P1_HealthDown() //�÷��̾� 1 ü�� ��ġ �� UI ����, ������ dead �ִϸ��̼ǰ� restart ��ư ȣ��
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

    public void P2_HealthDown() //�÷��̾� 2 ü�� ��ġ �� UI ����, ������ dead �ִϸ��̼ǰ� restart ��ư ȣ��
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

    public void isGetitem() //������ UI ����
    {
        UIItem[GetItem-1].color = new Color(1, 1, 1, 1);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Dead()
    {
        Player1.OnDie();
        Player2.OnDie();
        UIRestartBtn.SetActive(true);
    }

<<<<<<< Updated upstream
    void PlayerReposition()
    {
        Player1.transform.position = new Vector3(0,0,-1);
        Player1.VelocityZero();
        Player2.transform.position = new Vector3(0, 0, -1);
        Player2.VelocityZero();
    }

=======
    public void Playerreposition()
    {
    switch (stageIndex.ToString())
        {
            case "3":
                Player1.transform.position = new Vector3(21.36f, -9.76f, 0);
                Player2.transform.position = new Vector3(11.09f, 9.1f, 0);
                break;
            case "4":
                break;

        }
    }
>>>>>>> Stashed changes
}
