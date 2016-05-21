using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    private LightBulb lightbulb;

	// Use this for initialization
	void Start () {
        lightbulb = GetComponentInChildren<LightBulb>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col) {
        if(col.gameObject.tag == "Player") {
            col.GetComponent<PlayerBase>().
                playerForces.storeCurrentCheckPoint();
            lightBulbOn();       
        }
    }

    public void lightBulbOn() {
        lightbulb.On();
    }

    public void lightBulbOff() {
        lightbulb.Off();
    }
}
