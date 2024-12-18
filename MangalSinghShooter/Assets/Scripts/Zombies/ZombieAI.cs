using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    [SerializeField] private float initialZombieSpeed = 2f;
    [SerializeField] private float speedIncreaseRate = 0.1f;

    private float currentZombieSpeed;

    private Transform player;

    private const string PLAYER = "Player";

    private void Start() {
        player = GameObject.FindWithTag(PLAYER).transform;

        currentZombieSpeed = initialZombieSpeed + (GameManager.Instance.GetScore() * speedIncreaseRate);
    }

    private void Update() {
        if (player != null) {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.position += direction * currentZombieSpeed * Time.deltaTime;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag(PLAYER)) {
            PlayerHealth playerhealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerhealth != null) {
                playerhealth.TakeDamage(1);
            }

            Destroy(gameObject);
        }
    }
}
