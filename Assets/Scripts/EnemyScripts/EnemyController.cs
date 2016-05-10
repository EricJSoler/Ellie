using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour {
    //example enemy controller that will patrol between two locations
    public GameObject[] checkpoints;

    EnemyBase m_base;

    int currentGoal;

    float distanceBeforeTurning = .1f;
	// Use this for initialization
	void Start () {
        m_base = this.GetComponent<EnemyBase>();
	}
	
	// Update is called once per frame
	void Update () {

        if( Mathf.Abs(checkpoints[currentGoal].transform.position.x  
            - base.transform.position.x )<= distanceBeforeTurning) {
            currentGoal = (currentGoal + 1) % checkpoints.Length;
        }
        if (checkpoints[currentGoal].transform.position.x
            < transform.position.x) {
            m_base.playerForces.run(-1);
        }
        else {
            m_base.playerForces.run(1);
        }
	}
}
