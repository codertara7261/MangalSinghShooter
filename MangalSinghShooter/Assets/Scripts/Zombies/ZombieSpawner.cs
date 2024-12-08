using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float initialSpawnInterval = 2f;
    [SerializeField] private float minimumSpawnInterval = 0.5f;
    [SerializeField] private float difficultyIncreaseRate = 0.1f;
    [SerializeField] private float spawnDistance = 10f;

    private Transform player;

    private float currentSpawnInterval;

    private const string PLAYER = "Player";

    private void Start() {
        player = GameObject.FindWithTag(PLAYER).transform;

        currentSpawnInterval = initialSpawnInterval;

        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies() {
        while (true) {
            SpawnZombie();

            yield return new WaitForSeconds(currentSpawnInterval);

            currentSpawnInterval = Mathf.Max(currentSpawnInterval - difficultyIncreaseRate, minimumSpawnInterval);
        }
    }

    private void SpawnZombie() {
        float angle = Random.Range(0f, 360f);
        Vector3 spawnPosition = player.position + new Vector3(
            Mathf.Cos(angle) * spawnDistance,
            Mathf.Sin(angle) * spawnDistance,
            0f
            );

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }
}
