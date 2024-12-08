using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    private int currentHealth;

    private const string GAMEOVERCANVAS = "GameOverCanvas";

    GameObject gameOverCanvas;

    private void Awake() {
        gameOverCanvas = GameObject.Find(GAMEOVERCANVAS);

        if (gameOverCanvas != null) {
            gameOverCanvas.SetActive(false);
        }
    }

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

        if(gameOverCanvas != null) {
            gameOverCanvas.SetActive(true);

            Animator animator = gameOverCanvas.GetComponent<Animator>();
            if (animator != null) {
                animator.Play("GameOverMenuSildeDown");
            }
        }
        StartCoroutine(PauseGameAfterAnimation());

        Debug.Log("You Loose, Mangal Singh Died");
    }

    private IEnumerator PauseGameAfterAnimation() {
        yield return new WaitForSecondsRealtime(1f);

        Time.timeScale = 0;
    }


    public int GetHealth() {
        return currentHealth;
    }
}
