using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float powerUpSpawnChance = 0.2f;
    [SerializeField] private float maxPowerUpOnScreen = 1f;
    [SerializeField] private float spawnInterval = 15f;

    private int currentPowerUps;

    private int randomXSpawn;
    private int randomYSpawn;
    private Vector2 powerUpPosition;

    private void Start() {
        currentPowerUps = 0;
        if(powerUpPrefabs != null) {
            StartCoroutine(SpawnPowerUp());
        }
    }

    private IEnumerator SpawnPowerUp() {
        while (true) {

            randomXSpawn = Random.Range(-8, 8);
            randomYSpawn = Random.Range(-4, 4);
            powerUpPosition = new Vector2(randomXSpawn, randomYSpawn);

            if (Random.value < powerUpSpawnChance && currentPowerUps <= maxPowerUpOnScreen) {
                int randomIndex = Random.Range(0, powerUpPrefabs.Length);
                Instantiate(powerUpPrefabs[randomIndex], powerUpPosition, Quaternion.identity);
                currentPowerUps++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void OnPowerUpCollected() {
        currentPowerUps--;
    }
}
