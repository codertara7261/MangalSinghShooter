using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    [Header("UI References")]
    [SerializeField] private GameObject pauseMenuCanvas;
    [SerializeField] private GameObject gameOverCanvas;
    [SerializeField] private GameObject hudCanvas;

    public static GameManager Instance;

    private int score;
    private bool isPaused = false;

    private const string PauseMenuSlideUp = "GamePauseMenuSlideUp";
    private const string PauseMenuSlideDown = "GamePauseMenuSlideDown";
    private const string GameOverMenuSlideDown = "GameOverMenuSlideDown";

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void InitializeUI() {
        if (pauseMenuCanvas != null) pauseMenuCanvas.SetActive(false);
        if (gameOverCanvas != null) gameOverCanvas.SetActive(false);
        if (hudCanvas != null) hudCanvas.SetActive(true);
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause() {
        if(isPaused) {
            ResumeGame();
        } else {
            PauseGame();
        }
    }

    public void ToggleGameOver() {
        if(isPaused) {
            GameOver();
        }
    }

    public void ToggleQuitGame() {
        Application.Quit();
    }

    public void ToggleRestart() {
        if (isPaused) {
            Restart();
        } else {
            Restart();
            isPaused = false;
        }
    }

    private void Restart() {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ResumeGame() {
        if(pauseMenuCanvas != null) {
            pauseMenuCanvas.SetActive(true);
            Animator resumeAnimator = pauseMenuCanvas.GetComponent<Animator>();
            if (resumeAnimator != null) {
                resumeAnimator.Play(PauseMenuSlideUp);
                StartCoroutine(ActionAfterAnimation(1f, true));
            }
        }
    }

    private void PauseGame() {
        if(pauseMenuCanvas != null) {
            pauseMenuCanvas.SetActive(true);
            Animator pauseAnimator = pauseMenuCanvas.GetComponent<Animator>();
            if(pauseAnimator != null) {
                pauseAnimator.Play(PauseMenuSlideDown);
                StartCoroutine(ActionAfterAnimation(0f, true));
            }
        }
    }

    private void GameOver() {
        if(gameOverCanvas != null) {
            gameOverCanvas.SetActive(true);
            Animator gameOverAnimator = gameOverCanvas.GetComponent<Animator>();
            if(gameOverAnimator != null) {
                gameOverAnimator.Play(GameOverMenuSlideDown);
                StartCoroutine(ActionAfterAnimation(0f, true));
            }
        }
    }

    private IEnumerator ActionAfterAnimation(float timeScale, bool isPaused) { 
        yield return new WaitForSeconds(1f);
        Time.timeScale = timeScale;
        Debug.Log(Time.timeScale);
        this.isPaused = isPaused;

    }

    public void AddScore(int points) {
        score += points;
    }

    public int GetScore() {
        return score;
    }
}
