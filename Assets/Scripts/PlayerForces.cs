using UnityEngine;
using System.Collections;

public class PlayerForces : MonoBehaviour {

    PlayerBase m_base;
    Rigidbody2D m_rigidBody;
    //Detecting Ground 
    #region DetectingGround
    public Transform m_groundCheck;
    bool m_grounded;
    float m_groundCheckRadius = .1f;

    public bool grounded {
        get {
            return m_grounded;
        }
    }
    #endregion
    //Detecting Magnetic Fields
    #region DetectingMagneticFields
    public LayerMask worldGround;
    public LayerMask jumpAbleSurface;
    #endregion
    #region MovementSettings
    float m_horVel = 10f;
    float m_airBorneSlow = .75f;
    #endregion


    void Start () {
        m_rigidBody = this.GetComponent<Rigidbody2D>();
        m_base = this.GetComponent<PlayerBase>();
    }
	
	void Update () {
	    
	}

    void FixedUpdate() {
        checkIfOnJumpableSurface();
        checkWorldFieldPull();
    }

    void checkIfOnJumpableSurface() {
        if (Physics2D.OverlapCircle(m_groundCheck.position, 
                m_groundCheckRadius, jumpAbleSurface)) {
            m_grounded = true;
        }
        else if (Physics2D.OverlapCircle(m_groundCheck.position, 
                m_groundCheckRadius, worldGround)) {
            m_grounded = true;
        }
        else {
            m_grounded = false;
        }
    }

    void checkWorldFieldPull() {
        RaycastHit2D hitUp;
        RaycastHit2D hitDown;
        Vector2 up = transform.up;
        Vector2 down = new Vector2(transform.up.x, transform.up.y * -1);
        float distanceToUp = Mathf.Infinity;
        float distanceToDown = Mathf.Infinity;

        hitUp = Physics2D.Raycast(transform.position, up, Mathf.Infinity, worldGround);
        hitDown = Physics2D.Raycast(transform.position, down, Mathf.Infinity, worldGround);

        if (hitDown)
            distanceToDown = Vector2.Distance(transform.position, hitDown.transform.position);
        if (hitUp)
            distanceToUp = Vector2.Distance(transform.position, hitUp.transform.position);

        if (distanceToUp < distanceToDown) {
            m_rigidBody.gravityScale *= -1;
            m_base.playerAnim.rotatePlayer();
        }
    }

    //will be replaced
    public void jump() {
        if (grounded) {
            Debug.Log(transform.up);
            if (transform.up.y < 0) {
                m_rigidBody.velocity = new Vector2(
                    m_rigidBody.velocity.x, -10f);
                //m_rigidBody.AddForce(new Vector2(0f, -10f));
            }
            else {
                m_rigidBody.velocity = new Vector2(
                    m_rigidBody.velocity.x, 10f);
                //m_rigidBody.AddForce(new Vector2(0f, 10f));
            }
        }
    }

    public void run(int _direction) {
        Vector2 newVel;
        if (grounded) {
            newVel = new Vector2(m_horVel * _direction,
                m_rigidBody.velocity.y);
        }
        else {
            newVel = new Vector2(m_airBorneSlow * m_horVel 
                * _direction, m_rigidBody.velocity.y);
        }
        m_rigidBody.velocity = newVel; 
    }
}
