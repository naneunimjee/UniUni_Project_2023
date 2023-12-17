using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpriteChange : MonoBehaviour
{   
    SpriteRenderer spriteRenderer;
    GameObject gameOb;
    public int itemCnt;
    public Sprite[] spriteChanged;

    void Start()
    {   
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameOb = GameObject.Find("ItemBtnManager"); // 연결할 GameObject의 이름을 지정
        StartCoroutine(GetItemCount());
    }

    IEnumerator GetItemCount()
    {
        yield return new WaitForEndOfFrame();
        itemCnt = gameOb.GetComponent<ItemBtnManager>().count;
        spriteChanged = gameOb.GetComponent<ItemBtnManager>().sprites;

    }

    public void Update()
    {

        if ( (itemCnt % 2 == 1))
        {
            spriteRenderer.sprite = spriteChanged[1];
        }
    }
}
