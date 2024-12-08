using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private void Start() {
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage) {
        currentHealth -= damage;
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        Time.timeScale = 0;
        Debug.Log("You Loose, Mangal Singh Died");
    }

    public int GetHealth() {
        return currentHealth;
    }
}
