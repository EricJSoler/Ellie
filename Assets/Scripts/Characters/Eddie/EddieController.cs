using UnityEngine;
using System.Collections;

public class EddieController : MonoBehaviour {

    Rigidbody2D m_rigidBody;
    public float m_velocity = 2f;
    public static int MAX_MOVEMENT = 100;

    private int m_movementCounter;
    private int m_direction;

    void Start() {
        m_rigidBody = this.GetComponent<Rigidbody2D>();
        m_direction = 1;
        m_movementCounter = MAX_MOVEMENT;

    }

    void Update() {
        /*m_movementCounter--;
        run(m_direction);
        if (m_movementCounter == 0) {
            m_movementCounter = MAX_MOVEMENT;
            m_direction *= -1;
        }*/
    }

    private void run(int direction) {
        Vector2 newVel = new Vector2(m_velocity * direction, m_rigidBody.velocity.y);
        m_rigidBody.velocity = newVel;
    }
}

