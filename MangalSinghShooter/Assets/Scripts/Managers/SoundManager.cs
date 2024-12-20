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

    // private const string ZOMBIE = "Zombie";
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
        // zombieAudioSource = GameObject.FindWithTag(ZOMBIE).AddComponent<AudioSource>();
    }

    private void Start() {
        PlayBGM();
        Debug.Log("Control is coming inside thhe SoundManager.cs");
    }

    private void OnEnable() {
        PlayerShooting.OnPlayerShoot += PlayPlayerShootSound;
        ZombieAI.OnZombieDie += PlayZombieDieSound;
    }

    private void OnDisable() {
        PlayerShooting.OnPlayerShoot -= PlayPlayerShootSound;
        ZombieAI.OnZombieDie -= PlayZombieDieSound;
    }


    private void PlayBGM() {
        if(bgmSfx != null) {
            bgmAudioSource.mute = false;
            bgmAudioSource.loop = true;
            bgmAudioSource.clip = bgmSfx;
            bgmAudioSource.playOnAwake = true;
            bgmAudioSource.volume = 1f;
            bgmAudioSource.Play();
            Debug.LogError("BGM should be played");
        } else {
            Debug.LogError("BGM Sound is missing in Sound Manager");
        }
    }

    private void PlayZombieDieSound() {
        if(zombieDieSfx != null) {
            zombieAudioSource.PlayOneShot(zombieDieSfx, 1.0f);
            Debug.LogError("ZombieDieSfx should be played");
        } else {
            Debug.LogError("ZombieDieSfx is missing");
        }
    }

    private void PlayPlayerShootSound() {
        if(shootingSfx != null) {
            shootingAudioSource.PlayOneShot(shootingSfx, 0.6f);
            Debug.LogError("ShootingSfx should be played");
        } else {
            Debug.LogError("ShootingSfx is missing");
        }
    }
}
