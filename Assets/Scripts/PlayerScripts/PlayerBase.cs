using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour
{

    #region PlayerComponents
    PlayerAnim m_anim;
    PlayerController m_controller;
    PlayerForces m_forces;
    PlayerDevToss m_dev;
    PlayerStats m_stats;
    PlayerGuideToss m_guide;

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

    public PlayerDevToss playerDevice {
        get {
            return m_dev;
        }
    }

    public PlayerGuideToss playerGuide { get { return m_guide; } }

    #endregion

    public int m_PlayerPolarity = -1;
    public int m_startHealth = 3;
    bool m_repositioningPlayer = false;
    //returns true when the player is being 
    //moved by the game after being hit or whatever
    public bool relocationPlayer {
        get {
            return m_repositioningPlayer;
        }
    }

    Vector2 m_newPlayerPosition;
    private bool m_doneReposition = false;
    private float m_timeRepostiiongComplete;
    private float m_timetoReleaseAfterReposition = .5f;

    private float m_animationTime = 2f;
    private float m_timeAnimStarted;

    void Awake() {
        m_anim = GetComponent<PlayerAnim>();
        m_controller = GetComponent<PlayerController>();
        m_forces = GetComponent<PlayerForces>();
        m_dev = GetComponent<PlayerDevToss>();
        m_guide = GetComponent<PlayerGuideToss>();
        m_stats = new PlayerStats(m_startHealth);
    }

    void Start() {

    }

    // Update is called once per frame

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting) {
            stream.SendNext(playerForces.absHor);
            //  stream.SendNext(playerForces.myRigidBody.gravityScale);
        }
        else if (stream.isReading) {
            int currentDirection = (int)stream.ReceiveNext();
            // int currentGravity = (int)stream.ReceiveNext();
            playerForces.setAbsHor(currentDirection);
            //    playerForces.setGravity(currentGravity);
        }

    }

    void Update() {
        if (m_repositioningPlayer) {
            // this.GetComponent<Collider2D>().enabled = false;
            this.GetComponent<Rigidbody2D>().Sleep();
            movePlayerTowards(m_newPlayerPosition);
        }
    }

    public void movePlayerTowards(Vector2 destination) {
        if (Vector2.Distance(transform.position, destination)
            < 1f) {
            if (!m_doneReposition) {
                m_doneReposition = true;
                m_timeRepostiiongComplete = Time.time;
            }
            if (Time.time > m_timeRepostiiongComplete + m_timetoReleaseAfterReposition) {
                this.GetComponent<Collider2D>().enabled = true;
                this.GetComponent<Rigidbody2D>().WakeUp();
                m_repositioningPlayer = false;
                playerController.unlockControls();
            }
        }
        else {
            if (Time.time > m_animationTime + m_timeAnimStarted) {
                //Vector2 direction = new Vector2(
                //    destination.x - transform.position.x,
                //    destination.y - transform.position.y);
                //direction.Normalize();
                //float speed = Vector2.Distance(transform.position, destination);
                //Vector3 newposition = new Vector3(
                //    transform.position.x + (direction * speed * Time.deltaTime).x,
                //    transform.position.y + (direction * speed * Time.deltaTime).y,
                //    0f);
                transform.position = destination;
            }
        }

    }

    public void loseHealthTrap() {
        if (!relocationPlayer) {
            if (m_stats.takeHit()) { //player dead
                m_anim.killPlayer();
                FindObjectOfType<LevelManager>().restartLevel();
            }
            m_timeAnimStarted = Time.time;
            m_anim.hurtPlayer();
            repositionPlayer(playerForces.lastCheckPoint);
        }
    }

    public void repositionPlayer(Vector2 destination) {
        playerController.lockControls(Mathf.Infinity); //lock the controller from taking input
        m_repositioningPlayer = true;
        m_doneReposition = false;
        m_newPlayerPosition = destination;
    }

    public int Health() {
        return m_stats.health;
    }

    public void addHealth() {
        m_stats.addHealth();
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Guide")
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<CircleCollider2D>());
    }
}

