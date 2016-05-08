using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

    //negative or positive scalar that will control
    //how hard player is pushed or pulled
    public float m_fieldStrength;

    //#region EnviromentFieldVariables
    //public GameObject m_particles;
    //public float scale;
    //public bool enviromentField = false;
    //public float baseMag = 10f;
    //public float baseVel = 3.3f;
    //#endregion

    void Start () {
        //if (enviromentField) {
        //        ParticleSystem myParticles = m_particles.GetComponent<ParticleSystem>();
        //        myParticles.startLifetime = myParticles.startLifetime * scale;
        //}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setPolarity(float _polarity) {
        m_fieldStrength = _polarity;
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Eddie") {
            if (other.attachedRigidbody) {
                other.attachedRigidbody.AddForce(
                    this.transform.up * m_fieldStrength
                    * other.GetComponent<PlayerBase>().m_PlayerPolarity, 
                    ForceMode2D.Force);
            }
        }
        if (other.gameObject.tag == "Prottie") {
            if (other.attachedRigidbody) {
                other.attachedRigidbody.AddForce(
                    -1 * this.transform.up * m_fieldStrength
                    * other.GetComponent<PlayerBase>().m_PlayerPolarity,
                    ForceMode2D.Force);
            }
        }
    }

    public int isPosOrNeg() {
        if(m_fieldStrength < 0) {
            return -1;
        }
        else {
            return 1;
        }
    }
}
