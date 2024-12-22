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

    [Header("Animation Reference")]
    private const string PauseMenuSlideUp = "GamePauseMenuSlideUp";
    private const string PauseMenuSlideDown = "GamePauseMenuSlideDown";
    private const string GameOverMenuSlideDown = "GameOverMenuImg";

    [Header("Canvas References")]
    private const string PAUSEMENUCANVAS = "GamePauseMenu";
    private const string GAMEOVERMENUCANVAS = "GameOverMenu";
    private const string HUDCANVAS = "HUD";

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    private void Start() {
        SetupReferences();
    }

    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
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

    private void SetupReferences() {
        Canvas[] allCanvases = Resources.FindObjectsOfTypeAll<Canvas>();

        foreach (Canvas canvas in allCanvases) {
            if (canvas.gameObject.name == PAUSEMENUCANVAS) pauseMenuCanvas = canvas.gameObject;
            else if (canvas.gameObject.name == GAMEOVERMENUCANVAS) gameOverCanvas = canvas.gameObject;
            else if (canvas.gameObject.name == HUDCANVAS) hudCanvas = canvas.gameObject;
        }

        if (pauseMenuCanvas == null) Debug.LogError("PauseMenuCanvas references is missing");
        if (gameOverCanvas == null) Debug.LogError("GameOverMenuCanvas reference is missing");
        if (hudCanvas == null) Debug.LogError("HUDCanvas reference is missing");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        SetupReferences();
    }

    public void TogglePause() {
        if(isPaused) {
            ResumeGame();
        } else {
            PauseGame();
            isPaused = true;
        }
    }

    public void ToggleGameOver() {
        GameOver();
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        score = 0;
    }

    private void ResumeGame() {
        if(pauseMenuCanvas != null) {
            pauseMenuCanvas.SetActive(true);
            Animator resumeAnimator = pauseMenuCanvas.GetComponentInChildren<Animator>();
            if (resumeAnimator != null) {
                resumeAnimator.Play(PauseMenuSlideUp);
                StartCoroutine(ActionAfterAnimation(1f, false));
            } else {
                Debug.LogError("Resume Animator not found");
            }
        } else {
            Debug.LogError("PauseMenuCanvas not found");
        }
    }

    private void PauseGame() {
        if(pauseMenuCanvas != null) {
            pauseMenuCanvas.SetActive(true);
            Animator pauseAnimator = pauseMenuCanvas.GetComponentInChildren<Animator>();
            if(pauseAnimator != null) {
                pauseAnimator.Play(PauseMenuSlideDown);
                StartCoroutine(ActionAfterAnimation(0f, true));
            } else {
                Debug.LogError("PauseAnimator not found");
            }
        } else {
            Debug.LogError("PauseMenuCanvas not found");
        }
    }

    private void GameOver() {
        if(gameOverCanvas != null) {
            gameOverCanvas.SetActive(true);
            Animator gameOverAnimator = gameOverCanvas.GetComponentInChildren<Animator>();
            if(gameOverAnimator != null) {
                gameOverAnimator.Play(GameOverMenuSlideDown);
                StartCoroutine(ActionAfterAnimation(0f, true));
            } else {
                Debug.LogError("GameOverAnimator not found");
            }
        } else {
            Debug.LogError("GameOver Canvas not found");
        }
    }

    private IEnumerator ActionAfterAnimation(float _timeScale, bool isPaused) {
        yield return new WaitForSecondsRealtime(1f);
        Time.timeScale = _timeScale;
        this.isPaused = isPaused;

    }

    public void AddScore(int points) {
        score += points;
    }

    public int GetScore() {
        return score;
    }
}
