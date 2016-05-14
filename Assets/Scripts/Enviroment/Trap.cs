using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {

    public int polarity = 1;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col) {
        if(col.gameObject.tag == "Player") {
            PlayerBase pb = col.gameObject.
                GetComponent<PlayerBase>();
            if(pb.m_PlayerPolarity * polarity < 0) {
                pb.loseHealthTrap();
            }
        }
        if (col.gameObject.tag == "Enemy") {
            EnemyBase pb = col.gameObject.
              GetComponent<EnemyBase>();
            if (pb.m_PlayerPolarity * polarity < 0) {
                pb.killme();
            }
        }
    }
}
