using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BabaraManager : MonoBehaviour
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
        BabaraClick();
    }

    public void BabaraClick()
    {   
        audioManager.PlaySound("Jump");
        rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
        animator.SetBool("P2_isJumping", true);
    }

}
