using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public void OnQuitButtonPressed() {
        if(GameManager.Instance != null) {
            GameManager.Instance.ToggleGameOver();
        }
    }

    public void OnRestartButtonPressed() {
        if(GameManager.Instance != null) {
            GameManager.Instance.ToggleRestart();
        }
    }
}
