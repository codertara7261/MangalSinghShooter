using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // public static event Action OnZombieDie;

    private const string ZOMBIE = "Zombie";

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(ZOMBIE)) {

            SoundManager.Instance.PlayZombieDieSound();

            Destroy(gameObject);

            GameManager.Instance.AddScore(1);

            Destroy(collision.gameObject);

        }
    }
}
