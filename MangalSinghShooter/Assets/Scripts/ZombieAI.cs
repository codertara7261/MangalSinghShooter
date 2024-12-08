using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private float zombieSpeed = 2f;

    private Transform player;

    private const string PLAYER = "Player";

    private void Start() {
        player = GameObject.FindWithTag(PLAYER).transform;
    }

    private void Update() {
        if (player != null) {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * zombieSpeed * Time.deltaTime;
        }
    }
}
