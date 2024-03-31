using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] 
    private List<AudioClip> SE;
    [SerializeField]
    private List<AudioClip> BGM;

    public AudioSource audioSource {  get; private set; }

    public bool IsPlayingBGM { get; private set; } = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySE(int index)
    {
        if (SE != null)
        {
            audioSource.clip = SE[index];
            audioSource.Play();
        }
        
    }

    public void PlayBGM(int index)
    {
        if (BGM != null)
        {
            IsPlayingBGM = true;
            audioSource.clip = BGM[index];
            audioSource.Play();
        }
        
    }

    public void Stop()
    {
        audioSource?.Stop();
    }
}
