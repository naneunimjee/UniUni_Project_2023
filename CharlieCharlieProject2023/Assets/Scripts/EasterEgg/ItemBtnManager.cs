using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ItemBtnManager : MonoBehaviour
{   
    public Sprite[] sprites;
    public AudioManager audioManager;
    public int count = 0;


    public void OnClickButton()
    {
        count += 1;
        ItemBtnClick(count);
    }

    public void ItemBtnClick(int cnt)
    {
        Image image = GameObject.Find("ItemBtn").GetComponent<Image>();

        if ( (cnt%2) == 1)
        {   
            audioManager.PlaySound("Item");
            image.sprite = sprites[1];
        }

        else if ( (cnt%2) == 0)
        {   
            audioManager.PlaySound("Item");
            image.sprite = sprites[0];
        }
    }


    public void Awake()
    {
        DontDestroyOnLoad(gameObject);
        CheckSceneOrder();
        
    }

    private int lastSceneIndex = -1; // 이전 씬의 인덱스를 저장하는 변수

    
    void Update()
    {
        // 씬 전환 시마다 현재 씬의 빌드 인덱스를 확인하고 조건 검사
        if (SceneManager.GetActiveScene().buildIndex != lastSceneIndex)
        {
            CheckSceneOrder();
        }
    }

    void CheckSceneOrder()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        // 이전 씬의 인덱스가 -1이거나, 현재 씬의 인덱스가 0일 때만 조건을 만족시킴
        if (lastSceneIndex == 1 && currentSceneIndex == 0)
        {
            Destroy(gameObject);
        }

        // 현재 씬의 인덱스를 이전 씬의 인덱스로 업데이트
        lastSceneIndex = currentSceneIndex;
    }
}

