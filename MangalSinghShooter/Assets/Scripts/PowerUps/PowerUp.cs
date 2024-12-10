using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerUps;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration = 5f;

    private const string PLAYER = "Player";


    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag(PLAYER)) {
            PlayerPowerUp playerPowerUp = collision.GetComponent<PlayerPowerUp>();
            if(playerPowerUp != null) {
                playerPowerUp.ActivatePowerUp(powerUpType, duration);
            }

            FindObjectOfType<PowerUpSpawner>().OnPowerUpCollected();

            Destroy(gameObject);
        }
    }
}
