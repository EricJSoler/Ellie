using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILevelComplete : MonoBehaviour {
    public Text levelTXT;
    public Text thisTimeTXT;
    public Text bestTimeTXT;

    public int level;
    private float thisTime;
    private float bestTime;

    // Use this for initialization
    void Start () {
        levelTXT = gameObject.transform.GetChild(0).GetComponent<Text>() as Text;
        thisTimeTXT = gameObject.transform.GetChild(1).GetComponent<Text>() as Text;
        bestTimeTXT = gameObject.transform.GetChild(2).GetComponent<Text>() as Text;
    }
	
	// Update is called once per frame
	void Update () {
        if (level == 0) {
            levelTXT.text = "Tutorial Complete";
        } else {
            levelTXT.text = "Level " + level;

            bestTime = FindObjectOfType<DataHolder>().getBestTime(level);
            float minutes = bestTime / 60;
            float seconds = bestTime % 60;
            bestTimeTXT.text = minutes.ToString("00") + " : " + seconds.ToString("00"); 

            thisTime = FindObjectOfType<UITimer>().getTime();
            minutes = thisTime / 60;
            seconds = thisTime % 60;
            thisTimeTXT.text = minutes.ToString("00") + " : " + seconds.ToString("00");
        }

    }
}
