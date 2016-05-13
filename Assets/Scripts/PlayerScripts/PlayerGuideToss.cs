using UnityEngine;
using System.Collections;

public class PlayerGuideToss : MonoBehaviour {
    private const float MIN_DIST = 17f;                 // <-- Min distance (or considered 'strength') of throwing device
    private const float MAX_DIST = 21f;                 // <-- Max distance (or considered 'strength') of throwing device
    private const float MAX_ANGL = Mathf.PI / 2;        // <-- Max angle player can throw upward and downward
    private const float GRAV_WEIGHT = 6f;               // <-- Weight against rigidbody2d gravity scale

    private PlayerBase m_base;
    private Rigidbody2D m_body;
    private GameObject m_guide;
    private GameObject player;

    //Vectors to throw device in correct direction
    public Vector2 m_rightFacingSpawn;
    public Vector2 m_leftFacingSpawn;

    //Never initialized?
    public float m_centerOffset;

    //Horizontal velocity
    public float m_horVel = 10f;

    //Upward velocity
    public float m_upVel = 2f;

    //Weight to velocities dependent on cursor location
    public float m_upWeight;
    public float m_horWeight;

    private bool isHoldingDown;
    private float additionalDist;

    // Use this for initialization
    void Start () {
        player  = GameObject.FindGameObjectWithTag("Player");
        m_guide = Resources.Load("Guide") as GameObject;
        m_body  = player.GetComponent<Rigidbody2D>();
        m_base  = player.GetComponent<PlayerBase>();

        isHoldingDown = false;
        additionalDist = MIN_DIST;
	}
	
	// Update is called once per frame
	void Update () {
        float tempX, tempY;
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = player.transform.position;



        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) && !isHoldingDown)
            isHoldingDown = true;

        if ((Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1)) && isHoldingDown)
        { isHoldingDown = false; additionalDist = MIN_DIST; }

        if (isHoldingDown)
            additionalDist = MIN_DIST + additionalDist < MAX_DIST ? MIN_DIST + Time.deltaTime * MAX_DIST / 1.5f : MAX_DIST;



        if (m_base.playerForces.absHor > 0)
        {
            //Get mouse position relative to player's X and Y coordinates
            tempX = cursor.x - pos.x >= 0 ? cursor.x - pos.x : 0;
            tempY = m_base.playerForces.absUp > 0 ? cursor.y - pos.y : pos.y - cursor.y;
        }
        //Facing left cause
        else
        {
            //Get mouse position relative to player's X and Y coordinate
            tempX = pos.x - cursor.x >= 0 ? pos.x - cursor.x : 0;
            tempY = m_base.playerForces.absUp > 0 ? cursor.y - pos.y : pos.y - cursor.y;
        }

        float theta = Mathf.Atan(tempY / tempX);

        //Set maximum and minimum angle for theta
        if (theta > MAX_ANGL) theta = MAX_ANGL;
        if (theta < -1 * MAX_ANGL) theta = -1 * MAX_ANGL;

        //Set horVel and upVel to have magnitude of MAX_DIST based on theta
        m_upVel  = additionalDist * Mathf.Sin(theta);
        m_horVel = additionalDist * Mathf.Cos(theta);

        //Increment additional distance for velocity to where 2 second hold down gives max extra distance
    }

    public void throwGuide(float _polarity)
    {
        Vector3 spawn;
        Vector2 vel = new Vector2(m_horVel + m_body.velocity.x, m_upVel + m_body.velocity.y);
        //Vector2 vel = new Vector2(1, 1);

        if (m_base.playerForces.absHor == 1)
        { //facing right
            if (m_base.playerForces.absUp == 1)
            {
                spawn = new Vector3(
                    this.transform.position.x + m_rightFacingSpawn.x,
                    this.transform.position.y + m_centerOffset, 0f);

                //Move forward * forward weight, Move up * upward weight
                vel = new Vector2(m_horVel + m_body.velocity.x, m_upVel + m_body.velocity.y);
                //Debug.Log("throwing from normal up and right");
            }
            else
            {
                spawn = new Vector3(
                    this.transform.position.x + m_rightFacingSpawn.x,
                    this.transform.position.y - m_centerOffset, 0f);

                //Move forward * forward weight, Move up * upward weight
                vel = new Vector2(m_horVel + m_body.velocity.x, -1 * m_upVel + m_body.velocity.y);
                //Debug.Log("throwing from normal down and right");
            }
        }
        else
        { //facing left
            if (m_base.playerForces.absUp == 1)
            {
                spawn = new Vector3(
                    this.transform.position.x + m_leftFacingSpawn.x,
                    this.transform.position.y + m_centerOffset, 0f);

                //Move forward * forward weight, Move up * upward weight
                vel = new Vector2(m_horVel * -1 + m_body.velocity.x, m_upVel + m_body.velocity.y);
            }
            else
            {
                spawn = new Vector3(
                    this.transform.position.x + m_leftFacingSpawn.x,
                    this.transform.position.y - m_centerOffset, 0f);

                //Move forward * forward weight, Move up * upward weight
                vel = new Vector2(m_horVel * -1 + m_body.velocity.x, m_upVel * -1 + m_body.velocity.y);
            }
        }

        GameObject gid = (GameObject)Instantiate(m_guide, spawn, transform.rotation);
        Rigidbody2D tempRB = gid.GetComponent<Rigidbody2D>();
        tempRB.velocity = vel;
        //tempRB.GetComponent<Collider2D>().enabled = false;
        tempRB.gravityScale = GetComponent<Rigidbody2D>().gravityScale * GRAV_WEIGHT;
        //tempRB.GetComponentInChildren<Guide>().//setPolarity(_polarity);
    }
}
