﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;//for the Info switchin;

public class MainMenuScript : MonoBehaviour {
    
    /*No need for holding object variables, so their removed/[REDACTED]*/

    /// <summary>
    /// In project. "1" = Game, "0" = Menu. Binary wise.
    /// </summary>

    //play the first level, after menu. Scene wise
    public void PlayGame() {SceneManager.LoadScene(1); }//alt to (SceneManager.GetActiveScene().buildIndex + 1)

    public void LoadGame() { }//to load 'saved' save state, scene database management wise

    public void LoadMenu() { SceneManager.LoadScene(0); }

    public void QuitGame() {
        Debug.Log("Game has closed");
        Application.Quit();
    }

    /* No longer needed code, as GameObject.SetActive on OnClick() Unity does the trick just fine
    public void Credits() {}
    public void Info() {}
    public void Story() {}
    public void OptionsMenu() {}
    public void GoBack() {}
    */ /// end of no longer needed code
}