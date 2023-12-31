using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public AudioManager AudioManager;
    public void ChangeScenefunc()
    {
        AudioManager.PlaySound("UIBtnClk");
        switch (this.gameObject.name)
        {
            case "StartBtn":
                SceneManager.LoadScene(2);
                break;
            case "InfoBtn": //InfoScene number is 1
                SceneManager.LoadScene(1);
                break;
            case "BackBtn": //StartMenu number is 0
                SceneManager.LoadScene(0);
                break;
        }
    }
}
