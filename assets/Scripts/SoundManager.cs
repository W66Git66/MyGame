using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public static SoundManager instance;
    public AudioClip cardEliminationSound;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {

        audioSource = GetComponent<AudioSource>();
        PlaySound();
    }

    void PlaySound()
    {
        if(audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("û������");
        }
    }
    public void PlayCardEliminationSound()
    {
        // ������Ч
        audioSource.PlayOneShot(cardEliminationSound);
    }
}
