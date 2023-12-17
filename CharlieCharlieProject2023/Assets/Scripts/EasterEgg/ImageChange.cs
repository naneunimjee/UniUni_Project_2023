using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ImageChange : MonoBehaviour
{   
    Image image;
    GameObject gameOb;
    public int itemCnt;
    public Sprite[] spriteChanged;

    void Start()
    {   
        image = GetComponent<Image>();
        gameOb = GameObject.Find("ItemBtnManager"); // 연결할 GameObject의 이름을 지정
        StartCoroutine(GetItemCount());
    }

    IEnumerator GetItemCount()
    {
        yield return new WaitForEndOfFrame();
        itemCnt = gameOb.GetComponent<ItemBtnManager>().count;
        spriteChanged = gameOb.GetComponent<ItemBtnManager>().sprites;

        // Coroutine이 완료된 후에 UpdateImage 메서드 호출
        UpdateImage();
    }

    public void UpdateImage()
    {
        if (itemCnt % 2 == 1)
        {
            image.sprite = spriteChanged[1];
        }
    }
}