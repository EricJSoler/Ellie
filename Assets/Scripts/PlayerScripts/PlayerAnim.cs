using UnityEngine;
using System.Collections;

public class PlayerAnim : MonoBehaviour {

    public GameObject body;
    public GameObject player;
    private Animator animator;
    public int facingDirection,grounded;
    public bool hit;
        // Use this for initialization
    PlayerBase m_base;
	void Start () {
        m_base = GetComponent<PlayerBase>();
        player = gameObject;
        grounded = 1;
        animator = GetComponent<Animator>();
        hit = false;
    }
	
	// Update is called once per frame
	void Update () {

        facingDirection = player.GetComponent<PlayerForces>().absHor;
        if (player.GetComponent<PlayerForces>().absUp * facingDirection > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
	}

    public void hurtPlayer() {
        animator.SetTrigger("Hit");
    }
    public void rotatePlayer() {
        transform.Rotate(transform.forward, 180f); //make this apretty lerp 
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        //grounded = -grounded;


    }
}
