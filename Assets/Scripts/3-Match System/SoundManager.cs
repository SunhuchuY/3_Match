using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField]
    private AudioClip backgroundSound, gameoverSound;

    private AudioSource audioSource;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // GetComponent
        audioSource = GetComponent<AudioSource>();  
    }

    public void AudioPlay(GameStateType gameStateType)
    {
        switch (gameStateType)
        {
            case GameStateType.GameOver:
                audioSource.clip = gameoverSound;
                break;

            case GameStateType.GamerPause:
                break;

            case GameStateType.GameStart:
                audioSource.clip = backgroundSound;
                break;  
        }

        audioSource?.Play();
    }
}
