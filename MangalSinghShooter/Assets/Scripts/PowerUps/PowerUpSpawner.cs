using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] float powerUpSpawnChance = 0.2f;
    [SerializeField] float maxPowerUpOnScreen = 1f;
    [SerializeField] float spawnInterval = 5f;

    private int currentPowerUps = 0;

    private int randomXSpawn;
    private int randomYSpawn;
    private int randomIndex;
    private Vector2 powerUpPosition;

    private void Start() {
        if(powerUpPrefabs != null) {
            StartCoroutine(SpawnPowerUp());
            Debug.Log("StartCoroutine working");
        }
    }

    private IEnumerator SpawnPowerUp() {
        while (true) {

            randomXSpawn = Random.Range(-8, 8);
            randomYSpawn = Random.Range(-4, 4);
            powerUpPosition = new Vector2(randomXSpawn, randomYSpawn);

            if (Random.value < powerUpSpawnChance && currentPowerUps <= maxPowerUpOnScreen) {
                randomIndex = Random.Range(0, powerUpPrefabs.Length);
                Instantiate(powerUpPrefabs[randomIndex], powerUpPosition, Quaternion.identity);
                currentPowerUps++;
                Debug.Log("PowerUpsSpawning");
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void OnPowerUpCollected() {
        currentPowerUps--;
    }
}