using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static AudioClip _shutterSound;
    private static AudioClip _shutterSoundEcho;
    private static AudioClip _deathSound;
    private static AudioClip _flashRechargeSound;
    private static AudioClip _ghostAware;
    private static AudioClip _heartbeat;
    private static AudioClip _scream;
    private static AudioClip _keyPickup;

    private static AudioSource _audioSource;
    void Start()
    {
        _shutterSound = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1");
        _shutterSoundEcho = Resources.Load<AudioClip>("Audio/Sounds/CameraShutter1Echo");
        _deathSound = Resources.Load<AudioClip>("Audio/Sounds/Death1");
        _flashRechargeSound = Resources.Load<AudioClip>("Audio/Sounds/FlashRecharge");
        _ghostAware = Resources.Load<AudioClip>("Audio/Sounds/ToasterGhostChasing");
        _heartbeat = Resources.Load<AudioClip>("Audio/Sounds/SingleHeartbeat");
        _scream = Resources.Load<AudioClip>("Audio/Sounds/DemonicScream");
        _keyPickup = Resources.Load<AudioClip>("Audio/Sounds/KeyPickup");

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
            case "Heartbeat":
                if (!_audioSource.isPlaying)
                {
                    _audioSource.clip = _heartbeat;
                    _audioSource.Play();
                }
                else
                {
                    _audioSource.Stop();
                }
                break;
            case "Scream":
                _audioSource.PlayOneShot(_scream);
                break;
            case "KeyPickup":
                _audioSource.PlayOneShot(_keyPickup);
                break;
        }
    }


}
