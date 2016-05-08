using UnityEngine;
using System.Collections;

public class ProttieBase : MonoBehaviour {

    #region PlayerComponents
    EnemyAnim m_anim;
    ProttieController m_controller;
    ProttieForces m_forces;

    public EnemyAnim enemyAnim {
        get {
            return m_anim;
        }
    }

    public ProttieController prottieController {
        get {
            return m_controller;
        }
    }

    public ProttieForces prottieForces {
        get {
            return m_forces;
        }
    }
    #endregion

    public int m_PlayerPolarity = 1;
    public int m_startHealth = 3;
    bool m_repositioningPlayer = false;

    Vector2 m_newPlayerPosition;

    void Awake() {
        m_anim = GetComponent<EnemyAnim>();
        m_controller = GetComponent<ProttieController>();
        m_forces = GetComponent<ProttieForces>();
    }

    void Start() {

    }

    // Update is called once per frame
    void Update() {
        if (m_repositioningPlayer) {
            this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().Sleep();
            movePlayerTowards(m_newPlayerPosition);
        }
    }

    public void movePlayerTowards(Vector2 destination) {
        if (Vector2.Distance(transform.position, destination)
            < 1f) {
            this.GetComponent<Collider2D>().enabled = true;
            this.GetComponent<Rigidbody2D>().WakeUp();
            m_repositioningPlayer = false;
        } else {
            Vector2 direction = new Vector2(
                destination.x - transform.position.x,
                destination.y - transform.position.y);
            direction.Normalize();
            float speed = Vector2.Distance(transform.position, destination);
            Vector3 newposition = new Vector3(
                transform.position.x + (direction * speed * Time.deltaTime).x,
                transform.position.y + (direction * speed * Time.deltaTime).y,
                0f);
            this.transform.position = newposition;
        }

    }

    public void repositionPlayer(Vector2 destination) {
        m_repositioningPlayer = true;
        m_newPlayerPosition = destination;
    }
}
