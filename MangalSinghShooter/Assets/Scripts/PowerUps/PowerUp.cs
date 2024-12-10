using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField] private string powerUpType;
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
