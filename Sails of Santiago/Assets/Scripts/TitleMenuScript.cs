using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleMenuScript : MonoBehaviour {

    ///leave start and update stuff for later. Until then...
    public void PlayGame() { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); } //increment by scene 'layer'.
    public void QuitGame() {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
