using UnityEngine;
using System.Collections;

public class Bolt : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            GameObject.Find("LevelManager").GetComponent<LevelManager>().playExtraHealth(); // SFX

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<PlayerBase>().addHealth(); 
            Destroy(this.gameObject);
        }
    }
}
