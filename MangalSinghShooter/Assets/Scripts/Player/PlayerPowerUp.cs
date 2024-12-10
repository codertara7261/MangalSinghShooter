using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void ActivatePowerUp(string powerUpType, float duration) {
        switch (powerUpType) {
            case "Speed":
                StartCoroutine(SpeedBoost(duration));
                break;
            case "MachineGun":
                StartCoroutine(MachineGun(duration));
                break;
            case "Health":
                RestoreHealth();
                break;
        }
    }

    private IEnumerator MachineGun(float duration) {
        float originalFireRate = playerShooting.FireRate;
        playerShooting.FireRate /= 2;
        yield return new WaitForSeconds(duration);
        playerShooting.FireRate = originalFireRate;   
    }

    private IEnumerator SpeedBoost(float duration) {
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
