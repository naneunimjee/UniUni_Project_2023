using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    }

}
