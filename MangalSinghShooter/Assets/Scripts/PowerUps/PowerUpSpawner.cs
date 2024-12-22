using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private const string PLAYER = "Player";

    [SerializeField] private GameObject[] powerUpPrefabs;
    [SerializeField] private float powerUpSpawnChance = 0.6f;
    [SerializeField] private float maxPowerUpOnScreen = 5f;
    [SerializeField] private float spawnInterval = 5f;

    private float randomAngle;
    private int randomDistanceFromPlayer;
    private int currentPowerUps;

    private Transform playerTransform;

    private Camera mainCamera;
    private float camHeight;
    private float camWidth;

    private int randomX;
    private int randomY;
    private Vector2 powerUpPosition;

    private void Awake() {
        playerTransform = GameObject.FindWithTag(PLAYER).transform;
        mainCamera = Camera.main;
    }

    private void Start() {
        currentPowerUps = 0;
        if(powerUpPrefabs != null) {
            StartCoroutine(NewPowerUpSpawner());
        }

        camHeight = 2f * mainCamera.orthographicSize;
        camWidth = camHeight * mainCamera.aspect;
    }

    private IEnumerator NewPowerUpSpawner() {
        while (true) {

            float minX = playerTransform.position.x - camWidth / 4;
            float maxX = playerTransform.position.x + camWidth / 4;

            float minY = playerTransform.position.y - camHeight / 4;
            float maxY = playerTransform.position.y + camHeight / 4;

            Vector2 powerUpPosition = new Vector2(
                UnityEngine.Random.Range(minX, maxX),
                UnityEngine.Random.Range(minY, maxY)
                );

            if (UnityEngine.Random.value < powerUpSpawnChance && currentPowerUps <= maxPowerUpOnScreen) {
                int randomIndex = UnityEngine.Random.Range(0, powerUpPrefabs.Length);
                Instantiate(powerUpPrefabs[randomIndex], powerUpPosition, Quaternion.identity);
                currentPowerUps++;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void PowerUpCollected() {
        currentPowerUps--;
    }
}
