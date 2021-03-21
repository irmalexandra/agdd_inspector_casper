using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioClip _shutterSound;
    private static AudioClip _shutterSoundEcho;
    private static AudioClip _deathSound;

    private static AudioSource _audioSource;
    void Start()
    {
        _shutterSound = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1");
        _shutterSoundEcho = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1Echo");
        _deathSound = Resources.Load<AudioClip>("Audio/Sounds/Death1");
        _audioSource = GetComponent<AudioSource>();
    }
    
    public static void PlaySoundEffect(string soundEffectName)
    {
        switch (soundEffectName)
        {
            case "Shutter":
                _audioSource.PlayOneShot(_shutterSound);
                break;            
            case "ShutterEcho":
                _audioSource.PlayOneShot(_shutterSoundEcho);
                break;
            case "Death":
                _audioSource.PlayOneShot(_deathSound);
                break;
            
        }
    }
}
