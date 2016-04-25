using UnityEngine;
using System.Collections;

public class PlayerDevToss : MonoBehaviour {

    PlayerBase m_base;
    Rigidbody2D m_body;
    public string devicePrefab = "device";
    GameObject m_device;
    public Vector2 m_rightFacingSpawn;
    public Vector2 m_leftFacingSpawn;
    public float m_centerOffset;
    public float m_horVel = 10f;
    public float m_upVel = 2f;
	// Use this for initialization
	void Start () {
        m_base = this.GetComponent<PlayerBase>();
        m_device = Resources.Load(devicePrefab) as GameObject;
        m_body = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void throwDevice(int _polarity) {
        Vector3 spawn;
        Vector2 vel;
        
        if(m_base.playerForces.absHor == 1) { //facing right
            if (m_base.playerForces.absUp == 1) {
                spawn = new Vector3(
                    this.transform.position.x + m_rightFacingSpawn.x,
                    this.transform.position.y + m_centerOffset, 0f);
                vel = new Vector2(m_horVel + m_body.velocity.x, m_upVel + m_body.velocity.y);
                //Debug.Log("throwing from normal up and right");
            }
            else {
                spawn = new Vector3(
                    this.transform.position.x + m_rightFacingSpawn.x,
                    this.transform.position.y - m_centerOffset, 0f);
                vel = new Vector2(m_horVel + m_body.velocity.x, -1 *m_upVel + m_body.velocity.y);
                //Debug.Log("throwing from normal down and right");
            }
        }
        else { //facing left
            if(m_base.playerForces.absUp == 1) {
                spawn = new Vector3(
                    this.transform.position.x + m_leftFacingSpawn.x,
                    this.transform.position.y + m_centerOffset, 0f);
                vel = new Vector2(m_horVel * -1 + m_body.velocity.x, m_upVel + m_body.velocity.y);
            }
            else {
                spawn = new Vector3(
                    this.transform.position.x + m_leftFacingSpawn.x,
                    this.transform.position.y - m_centerOffset, 0f);
                vel = new Vector2(m_horVel * -1 + m_body.velocity.x, m_upVel * -1 + m_body.velocity.y);
            }
        }

        GameObject dev = (GameObject)Instantiate(m_device,
            spawn, transform.rotation);

        Rigidbody2D rb = dev.GetComponent<Rigidbody2D>();
        rb.velocity = vel;
        rb.gravityScale = this.GetComponent<Rigidbody2D>().gravityScale;
    }
}
