using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {
    
    //negative or positive scalar that will control
    //how hard player is pushed or pulled
    float m_fieldStrength = 20f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setPolarity(float _polarity) {
        m_fieldStrength = _polarity;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.attachedRigidbody) {
                other.attachedRigidbody.AddForce(
                    this.transform.up * m_fieldStrength, 
                    ForceMode2D.Force);
            }
        }
        
    }
}
