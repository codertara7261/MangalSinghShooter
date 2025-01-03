using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    public static event Action OnPlayerDie;
    public event Action<int> OnHealthChanged;
    public static event Action<GameObject> OnPlayerInitialized;

    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    public int CurrentHealth => currentHealth;

    public int MaxHealth {
        get => maxHealth;
        set => maxHealth = value;
    }

    private void Awake() {
        OnPlayerInitialized?.Invoke(gameObject);
        currentHealth = maxHealth;
        OnHealthChanged?.Invoke(currentHealth); // Trigger UI update on initialization
    }

    public void TakeDamage(int damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth); // Simplified clamping

        if (currentHealth <= 0) {
            Die();
            Debug.Log("Die method called");
        }

        OnHealthChanged?.Invoke(currentHealth);

    }

    private void Die() {
        GameManager.Instance.ToggleGameOver();
        OnPlayerDie?.Invoke();
    }
}