using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EasterEgg_Charlie : MonoBehaviour
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

    public void OnButtonClick()
    {
        CharlieBtnClick();
    }

    public void CharlieBtnClick()
    {
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        audioManager.PlaySound("Jump");
    }
    
}
