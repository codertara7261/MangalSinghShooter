using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float spawnInterval;
    [SerializeField] private float spawnDistance;

    private Transform player;

    private const string PLAYER = "Player";

    private void Start() {
        player = GameObject.FindWithTag(PLAYER).transform;

        StartCoroutine(SpawnZombies());
    }

    private IEnumerator SpawnZombies() {
        while (true) {
            SpawnZombie();
            yield return new WaitForSeconds(spawnInterval);
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
