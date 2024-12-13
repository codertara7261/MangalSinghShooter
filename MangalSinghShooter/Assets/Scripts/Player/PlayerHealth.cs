using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    public int MaxHealth {
        get => maxHealth;
        set => maxHealth = value;
    }

    public event Action<int> HealthChanged;

    private void Awake() {

        currentHealth = maxHealth;
        HealthChanged?.Invoke(currentHealth); // Trigger UI update on initialization
    }

    public void TakeDamage(int damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); // Simplified clamping

        if (currentHealth <= 0) {
            Die();
        }

        HealthChanged?.Invoke(currentHealth);
    }

    private void Die() {
        GameManager.Instance.ToggleGameOver();
    }
}