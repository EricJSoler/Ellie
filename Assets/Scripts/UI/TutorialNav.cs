using UnityEngine;
using System.Collections;

public class TutorialNav : MonoBehaviour {

    public GameObject[] panels;

	// Use this for initialization
	void Start () {
        disablePanels();
        next(0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void next (int index) {
        disablePanels();
        panels[index].SetActive(true);
    }

    public void disablePanels () {
        for (int i = 0; i < panels.Length; i++) {
            panels[i].SetActive(false);
        }
    }
}
