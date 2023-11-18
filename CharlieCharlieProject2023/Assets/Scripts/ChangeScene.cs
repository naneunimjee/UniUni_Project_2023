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
                SceneManager.LoadScene("SkysMap");
                break;
            case "InfoBtn":
                SceneManager.LoadScene("InfoScene");
                break;
            case "BackBtn":
                SceneManager.LoadScene("StartMenu");
                break;
        }
    }
}
