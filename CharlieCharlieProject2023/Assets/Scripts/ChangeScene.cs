using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void ChangeScenefunc()
    {
        switch (this.gameObject.name)
        {
            case "StartBtn":
                SceneManager.LoadScene(2);
                break;
            case "InfoBtn": //InfoScene number is 1
                SceneManager.LoadScene("InfoScene");
                break;
            case "BackBtn": //StartMenu number is 0
                SceneManager.LoadScene("StartMenu");
                break;
        }
    }

    public void ItemChange()
    {
        public int ItemIndex;
    }
}
