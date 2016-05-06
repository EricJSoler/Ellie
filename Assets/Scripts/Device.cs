using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {

    
    Rigidbody2D m_rigidBody;

    Vector2 normal;

    public GameObject m_particlesPos;
    public GameObject m_particlesNeg;

    public float lifeSpan = 60f;
    private float spawnTime;
    void Start() {
        m_rigidBody = this.GetComponent<Rigidbody2D>();
        spawnTime = Time.time;
    }
    // Update is called once per frame
    void Update () {
        if(Time.time > spawnTime + lifeSpan) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D a) {
        if (a.gameObject.tag != "Player") {
            m_rigidBody.isKinematic = true;
            normal = a.contacts[0].normal;
            transform.up = normal;
            this.GetComponent<Collider2D>().enabled = false;
            Field myField = this.GetComponentInChildren<Field>();
            if(myField.isPosOrNeg() == 1) {
                m_particlesPos.GetComponent<ParticleSystem>().Play();  //Play();
            }
            else {
                m_particlesNeg.GetComponent<ParticleSystem>().Play();
            }
            
        }
    }
}
