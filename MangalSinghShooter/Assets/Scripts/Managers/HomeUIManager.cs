using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeUIManager : MonoBehaviour
{
    private const string LOADINGSCREEN = "LoadingScreen";

    public void OnPlayButtonClicked() {
        SceneManager.LoadScene(LOADINGSCREEN);
    }

    public void OnQuitButtonClicked() {
        Application.Quit();
    }
}
