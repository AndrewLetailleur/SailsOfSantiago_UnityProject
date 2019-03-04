using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour {

    private SceneHandler sh;
    [SerializeField]
    private string initialLevelToLoad;

    private void Awake() {
        sh = FindObjectOfType<SceneHandler>();
    }

    ///leave start and update stuff for later. Until then...
    public void PlayGame() {
        if (initialLevelToLoad == null || initialLevelToLoad == "") initialLevelToLoad = "PortTest";
        sh.LoadScene(initialLevelToLoad);
    }
    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
