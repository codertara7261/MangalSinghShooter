using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas rootCanvas;
    private GameObject pauseMenuPanel;

    private bool isPaused = false;

    private const string GAMEPAUSEMENU = "GamePauseMenu";

    private void Awake() {
        if(rootCanvas != null) {
            pauseMenuPanel = rootCanvas.transform.Find(GAMEPAUSEMENU).gameObject;
        }

        if(pauseMenuPanel != null) {
            pauseMenuPanel.SetActive(false);
        } else {
            Debug.Log("Cannot Find GamePauseMenu gameObejct in the RootCanvas");
        }
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    private void ResumeGame() {
        if(pauseMenuPanel != null) {
            pauseMenuPanel.SetActive(false);

            Animator animator = pauseMenuPanel.GetComponent<Animator>();
            if(animator != null) {
                animator.Play("GamePauseMenuSlideUp");
            }
        }
        StartCoroutine(ActionAfterAnimation(1f, false));
        
    }

    private void PauseGame() {
        if(pauseMenuPanel != null) {
            pauseMenuPanel.SetActive(true);

            Animator animator = pauseMenuPanel.GetComponent<Animator>();
            if(animator != null) {
                animator.Play("GamePauseMenuSlideDown");
            }
        }
        StartCoroutine(ActionAfterAnimation(0f, true));
        
    }

    private IEnumerator ActionAfterAnimation(float timeScale, bool isPaused) {
        yield return new WaitForSeconds(1f);
        Time.timeScale = timeScale;
        this.isPaused = isPaused;
    }
}
