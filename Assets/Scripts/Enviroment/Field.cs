using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour
{

    //negative or positive scalar that will control
    //how hard player is pushed or pulled
    public float m_fieldStrength;

    void Start() {
    }

    void Update() {

    }

    public void setPolarity(float _polarity) {
        m_fieldStrength = _polarity;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Vector2 addVel = this.transform.up * m_fieldStrength
                * other.GetComponent<PlayerBase>().m_PlayerPolarity;
            other.GetComponent<PlayerBase>().playerForces.addToDelta(addVel);
        }
        if (other.gameObject.tag == "Enemy") {
            Vector2 addVel = this.transform.up * m_fieldStrength
                * other.GetComponent<EnemyBase>().m_PlayerPolarity;
            other.GetComponent<EnemyBase>().playerForces.addToDelta(addVel);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Vector2 addVel = this.transform.up * m_fieldStrength
    * other.GetComponent<PlayerBase>().m_PlayerPolarity;
            other.GetComponent<PlayerBase>().playerForces.addToDelta(addVel *-1);            
        }
        else if (other.gameObject.tag == "Enemy") {
            Vector2 addVel = this.transform.up * m_fieldStrength
    * other.GetComponent<EnemyBase>().m_PlayerPolarity;
            other.GetComponent<EnemyBase>().playerForces.addToDelta(addVel * -1);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        //if (other.gameObject.tag == "Player") {
        //    if (other.attachedRigidbody) {
        //        Vector2 addVel = this.transform.up * m_fieldStrength
        //            * other.GetComponent<PlayerBase>().m_PlayerPolarity;
        //        other.attachedRigidbody.velocity = new Vector2(
        //            other.attachedRigidbody.velocity.x +
        //            addVel.x, other.attachedRigidbody.velocity.y +
        //            addVel.y);
        //        //other.attachedRigidbody.AddForce(
        //        //    this.transform.up * m_fieldStrength
        //        //    * other.GetComponent<PlayerBase>().m_PlayerPolarity, 
        //        //    ForceMode2D.Force);
        //    }
        //}
        //if (other.gameObject.tag == "Enemy") {
        //    if (other.attachedRigidbody) {
        //        other.attachedRigidbody.AddForce(
        //            this.transform.up * m_fieldStrength
        //            * other.GetComponent<EnemyBase>().m_PlayerPolarity,
        //            ForceMode2D.Force);

        //        //Debug.Log("here:");
        //    }
        //}
    }

    public int isPosOrNeg() {
        if (m_fieldStrength < 0) {
            return -1;
        }
        else {
            return 1;
        }
    }
}
