using UnityEngine;
using System.Collections;

public class BlockMove : MonoBehaviour {

    
    public Vector2[] checkpoints;
    public int moveSpeed;

    int currentGoal;

    Rigidbody2D m_rBody;
    float distanceBeforeTurning = .1f;
    // Use this for initialization
    void Start() {
        m_rBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        if (Vector3.Distance(transform.position, checkpoints[currentGoal]) <= distanceBeforeTurning) {
            currentGoal = (currentGoal + 1) % checkpoints.Length;
        }
        Vector2 newVel = (checkpoints[currentGoal] - new Vector2(transform.position.x, transform.position.y)).normalized * moveSpeed;
        m_rBody.velocity = newVel;
        
    }
}
