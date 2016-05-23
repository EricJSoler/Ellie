using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour {
    public GameObject body;
    public GameObject enemy;
    public int facingDirection, grounded;
    EnemyBase m_base;

    // Use this for initialization
    void Start () {
        m_base = GetComponent<EnemyBase>();
        enemy = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
        facingDirection = enemy.GetComponent<EnemyForces>().absHor;
        if (enemy.GetComponent<EnemyForces>().absUp * facingDirection > 0) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }

    }

    public void rotatePlayer() {
        transform.Rotate(transform.forward, 180f); //make this apretty lerp 
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
        //grounded = -grounded;
    }
}
