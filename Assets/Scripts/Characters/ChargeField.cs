using UnityEngine;
using System.Collections;

public class ChargeField : MonoBehaviour {

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

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.attachedRigidbody) {
                other.attachedRigidbody.AddForce(
                    this.transform.up * m_fieldStrength
                    * other.GetComponent<PlayerBase>().m_PlayerPolarity,
                    ForceMode2D.Force);
            }
            Debug.Log("COLLIDED");
        }
    }

    public int isPosOrNeg() {
        if (m_fieldStrength < 0) {
            return -1;
        } else {
            return 1;
        }
    }
}
