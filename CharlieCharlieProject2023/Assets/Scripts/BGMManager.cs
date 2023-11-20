using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public AudioSource BGM;
    private void Awake()
    {
        var BGMManagers = FindObjectsOfType<BGMManager>().Length;
        if (BGMManagers == 1) //avoid duplication play 
        {
            DontDestroyOnLoad(gameObject); //DontDestroyOnLoad = Don't stop the bgm even when the scene changed
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        BGM.Play();
    }
}