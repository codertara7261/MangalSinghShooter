using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour {

    [Header("Sprite References")]
    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [Header("Animation Settings")]
    [SerializeField] private float wobbleSpeed = 6f;
    [SerializeField] private float wobbleAmount = 10f;

    private PlayerHealth playerHealth;

    private void Awake() {
        playerHealth = FindObjectOfType<PlayerHealth>();

        if (playerHealth != null) {
            UpdateHearts(playerHealth.CurrentHealth);
            playerHealth.OnHealthChanged += UpdateHearts;     
        }
        else {
            Debug.LogError("PlayerHealth not found in the scene!");
        }
    }

    private void UpdateHearts(int currentHealth) {
        for (int i = 0; i < hearts.Length; i++) {
            // old heart system before adding effects to the heart -> hearts[i].sprite = i < currentHealth ? fullHeart : emptyHeart;
            if(i < currentHealth) {
                hearts[i].sprite = fullHeart;
                hearts[i].color = new Color(1f, 1f, 1f, 1f);
                StartCoroutine(WobbleHeart(hearts[i].rectTransform));
                Debug.Log("If part called");
            } else {
                hearts[i].sprite = emptyHeart;
            }
        }
    }

    private IEnumerator WobbleHeart(RectTransform heartTransform) {
        float time = 0f;
        Vector3 originalPos = heartTransform.localPosition;

        while(true) {
            time += Time.deltaTime * wobbleSpeed;
            heartTransform.localPosition = originalPos + new Vector3(0, Mathf.Sin(time) * wobbleAmount, 0);
            yield return null;
        }
    }

    private void OnDestroy() {
        if (playerHealth != null) {
            playerHealth.OnHealthChanged -= UpdateHearts; // Corrected condition
        }
    }
}
