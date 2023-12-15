using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg_Item : MonoBehaviour
{
    public AudioManager audioManager;
    public Sprite[] sprites;
    // index 0 : cherry (or apple) index 1 : another item image
    public int count = 0;


    public void OnButtonClick()
    {
        count = count + 1;
        ItemBtnClick(count);
    }

    public void ItemBtnClick(int cnt)
    {
        Image image = GameObject.Find("ItemBtn").GetComponent<Image>();

        if ((cnt % 2) == 1)
        {
            image.sprite = sprites[1];
            audioManager.PlaySound("Item");
        }
        
        else if ((cnt % 2) == 0)
        {
            image.sprite = sprites[0];
            audioManager.PlaySound("Item");
        }
    }
}
