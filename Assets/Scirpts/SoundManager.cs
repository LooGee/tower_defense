using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip Standard;
    public AudioClip ExplodeSound;

    AudioSource audioSource;

    public static SoundManager instance;
    // Start is called before the first frame update

    private void Awake()
    {
        if (SoundManager.instance == null)
        {
            SoundManager.instance = this;
        }
    }
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayMissile()
    {
        audioSource.PlayOneShot(ExplodeSound);
    }
    public void PlayStadard()
    {
        audioSource.PlayOneShot(Standard);
    }
  
}
    
