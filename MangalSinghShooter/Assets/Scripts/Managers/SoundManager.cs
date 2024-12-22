using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Background Music")]
    [SerializeField] private AudioClip bgmSfx;
    [SerializeField] private AudioSource bgmAudioSource;

    [Header("Zombie Sounds")]
    // [SerializeField] private AudioClip[] zombieHitSfxArray;
    [SerializeField] private AudioClip zombieDieSfx;
    [SerializeField] private AudioSource zombieAudioSource;

    [Header("Shooting Sounds")]
    [SerializeField] private AudioClip shootingSfx;
    private AudioSource shootingAudioSource;

    public static SoundManager Instance;

    private const string ZOMBIE = "Zombie";
    private const string PLAYER = "Player";

    private void Awake() {

        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }

        bgmAudioSource = gameObject.GetComponent<AudioSource>();
        shootingAudioSource = GameObject.FindWithTag(PLAYER).AddComponent<AudioSource>();
        
    }

    private void Start() {
        PlayBGM();
    }

    private void OnEnable() {
        PlayerShooting.OnPlayerShoot += PlayPlayerShootSound;
        PlayerHealth.OnPlayerDie += StopBGM;
        ZombieSpawner.OnZombieInstantiated += AssignZombieAudioSource;
    }

    private void OnDisable() {
        PlayerShooting.OnPlayerShoot -= PlayPlayerShootSound;
        PlayerHealth.OnPlayerDie -= StopBGM;
        ZombieSpawner.OnZombieInstantiated -= AssignZombieAudioSource;
    }


    private void PlayBGM() {
        if(bgmSfx != null) {
            bgmAudioSource.mute = false;
            bgmAudioSource.loop = true;
            bgmAudioSource.clip = bgmSfx;
            bgmAudioSource.playOnAwake = true;
            bgmAudioSource.volume = 1f;
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
        if(zombieDieSfx != null) {

            zombieAudioSource.priority = 129;
            zombieAudioSource.PlayOneShot(zombieDieSfx, 0.5f);
        } else {
            Debug.LogError("ZombieDieSfx is missing");
        }
    }

    private void PlayPlayerShootSound() {
        if(shootingSfx != null) {
            shootingAudioSource.priority = 128;
            shootingAudioSource.PlayOneShot(shootingSfx, 0.5f);
        } else {
            Debug.LogError("ShootingSfx is missing");
        }
    }

    private void AssignZombieAudioSource() {
        zombieAudioSource = GameObject.FindWithTag(ZOMBIE).AddComponent<AudioSource>();
    }
}
