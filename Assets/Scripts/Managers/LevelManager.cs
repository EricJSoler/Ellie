using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {


	// Use this for initialization
	void Start () {
        //SpawnNubbie();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void SpawnNubbie() {
        GameObject Nubbie = (GameObject)Instantiate(Resources.Load("Nubbie") as GameObject);
    }
}
