using UnityEngine;
using System.Collections;

public class TossGuide : MonoBehaviour {

    private Rigidbody2D rb;
    private GameObject player;
    private Renderer ren;
    private PlayerDevToss toss;
    private GameObject m_guide;
    private float m_upVel;
    private float m_horVel;
    private float additionalTime;

	// Use this for initialization
	void Start () {
        //Initialize fields
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody2D>();
        ren = this.GetComponent<Renderer>();
        toss = new PlayerDevToss();
        m_guide = Resources.Load("TossGuide") as GameObject;
        additionalTime = 0;
        
        //Set specific traits        
        transform.position = player.transform.position;
        ren.material.color = new Color(1, 1, 1, 0.5f);

        //Execute guide
        Run();
	}
	
	// Update is called once per frame
	void Update() { }

    void Run()
    {
        Vector2 v = toss.determineVelocity();
        m_horVel = v.x;
        m_upVel = v.y;

        Vector3[] srb = toss.determineRB();
        Vector3 spawn = srb[0];
        Vector2 rb_vel = new Vector2(srb[1].x, srb[1].y);

        GameObject gide = (GameObject)Instantiate(m_guide, spawn, transform.rotation);
        Rigidbody2D gide_rb = gide.GetComponent<Rigidbody2D>();
        gide_rb.velocity = rb_vel;
        gide_rb.gravityScale = this.GetComponent<Rigidbody2D>().gravityScale * PlayerDevToss.GRAV_WEIGHT;
    }

    void OnCollisionEnter2D(Collision2D c2d)
    {
        if (c2d.gameObject.tag != "Player")
        { Destroy(gameObject); }
    }
}
