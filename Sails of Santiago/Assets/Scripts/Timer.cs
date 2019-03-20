using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text UI_Time;
    public float time_bonus = 4f;
    private float time_left = 60f;

    public void TimeAdd () {
        time_left += time_bonus;
        UI_Time.text = "Time Left:" + Math.Round(time_left, 0);
        Debug.Log("adding time");
    }
    
    // Update is called once per frame
    void Update()
    {
        time_left -= Time.deltaTime;
        UI_Time.text = "Time Left:" + Math.Round(time_left, 0);

        if (time_left <= 0f)
            SceneManager.LoadScene("GameOver");
        //endif
    }



}
