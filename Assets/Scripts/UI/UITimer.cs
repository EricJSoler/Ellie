﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UITimer : MonoBehaviour {

    public Text timer;
    private float time;

    private float minutes;
    private float seconds;
    private bool timerPaused;

    void Start() {
        time = 0f;

        timer = GetComponent<Text>() as Text;
        timerPaused = false;
    }

    void Update() {
        if (!timerPaused) {
            time += Time.deltaTime;

            minutes = time / 60; //Divide the guiTime by sixty to get the minutes.
            seconds = time % 60;//Use the euclidean division for the seconds.
            //var fraction = (time * 100) % 100;

            //update the label value
            //timer.text = string.Format("{0:00} : {1:00} : {2:000}", minutes, seconds, fraction);
            timer.text = minutes.ToString("00") + ":" + seconds.ToString("00");
        } 
    }

    public float getTime() {
        return time;
    }

    public void pause() {
        timerPaused = true;
    }

    public void resume() {
        timerPaused = false;
    }
}