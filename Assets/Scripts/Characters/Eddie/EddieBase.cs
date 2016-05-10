using UnityEngine;
using System.Collections;

public class EddieBase : MonoBehaviour {

    #region PlayerComponents
    PlayerAnim m_anim;
    PlayerController m_controller;
    PlayerForces m_forces;

    public PlayerAnim playerAnim {
        get {
            return m_anim;
        }
    }

    public PlayerController playerController {
        get {
            return m_controller;
        }
    }

    public PlayerForces playerForces {
        get {
            return m_forces;
        }
    }
    #endregion

    public int m_PlayerPolarity = -1;
    public int m_startHealth = 0;
    bool m_repositioningPlayer = false;
    //returns true when the player is being 
    //moved by the game after being hit or whatever
    public bool relocationPlayer {
        get {
            return m_repositioningPlayer;
        }
    }

    Vector2 m_newPlayerPosition;

    void Awake() {
        m_anim = GetComponent<PlayerAnim>();
        m_controller = GetComponent<PlayerController>();
        m_forces = GetComponent<PlayerForces>();
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
            playerController.unlockControls();
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

    public void loseHealthTrap() {
        Destroy(this.gameObject);
    }
}
