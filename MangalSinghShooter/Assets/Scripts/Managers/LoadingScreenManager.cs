using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingScreenManager : MonoBehaviour
{
    private static LoadingScreenManager Instance;


    private const string sceneToLoad = "GameScreen";
    private const string LOADINGSCREENCANVAS = "LoadingScreenCanvas";

    [Header("Canvas References")]
    private TextMeshProUGUI loadingText;
    private GameObject loadingScreenCanvas;

    private void Start() {
        StartCoroutine(LoadSceneAsync());
    }

    private void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        FindLoadingScreenObjects();
    }

    private void FindLoadingScreenObjects() {
        loadingScreenCanvas = GameObject.FindWithTag(LOADINGSCREENCANVAS);
        if(loadingScreenCanvas != null) {
            loadingText = loadingScreenCanvas.GetComponentInChildren<TextMeshProUGUI>();
            loadingText.gameObject.SetActive(false);
        } else {
            Debug.LogWarning("LoadingScreen with given tag not found");
        }
    }

    private IEnumerator LoadSceneAsync() {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);

        if(loadingText != null) {
            loadingText.gameObject.SetActive(true);
            while (!operation.isDone) {
                loadingText.text = "Loading.";
                yield return new WaitForSeconds(0.5f);
                loadingText.text = "Loading..";
                yield return new WaitForSeconds(0.5f);
                loadingText.text = "Loading...";
                yield return new WaitForSeconds(0.5f);
                loadingText.text = "Loading....";
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
