using UnityEngine;
using System.Collections;

public class EddieField : MonoBehaviour {

    public float m_fieldStrength;

    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }

    public void setPolarity(float _polarity) {
        m_fieldStrength = _polarity;
    }

    void OnTriggerStay2D(Collider2D other) {
        /*if (other.gameObject.tag == "Player") {
            if (other.attachedRigidbody) {
                other.attachedRigidbody.AddForce(
                    this.transform.right * m_fieldStrength
                    * other.GetComponent<PlayerBase>().m_PlayerPolarity,
                    ForceMode2D.Force);
            }
        }*/
    }

    public int isPosOrNeg() {
        if (m_fieldStrength < 0) {
            return -1;
        } else {
            return 1;
        }
    }
}
