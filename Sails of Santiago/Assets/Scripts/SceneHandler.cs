using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour {

    public static SceneHandler instance;
    private float loadingProgress;
    public float LoadingProgress { get { return loadingProgress; } }
    public Slider progressBar;
    public GameObject loadingScreenObj;

    private GameHandler gh;
    private AudioHandler ah;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        ah = FindObjectOfType<AudioHandler>();
        gh = FindObjectOfType<GameHandler>();

        loadingProgress = 0.0f;
        if (progressBar == null) progressBar = GetComponentInChildren<Slider>(true);
        if (loadingScreenObj == null) loadingScreenObj = GetComponentInChildren<GameObject>(true);

        //Ensures object remains present between scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
    }

    #region Scene Transition Handling
    public void LoadScene(string sceneName) {
        loadingProgress = 0.0f;
        //loads a small loading screen scene
        SceneManager.LoadSceneAsync("LoadingScreen");

        loadingScreenObj.SetActive(true);

        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName) {

        //starts loading the desired scene in the background, setting it not to display.
        AsyncOperation toLoad = SceneManager.LoadSceneAsync(sceneName);
        toLoad.allowSceneActivation = false;

        //Updates load progress value for loading bars etc.
        while (!toLoad.isDone) {
            loadingProgress = Mathf.Clamp01(toLoad.progress / 0.9f);
            progressBar.value = loadingProgress;

            //NOTE: progress only reaches 0.9f and no higher as allowSceneActivation is set to false.
            if (toLoad.progress >= 0.9f) {
                toLoad.allowSceneActivation = true;
            }
            yield return null;
        }

        if (toLoad.isDone) loadingScreenObj.SetActive(false);
    }

    #endregion
}
