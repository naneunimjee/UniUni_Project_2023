using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}