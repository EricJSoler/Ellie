using UnityEngine;
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
            seconds = time % 60; //Use the euclidean division for the seconds.

            timer.text = minutes.ToString("00") + " : " + seconds.ToString("00");
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