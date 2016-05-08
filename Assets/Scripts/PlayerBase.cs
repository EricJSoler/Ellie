using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {

    #region PlayerComponents
    PlayerAnim m_anim;
    PlayerController m_controller;
    PlayerForces m_forces;
    PlayerDevToss m_dev;
    PlayerStats m_stats;

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

    void Awake() {
        m_anim = GetComponent<PlayerAnim>();
        m_controller = GetComponent<PlayerController>();
        m_forces = GetComponent<PlayerForces>();
        m_dev = GetComponent<PlayerDevToss>();
        m_stats = new PlayerStats(m_startHealth);       
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
        }
        else {
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
        if (!relocationPlayer) {
            if (m_stats.takeHit()) { //player dead

            }
            Debug.Log("Health of Ellie: " + m_stats.health);
            repositionPlayer(playerForces.lastCheckPoint);
        }
    }

    public void repositionPlayer(Vector2 destination) {
        playerController.lockControls(Mathf.Infinity); //lock the controller from taking input for 5 seconds
        m_repositioningPlayer = true;
        m_newPlayerPosition = destination;
    }

    IEnumerator repositionPlayerAfterHit() {
        transform.position = playerForces.lastCheckPoint;
        yield return new WaitForSeconds(2f);
        m_repositioningPlayer = false;
        playerController.unlockControls();
    }

    public int Health() {
        return m_stats.health;
    }

    public void addHealth() {
        m_stats.addHealth();
    }
}
