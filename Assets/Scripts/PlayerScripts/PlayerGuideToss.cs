using UnityEngine;
using System.Collections;

public class PlayerGuideToss : MonoBehaviour {



    public const float MAGNITUDE_SCALE = 2f;


    #region Field Declaration
    private PlayerBase   m_base;
    private GameObject   player;
    private GameObject   guide;
    private GameObject   guide1;
    private GameObject   guide2;
    public LineRenderer  line;
    private Material     line_mat;
    private Vector2      pos1;
    private Vector2      pos2;
    private Vector3      glb_vect1;
    private Vector3      glb_vect2;
    private float        additionalDist;
    private float        prev_additionalDist;
    private float        curr_additionalDist;
    private bool         canDelete;
    private float        offset_hor;
    private float        offset_vert;
    public Color         lineColor;
    private float        lineA;
    private bool         shouldMute;
    PlayerController pController;
    #endregion


    void Start()
    {
        #region Field Instantiation
        player         = GameObject.FindGameObjectWithTag("Player");
        line_mat       = Resources.Load("line_mat") as Material;
        m_base         = player.GetComponent<PlayerBase>();
        guide          = Resources.Load("Guide") as GameObject;
        line           = player.GetComponent<LineRenderer>();
        additionalDist = 0f;
        pos1 = pos2    = new Vector2();
        canDelete      = false;
        shouldMute     = false;
        offset_hor     = 0.4f;
        offset_vert    = 0.5f;
        glb_vect1      = new Vector3();
        glb_vect2      = new Vector3();
        lineA          = 0.7f;
        pController    = player.GetComponent<PlayerController>();
        #endregion
    }



    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.G))
            shouldMute = !shouldMute;


        if (!shouldMute)
        {
            unhideGuide();
            determineAdditionalDist();
            getAngle();
        }

        else hideGuide();
    }



    private void getAngle()
    {
        float tempX, tempY;
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = player.transform.position;

        //Calculate cursor based on facing direction
        if (m_base.playerForces.absHor > 0)
        {
            tempX = cursor.x - pos.x >= 0 ? cursor.x - pos.x : 0;
            tempY = m_base.playerForces.absUp > 0 ? cursor.y - pos.y : pos.y - cursor.y;
        }
        else
        {
            tempX = pos.x - cursor.x >= 0 ? pos.x - cursor.x : 0;
            tempY = m_base.playerForces.absUp > 0 ? cursor.y - pos.y : pos.y - cursor.y;
        }

        //Get angle between player and mouse pointer
        float theta = Mathf.Atan(tempY / tempX);

        //Set maximum and minimum angle for theta
        if (theta > PlayerDevToss.MAX_ANGL)      theta = PlayerDevToss.MAX_ANGL;
        if (theta < -1 * PlayerDevToss.MAX_ANGL) theta = PlayerDevToss.MAX_ANGL * -1;

        //Set horVel and upVel to have magnitude of MAX_DIST based on theta
        pos2.x = Mathf.Cos(theta);
        pos2.y = Mathf.Sin(theta);

        #region Set Direction Guide Should Go
        if (m_base.playerForces.absHor > 0)
        {
            if (m_base.playerForces.absUp > 0)
            {
                glb_vect1 = new Vector3(
                    player.transform.position.x + offset_hor,
                    player.transform.position.y + offset_vert,
                    0f
                );

                glb_vect2 = new Vector3(
                    player.transform.position.x + pos2.x * MAGNITUDE_SCALE,
                    player.transform.position.y + pos2.y * MAGNITUDE_SCALE,
                    0f
                );
            }
            else
            {
                glb_vect1 = new Vector3(
                    player.transform.position.x + offset_hor,
                    player.transform.position.y - offset_vert,
                    0f
                );

                glb_vect2 = new Vector3(
                    player.transform.position.x + pos2.x * MAGNITUDE_SCALE,
                    player.transform.position.y - pos2.y * MAGNITUDE_SCALE,
                    0f
                );
            }
        }
        else
        {
            if (m_base.playerForces.absUp > 0)
            {
                glb_vect1 = new Vector3(
                    player.transform.position.x - offset_hor,
                    player.transform.position.y + offset_vert,
                    0f
                );

                glb_vect2 = new Vector3(
                    player.transform.position.x - pos2.x * MAGNITUDE_SCALE,
                    player.transform.position.y + pos2.y * MAGNITUDE_SCALE,
                    0f
                );
            }
            else
            {
                glb_vect1 = new Vector3(
                    player.transform.position.x - offset_hor,
                    player.transform.position.y - offset_vert,
                    0f
                );

                glb_vect2 = new Vector3(
                    player.transform.position.x - pos2.x * MAGNITUDE_SCALE,
                    player.transform.position.y - pos2.y * MAGNITUDE_SCALE,
                    0f
                );
            }
            
        }
        #endregion

        line.SetVertexCount(2);
        line.SetPosition(0, glb_vect1);
        line.SetPosition(1, glb_vect2);
        line.SetWidth(0.05f * getDist(), 0.05f);
    }



    //Determines distance object should be thrown
    private float getDist()
    {
        return PlayerDevToss.MIN_DIST + additionalDist < PlayerDevToss.MAX_DIST ?
               PlayerDevToss.MIN_DIST + additionalDist : PlayerDevToss.MAX_DIST ;
    }



    //Determines the additional distance for the throw
    private void determineAdditionalDist()
    {
            if (!pController.hasFired && (Input.GetMouseButton(0) || Input.GetMouseButton(1)))
                additionalDist += Time.fixedDeltaTime * PlayerDevToss.MAX_DIST * 1.5f;
            else if (Input.GetMouseButtonUp(0) && Input.GetMouseButtonUp(1))
                additionalDist = additionalDist - 0.5f > 0 ?
                                 additionalDist - 0.5f : 0;
            else
                additionalDist = additionalDist - 0.5f > 0 ?
                                 additionalDist - 0.5f : 0;
        
    }


    #region Change Guide Visibility
    private void hideGuide()
    {
        line.SetVertexCount(0);
    }



    private void unhideGuide()
    {
        line.SetVertexCount(2);
    }
    #endregion



    #region Old Throw Code
    //   private const float MIN_DIST = 12.4f;               // <-- Min distance (or considered 'strength') of throwing device
    //   private const float MAX_DIST = 15f;                 // <-- Max distance (or considered 'strength') of throwing device
    //   private const float MAX_ANGL = Mathf.PI / 2;        // <-- Max angle player can throw upward and downward
    //   private const float GRAV_WEIGHT = 3f;               // <-- Weight against rigidbody2d gravity scale

    //   private PlayerBase m_base;
    //   private Rigidbody2D m_body;
    //   private GameObject m_guide;
    //   private GameObject player;

    //   //Vectors to throw device in correct direction
    //   public Vector2 m_rightFacingSpawn;
    //   public Vector2 m_leftFacingSpawn;

    //   //Never initialized?
    //   public float m_centerOffset;

    //   //Horizontal velocity
    //   public float m_horVel = 10f;

    //   //Upward velocity
    //   public float m_upVel = 2f;

    //   //Weight to velocities dependent on cursor location
    //   public float m_upWeight;
    //   public float m_horWeight;

    //   private bool isHoldingDown;
    //   private float additionalDist;
    //   private bool hideGuide;

    //   // Use this for initialization
    //   void Start () {
    //       player  = GameObject.FindGameObjectWithTag("Player");
    //       m_guide = Resources.Load("Guide") as GameObject;
    //       m_body  = player.GetComponent<Rigidbody2D>();
    //       m_base  = player.GetComponent<PlayerBase>();

    //       isHoldingDown = false;
    //       additionalDist = MIN_DIST;
    //       //hideGuide = true;
    //}

    //   void changeVisibility()
    //   {
    //       m_guide.GetComponent<Renderer>().enabled = !m_guide.GetComponent<Renderer>().enabled;
    //   }

    //// Update is called once per frame
    //void FixedUpdate () {
    //       float tempX, tempY;
    //       Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //       Vector3 pos = player.transform.position;

    //       //if (Input.GetKeyDown(KeyCode.G))
    //       //    changeVisibility();

    //       //if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1)) && !isHoldingDown)
    //       if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !isHoldingDown)
    //       { isHoldingDown = true; }
    //       //else isHoldingDown = false;

    //       //if ((Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1)) && isHoldingDown)
    //       if ((Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) && isHoldingDown)
    //       { isHoldingDown = false; additionalDist = MIN_DIST; }

    //       if (isHoldingDown)
    //           additionalDist = MIN_DIST + additionalDist < MAX_DIST ? 
    //               MIN_DIST + Time.deltaTime * MAX_DIST / 1.5f : MAX_DIST;



    //       if (m_base.playerForces.absHor > 0)
    //       {
    //           //Get mouse position relative to player's X and Y coordinates
    //           tempX = cursor.x - pos.x >= 0 ? cursor.x - pos.x : 0;
    //           tempY = m_base.playerForces.absUp > 0 ? cursor.y - pos.y : pos.y - cursor.y;
    //       }
    //       //Facing left cause
    //       else
    //       {
    //           //Get mouse position relative to player's X and Y coordinate
    //           tempX = pos.x - cursor.x >= 0 ? pos.x - cursor.x : 0;
    //           tempY = m_base.playerForces.absUp > 0 ? cursor.y - pos.y : pos.y - cursor.y;
    //       }

    //       float theta = Mathf.Atan(tempY / tempX);

    //       //Set maximum and minimum angle for theta
    //       if (theta > MAX_ANGL) theta = MAX_ANGL;
    //       if (theta < -1 * MAX_ANGL) theta = -1 * MAX_ANGL;

    //       //Set horVel and upVel to have magnitude of MAX_DIST based on theta
    //       m_upVel  = additionalDist * Mathf.Sin(theta);
    //       m_horVel = additionalDist * Mathf.Cos(theta);

    //       //Increment additional distance for velocity to where 2 second hold down gives max extra distance
    //   }

    //   public void throwGuide(float _polarity)
    //   {
    //       Vector3 spawn;
    //       Vector2 vel = new Vector2(m_horVel + m_body.velocity.x, m_upVel + m_body.velocity.y);
    //       //Vector2 vel = new Vector2(1, 1);

    //       if (m_base.playerForces.absHor == 1)
    //       { //facing right
    //           if (m_base.playerForces.absUp == 1)
    //           {
    //               spawn = new Vector3(
    //                   this.transform.position.x + m_rightFacingSpawn.x,
    //                   this.transform.position.y + m_centerOffset, 0f);

    //               //Move forward * forward weight, Move up * upward weight
    //               vel = new Vector2(m_horVel + m_body.velocity.x, m_upVel + m_body.velocity.y);
    //               //Debug.Log("throwing from normal up and right");
    //           }
    //           else
    //           {
    //               spawn = new Vector3(
    //                   this.transform.position.x + m_rightFacingSpawn.x,
    //                   this.transform.position.y - m_centerOffset, 0f);

    //               //Move forward * forward weight, Move up * upward weight
    //               vel = new Vector2(m_horVel + m_body.velocity.x, -1 * m_upVel + m_body.velocity.y);
    //               //Debug.Log("throwing from normal down and right");
    //           }
    //       }
    //       else
    //       { //facing left
    //           if (m_base.playerForces.absUp == 1)
    //           {
    //               spawn = new Vector3(
    //                   this.transform.position.x + m_leftFacingSpawn.x,
    //                   this.transform.position.y + m_centerOffset, 0f);

    //               //Move forward * forward weight, Move up * upward weight
    //               vel = new Vector2(m_horVel * -1 + m_body.velocity.x, m_upVel + m_body.velocity.y);
    //           }
    //           else
    //           {
    //               spawn = new Vector3(
    //                   this.transform.position.x + m_leftFacingSpawn.x,
    //                   this.transform.position.y - m_centerOffset, 0f);

    //               //Move forward * forward weight, Move up * upward weight
    //               vel = new Vector2(m_horVel * -1 + m_body.velocity.x, m_upVel * -1 + m_body.velocity.y);
    //           }
    //       }

    //       GameObject gid = (GameObject)Instantiate(m_guide, spawn, transform.rotation);
    //       Rigidbody2D tempRB = gid.GetComponent<Rigidbody2D>();
    //       tempRB.velocity = vel;
    //       //tempRB.GetComponent<Collider2D>().enabled = false;
    //       tempRB.gravityScale = GetComponent<Rigidbody2D>().gravityScale * GRAV_WEIGHT;
    //       //tempRB.GetComponentInChildren<Guide>().//setPolarity(_polarity);
    //   }
    #endregion
}
