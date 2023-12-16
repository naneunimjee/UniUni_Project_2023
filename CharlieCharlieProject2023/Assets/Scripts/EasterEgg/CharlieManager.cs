using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharlieManager : MonoBehaviour
{
    public AudioManager audioManager;
    public int jumpPower;
    Animator animator;
    Rigidbody2D rigid;

    public void Awake()
    {
        animator = GetComponentInParent<Animator>();
        rigid = GetComponentInParent<Rigidbody2D>();
    }


    public void OnClickButton()
    {
        CharlieClick();
    }

    public void CharlieClick()
    {   
        audioManager.PlaySound("Jump");
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
    }

}
