using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerForces : MonoBehaviour
{
    #region speedStuff
    Vector2 ext_field;
    #endregion

    #region MovementSettings
    float m_horVelMax = 5f;
    float m_speedIncrement = 2f;
    float m_airBorneSlow = .5f;
    #endregion

    PlayerBase m_base;
    Rigidbody2D m_rigidBody;
    //Detecting Ground 
    #region DetectingGround
    public Transform m_groundCheck;
    public bool m_grounded;
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

    int m_lastMovedDirection = 1;

    Stack<Vector2> m_checkPoints = new Stack<Vector2>();

    public Vector2 lastCheckPoint {
        get {
            return m_checkPoints.Peek();
        }
    }
    void Start() {
        m_rigidBody = this.GetComponent<Rigidbody2D>();
        m_base = this.GetComponent<PlayerBase>();
        ext_field = new Vector2(0f, 0f);
    }

    void Update() {
    }

    private void checkYField() {
        m_rigidBody.velocity = new Vector2(m_rigidBody.velocity.x,
            m_rigidBody.velocity.y + ext_field.y);
    }

    void FixedUpdate() {
        //      if (!m_base.relocationPlayer) { //I FORGOT IF THIS IS SUPOOSSSED TO BE HERE OR NOT
        checkIfOnJumpableSurface();
        checkWorldFieldPull();
        checkYField();
        //Debug.Log(m_rigidBody.velocity);       

        //   Debug.DrawRay(transform.position, new Vector3(absHor, 0f), Color.red);
        Debug.DrawRay(transform.position, new Vector3(ext_field.x, ext_field.y), Color.blue);
    }

    public void storeCurrentCheckPoint() {
        m_checkPoints.Push(transform.position);
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
            if (absUp < 0) {
                m_rigidBody.velocity = new Vector2(
                    m_rigidBody.velocity.x, -10f);


            }
            else {
                m_rigidBody.velocity = new Vector2(
                    m_rigidBody.velocity.x, 10f);
            }
        }
    }

    public void run(float _input) {

        int _direction = 0;
        if (_input > 0) {
            _direction = 1;
        }
        else if (_input < 0) {
            _direction = -1;
        }

        if (_direction != 0) //{
            m_lastMovedDirection = _direction;
        float newXVel = m_rigidBody.velocity.x + ext_field.x + m_speedIncrement * _input;
        Debug.Log(newXVel);
        newXVel = Mathf.Clamp(newXVel, -1 * m_horVelMax + ext_field.x, m_horVelMax + ext_field.x);
        m_rigidBody.velocity = new Vector2(newXVel, m_rigidBody.velocity.y);
        //}
        //else {
        //    float m_speedDecrement = .5f;
        //    if (m_rigidBody.velocity.x < 0) {
        //        float newXVel = m_rigidBody.velocity.x + ext_field.x + m_speedDecrement;
        //        newXVel = Mathf.Clamp(newXVel, m_rigidBody.velocity.x, ext_field.x);
        //        m_rigidBody.velocity = new Vector2(newXVel, m_rigidBody.velocity.y);
        //        Debug.Log(newXVel);
        //    }
        //    else {
        //        float newXVel = m_rigidBody.velocity.x + ext_field.x - m_speedDecrement;
        //        newXVel = Mathf.Clamp(newXVel, ext_field.x,  m_rigidBody.velocity.x);
        //        m_rigidBody.velocity = new Vector2(newXVel, m_rigidBody.velocity.y);
        //        Debug.Log(newXVel);
        //    }





    }

    public int absUp {
        get {
            if (transform.up.y < 0) {
                return -1;
            }
            else {
                return 1;
            }
        }
    }

    public int absHor {
        get {
            return m_lastMovedDirection;
        }
    }


    public void addToDelta(Vector2 amount) {
        ext_field += amount;
    }


}
