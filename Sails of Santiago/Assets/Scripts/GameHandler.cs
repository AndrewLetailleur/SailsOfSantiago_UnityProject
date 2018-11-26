using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static GameHandler instance = null;
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused;}}

    private SceneHandler sh;
    private AudioHandler ah;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }

        ah = FindObjectOfType<AudioHandler>();
        sh = FindObjectOfType<SceneHandler>();

        //Ensures object remains present between scenes.
        DontDestroyOnLoad(this.gameObject);
    }

    void Start() {
        sh.LoadScene("Inside");
    }

    void Update() {

    }

    #region Pause Handling
    public void PauseUnpase() {
        if (isPaused) {
            Unpause();
        }
        else {
            Pause();
        }
        isPaused = !isPaused;
    }
    private void Pause() {
        isPaused = true;
        Time.timeScale = 0.0f;
    }
    private void Unpause() {
        isPaused = false;
        Time.timeScale = 1.0f;
    }
    #endregion

}
