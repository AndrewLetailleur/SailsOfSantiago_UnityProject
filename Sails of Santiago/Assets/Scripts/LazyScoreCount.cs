using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LazyScoreCount : MonoBehaviour {

    private int c_score = 0;
    public Text scoreText;
	
    // Use this for initialization
	void Start () {
        scoreText.GetComponent<Text>();//within the score object itself, with the text property.
        scoreText.text = "Score: " + c_score; }

    public void ScoreCount(int value) {
        c_score += value;
        scoreText.text = "Score: " + c_score;
    }
}
