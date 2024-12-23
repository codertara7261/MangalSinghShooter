using PowerUps;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioClip bgmSfx;
    [SerializeField] private AudioSource bgmAudioSource;

    [Header("Zombie Sounds")]
    [SerializeField] private AudioClip[] zombieHitSfxArray;
    // [SerializeField] private AudioClip zombieDieSfx;
    [SerializeField] private AudioSource zombieAudioSource;

    [Header("Shooting Sounds")]
    [SerializeField] private AudioClip shootingSfx;
    private AudioSource shootingAudioSource;

    [Header("PowerUp Sounds")]
    [SerializeField] private AudioSource powerUpAudioSource;
    [SerializeField] private AudioClip[] powerUpSfxArray;

    public static SoundManager Instance;

    private const string ZOMBIE = "Zombie";
    private const string PLAYER = "Player";

    private void Awake() {

        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            bgmAudioSource = gameObject.GetComponent<AudioSource>();
            
        } else {
            Destroy(gameObject);
            return;
        }
        
    }

    private void Start() {
        PlayBGM();
    }

    private void OnEnable() {
        PlayerShooting.OnPlayerShoot += PlayPlayerShootSound;
        PlayerHealth.OnPlayerDie += StopBGM;
        ZombieSpawner.OnZombieInstantiated += AssignZombieAudioSource;
        PowerUp.OnPowerUpCollected += PlayPowerUpCollected;
        PlayerHealth.OnPlayerInitialized += AssignPlayerAudioSource;

        shootingAudioSource = GameObject.FindWithTag(PLAYER).AddComponent<AudioSource>();
    }

    private void OnDisable() {
        PlayerShooting.OnPlayerShoot -= PlayPlayerShootSound;
        PlayerHealth.OnPlayerDie -= StopBGM;
        ZombieSpawner.OnZombieInstantiated -= AssignZombieAudioSource;
        PowerUp.OnPowerUpCollected -= PlayPowerUpCollected;
    }


    private void PlayBGM() {
        if(bgmSfx != null) {
            bgmAudioSource.mute = false;
            bgmAudioSource.loop = true;
            bgmAudioSource.clip = bgmSfx;
            bgmAudioSource.playOnAwake = true;
            bgmAudioSource.volume = 0.6f;
            bgmAudioSource.Play();
            bgmAudioSource.priority = 0;
        } else {
            Debug.LogError("BGM Sound is missing in Sound Manager");
        }
    }

    private void StopBGM() {
        if(bgmSfx != null) {
            bgmAudioSource.Stop();
        }
    }

    public void PlayZombieDieSound() {
        Debug.Log("zombie die sound");
        if(zombieHitSfxArray != null) {
            int randomIndex = Random.Range(0, 2);
            zombieAudioSource.PlayOneShot(zombieHitSfxArray[randomIndex], 0.5f);
        } else {
            Debug.LogError("ZombieDieSfx is missing");
        }
    }

    private void PlayPlayerShootSound() {
        if(shootingSfx != null && shootingAudioSource != null) {
            shootingAudioSource.PlayOneShot(shootingSfx, 0.3f);
        } else {
            Debug.LogError("ShootingSfx or ShootingAudioSource is missing");
        }
    }

    private void PlayPowerUpCollected(PowerUpType powerUpType) {
        if(powerUpAudioSource != null) {
            switch(powerUpType) {
                case PowerUpType.Speed:
                    powerUpAudioSource.clip = powerUpSfxArray[2];
                    break;
                case PowerUpType.MachineGun:
                    powerUpAudioSource.clip = powerUpSfxArray[1];
                    break;
                case PowerUpType.Health:
                    powerUpAudioSource.clip = powerUpSfxArray[0];
                    break;
            }
            powerUpAudioSource.PlayOneShot(powerUpAudioSource.clip, 0.7f);
        }
    }

    private void AssignZombieAudioSource() {
        zombieAudioSource = GameObject.FindWithTag(ZOMBIE).AddComponent<AudioSource>();
    }

    private void AssignPlayerAudioSource(GameObject player) {
        if(player != null) {
            shootingAudioSource = player.GetComponent<AudioSource>();
            if(shootingAudioSource == null) {
                shootingAudioSource = player.AddComponent<AudioSource>();
            }
        } else {
            Debug.LogError("Player GameObject is null in AssignPlayerAudioSource");
        }
    }
}
