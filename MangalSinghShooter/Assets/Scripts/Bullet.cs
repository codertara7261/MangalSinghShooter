using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const string ZOMBIE = "Zombie";

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(ZOMBIE)) {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
