using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private float initialSpawnInterval = 2f;
    [SerializeField] private float minimumSpawnInterval = 0.5f;
    [SerializeField] private float difficultyIncreaseRate = 0.1f;
    [SerializeField] private float spawnDistance = 10f;

    private Transform player;
    private Camera mainCamera;

    private float currentSpawnInterval;

    private const string PLAYER = "Player";

    private void Start() {
        player = GameObject.FindWithTag(PLAYER).transform;
        mainCamera = Camera.main;

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
        Vector3 spawnPosition = GetSpawnPositionOutsideCamera();

        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetSpawnPositionOutsideCamera() {
        float camHeight = 2f * mainCamera.orthographicSize;
        float camWidth = camHeight * mainCamera.aspect;

        float spawnOffset = spawnDistance;

        int side = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch(side) {
            case 0:
                spawnPosition = new Vector3(
                    Random.Range(player.position.x - camWidth / 2, player.position.x + camWidth / 2),
                    player.position.y + camHeight / 2 + spawnOffset,
                    0f
                    );
                break;

            case 1:
                spawnPosition = new Vector3(
                    Random.Range(player.position.x - camWidth / 2, player.position.x + camWidth / 2),
                    player.position.y - camHeight  / 2  - spawnOffset,
                    0f
                    );
                break;

            case 2:
                spawnPosition = new Vector3(
                    player.position.x - camWidth / 2 - spawnOffset,
                    Random.Range(player.position.y - camHeight / 2, player.position.y + camHeight / 2),
                    0f
                    );
                break;

            case 3:
                spawnPosition = new Vector3(
                    player.position.x + camWidth / 2 + spawnOffset,
                    Random.Range(player.position.y - camHeight / 2, player.position.y + camHeight / 2),
                    0f
                    );
                break;
        }

        return spawnPosition;
    }
}
