using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioClip _shutterSound;
    private static AudioClip _shutterSoundEcho;
    private static AudioClip _deathSound;
    private static AudioClip _flashRechargeSound;

    private static AudioSource _audioSource;
    void Start()
    {
        _shutterSound = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1");
        _shutterSoundEcho = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1Echo");
        _deathSound = Resources.Load<AudioClip>("Audio/Sounds/Death1");
        _flashRechargeSound = Resources.Load<AudioClip>("Audio/Sounds/FlashRecharge");
        
        _audioSource = GetComponent<AudioSource>();
    }
    
    public static void PlaySoundEffect(string soundEffectName)
    {
        switch (soundEffectName)
        {
            case "Shutter":
                _audioSource.PlayOneShot(_shutterSound);
                _audioSource.PlayOneShot(_flashRechargeSound);
                break;            
            case "ShutterEcho":
                _audioSource.PlayOneShot(_shutterSoundEcho);
                _audioSource.PlayOneShot(_flashRechargeSound);
                break;
            case "Death":
                _audioSource.PlayOneShot(_deathSound);
                break;
            case "FlashRecharge":
                
                break;
            
        }
    }
}
