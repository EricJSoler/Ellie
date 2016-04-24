using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    Rigidbody2D m_rigidBody;
    public float m_moveSpeed = 5f;
    public float jumpHeight = 5f;
    // Use this for initialization

    //Detecting Ground 
    #region detectingGround
    public LayerMask worldGround;
    public LayerMask jumpAbleSurface;
    public bool grounded;
    public Transform groundCheck;
    public float groundCheckRadius;

    #endregion
    int upDirection = 1;

    void Start () {
        m_rigidBody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            if (upDirection == -1) {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, jumpHeight * -1);
            }
            else {
                m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x, jumpHeight);
            }
        }
        int turnMultiplier = 1;
        //if(upDirection == -1) {
        //    turnMultiplier = -1;
        //}

        if (Input.GetKey(KeyCode.D)) {
            m_rigidBody.velocity = new Vector2(m_moveSpeed * turnMultiplier, m_rigidBody.velocity.y);
        }
        if (Input.GetKey(KeyCode.A)) {
            m_rigidBody.velocity = new Vector2(turnMultiplier* -1 * m_moveSpeed, m_rigidBody.velocity.y);
        }

        //if(transform.up.y * m_rigidBody.velocity.y < 0) {

        //}
    }

    void FixedUpdate() {

        checkIfOnJumpableSurface();
        checkWorldFieldPull();
    }

    void checkIfOnJumpableSurface() {
        if( Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, jumpAbleSurface)) {
            grounded = true;
        }
        else if(Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, worldGround)) {
            grounded = true;
        }
        else {
            grounded = false;
        }
    }

    void LateUpdate() {
        Debug.Log(m_rigidBody.velocity);
    }
    

    //bug preventing you from falling off the map unless we make like a dark worldbound
    //layer that makes you die
    void checkWorldFieldPull() {
        
        RaycastHit2D hitUp;
        RaycastHit2D hitDown;
        Vector2 up = transform.up;
        Vector2 down = new Vector2(transform.up.x, transform.up.y * -1);
        float distanceToUp = Mathf.Infinity;
        float distanceToDown = Mathf.Infinity;

        hitUp = Physics2D.Raycast(transform.position, up, Mathf.Infinity, worldGround);
        hitDown = Physics2D.Raycast(transform.position, down, Mathf.Infinity, worldGround);
        distanceToDown = Vector2.Distance(transform.position, hitDown.transform.position);
        distanceToUp = Vector2.Distance(transform.position, hitUp.transform.position);

        if (distanceToUp < distanceToDown) {
            m_rigidBody.gravityScale *= -1;
            transform.Rotate(transform.forward, 180f); //make this apretty lerp 
            upDirection *= -1;
            
        }
    }
}
