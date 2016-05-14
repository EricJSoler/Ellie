using UnityEngine;
using System.Collections;

public class PlayerDevToss : MonoBehaviour {

    //Constants with arbitrary values used to set 'Look Good' limits on device velocity
    private const float MIN_DIST = 15f;                 // <-- Min distance (or considered 'strength') of throwing device
    private const float MAX_DIST = 20f;                 // <-- Max distance (or considered 'strength') of throwing device
    private const float MAX_ANGL = Mathf.PI / 2;        // <-- Max angle player can throw upward and downward
    private const float GRAV_WEIGHT = 6f;               // <-- Weight against rigidbody2d gravity scale

    private float additionalDist;

    PlayerBase m_base;
    Rigidbody2D m_body;
    public string devicePrefab = "device";
    GameObject m_device;
    public GameObject player;

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

	// Use this for initialization
	void Start () {
        m_base   = this.GetComponent<PlayerBase>();
        m_device = Resources.Load(devicePrefab) as GameObject;
        m_body   = this.GetComponent<Rigidbody2D>();

        //Intialize additional distance to be added to the throw distance
        additionalDist = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () //MAY NEED TO CHANGE tempX & tempY CALCULATION
    {

        float tempX, tempY;
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = player.transform.position;

        //Facing right case
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
        //Get angle between player and mouse pointer
        float theta = Mathf.Atan(tempY / tempX);

        //Set maximum and minimum angle for theta
        if (theta > MAX_ANGL) theta = MAX_ANGL;
        if (theta < -1 * MAX_ANGL) theta = -1 * MAX_ANGL;

        //Set horVel and upVel to have magnitude of MAX_DIST based on theta
        m_upVel = getDist() * Mathf.Sin(theta);
        m_horVel = getDist() * Mathf.Cos(theta);

        //Increment additional distance for velocity to where 2 second hold down gives max extra distance
        additionalDist += Time.deltaTime * MAX_DIST / 1.5f;
    }

    public void determineVelocity()
    {

    }

    //Reset additionalDist back to 0 and prepare for key release
    public void startTimer() { additionalDist = 0; }

    //Determine additional velocity based on duration of key being held down
    private float getDist() { return MIN_DIST + additionalDist < MAX_DIST ? MIN_DIST + additionalDist : MAX_DIST; }

    //TODO: Make throw device less convoluted
    public void throwDevice(float _polarity) {
        Vector3 spawn;
        Vector2 vel = new Vector2(m_horVel + m_body.velocity.x, m_upVel + m_body.velocity.y);
        
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

        //vel = new Vector2(m_horVel + m_body.velocity.x, m_upVel + m_body.velocity.y);

        GameObject dev = (GameObject)Instantiate(m_device,
            spawn, transform.rotation);

        Rigidbody2D rb = dev.GetComponent<Rigidbody2D>();
        rb.velocity = vel;
        rb.gravityScale = this.GetComponent<Rigidbody2D>().gravityScale * GRAV_WEIGHT;
        //adding polarity to the instantiation of the field
        dev.GetComponent<Device>().setPolarity(_polarity);

    }
}