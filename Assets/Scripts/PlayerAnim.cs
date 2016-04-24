using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void rotatePlayer() {
        transform.Rotate(transform.forward, 180f); //make this apretty lerp 
    }
}
