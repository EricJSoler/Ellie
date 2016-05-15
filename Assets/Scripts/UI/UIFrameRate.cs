using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIFrameRate : MonoBehaviour {

    public Text timer;
    private double framerate;

    void Start() {
        framerate = 0f;

        timer = GetComponent<Text>() as Text;
    }

    void Update() {
        framerate = 1.0 / (Time.deltaTime);
        timer.text = "Frame Rate: " + framerate.ToString("00");
      
    }
}