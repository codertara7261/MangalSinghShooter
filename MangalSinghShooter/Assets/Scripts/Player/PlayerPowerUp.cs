using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PowerUps;

public class PlayerPowerUp : MonoBehaviour
{
    private PlayerMovements playerMovements;
    private PlayerHealth playerHealth;
    private PlayerShooting playerShooting;

    private void Start() {
        playerMovements = GetComponent<PlayerMovements>();
        playerHealth = GetComponent<PlayerHealth>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    public void ActivatePowerUp(PowerUpType powerUpType, float duration) {
        switch (powerUpType) {
            case PowerUpType.Speed:
                StartCoroutine(ActivateSpeedBoost(duration));
                break;
            case PowerUpType.MachineGun:
                StartCoroutine(ActivateMachineGun(duration));
                break;
            case PowerUpType.Health:
                RestoreHealth();
                break;
        }
    }

    private IEnumerator ActivateMachineGun(float duration) {
        float originalFireRate = playerShooting.FireRate;
        playerShooting.FireRate /= 2;
        yield return new WaitForSeconds(duration);
        playerShooting.FireRate = originalFireRate;   
    }

    private IEnumerator ActivateSpeedBoost(float duration) {
        playerMovements.MoveSpeed *= 2;
        yield return new WaitForSeconds(duration);
        playerMovements.MoveSpeed /= 2;
    }

    private void RestoreHealth() {
        if(playerHealth != null) {
            playerHealth.TakeDamage(-1);
        }
    }
}
