using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIBorder : MonoBehaviour {

    public int COLORCHANGE_LENGTH = 20;

    public int colorIndex;
    private int timer;
    private bool changeEnabled;
    
	// Use this for initialization
	void Start () {
        disableColorChange();
        timer = COLORCHANGE_LENGTH;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (changeEnabled) {
            if (timer > 0) {
                timer--;
            } else {
                timer = COLORCHANGE_LENGTH;
                changeColors();
                colorIndex++;
                colorIndex %= 7;
            }
        }
	}

    private void changeColors() {
        switch (colorIndex) {
            case 0:
                GetComponent<Image>().color = Color.red;
                return;
            case 1:
                GetComponent<Image>().color = new Color(255, 100, 0);
                return;
            case 2:
                GetComponent<Image>().color = Color.yellow;
                return;
            case 3:
                GetComponent<Image>().color = Color.green;
                return;
            case 4:
                GetComponent<Image>().color = Color.cyan;
                return;
            case 5:
                GetComponent<Image>().color = Color.blue;
                return;
            case 6:
                GetComponent<Image>().color = new Color(146, 0, 255);
                return;
        }
    }

    public void disableColorChange() {
        changeEnabled = false;
        GetComponent<Image>().color = Color.grey;
        //GetComponent<Image>().color = Color.black;
    }

    public void enableColorChange() {
        changeEnabled = true;
    }
}
