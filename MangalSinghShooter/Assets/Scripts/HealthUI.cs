using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    private PlayerHealth playerHealth;

    private void Awake() {
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null) {
            playerHealth.HealthChanged += UpdateHearts;
            UpdateHearts(playerHealth.CurrentHealth); // Ensure UI reflects current health at start
        }
        else {
            Debug.LogError("PlayerHealth not found in the scene!");
        }
    }

    private void UpdateHearts(int currentHealth) {
        for (int i = 0; i < hearts.Length; i++) {
            hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
        }
    }

    private void OnDestroy() {
        if (playerHealth != null) {
            playerHealth.HealthChanged -= UpdateHearts; // Corrected condition
        }
    }
}
