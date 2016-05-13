using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    public GameObject body;
    public GameObject player;
    int facingDirection,grounded;
        // Use this for initialization
    PlayerBase m_base;
	void Start () {
        m_base = GetComponent<PlayerBase>();
        player = GameObject.FindGameObjectWithTag("Player");
        grounded = 1;
    }
	
	// Update is called once per frame
	void Update () {

        facingDirection = player.GetComponent<PlayerForces>().absHor;
        if (grounded*facingDirection > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
	}

    public void rotatePlayer() {
        transform.Rotate(transform.forward, 180f); //make this apretty lerp 
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        grounded = -grounded;


    }
}
