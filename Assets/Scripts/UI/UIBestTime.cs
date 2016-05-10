using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBestTime : MonoBehaviour {

    public Text bestTime;
    private float time;
    public int level = 1;

    void Start() {
        time = 0f;
        bestTime = GetComponent<Text>() as Text;
    }

    void Update() {
        time = FindObjectOfType<DataHolder>().getBestTime(level);
        float minutes = time / 60;
        float seconds = time % 60;
        Debug.Log("Level " +  level);
        bestTime.text = minutes.ToString("00") + ":" + seconds.ToString("00");
    }
}