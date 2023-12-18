using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip ADItem;
    public AudioClip ADAttack;
    public AudioClip ADUIBtnClk;
    public AudioClip ADGameFail;
    public AudioClip ADGameClear;
    public AudioClip ADJump;
    public AudioClip ADTeleport;
    public AudioClip ADStageClear;
    public AudioClip ADDamaged;
    public AudioClip ADPushBtn;

    public AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
     
    public void PlaySound(string action)
    {
        GameObject go = new GameObject("AD"+action);
        audioSource = go.AddComponent<AudioSource>();
        DontDestroyOnLoad(go);

        switch (action)
        {
            case "Item":
                audioSource.clip = ADItem; break;
            case "Attack":
                audioSource.clip = ADAttack; break;
            case "UIBtnClk":
                audioSource.clip = ADUIBtnClk; break;
            case "GameFail":
                audioSource.clip = ADGameFail; break;
            case "GameClear":
                audioSource.clip = ADGameClear; break;
            case "Jump":
                audioSource.clip = ADJump; break;
            case "Teleport":
                audioSource.clip= ADTeleport; break;
            case "StageClear":
                audioSource.clip = ADStageClear; break;
            case "Damaged":
                audioSource.clip = ADDamaged; break;
            case "PushBtn":
                audioSource.clip = ADPushBtn; break;
        }
        audioSource.Play();
        Destroy(go, audioSource.clip.length);
    }
}
