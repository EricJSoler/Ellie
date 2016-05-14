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

        //string[] ignoreObjs = { "Player", "Guide", "Nubbie", "Prottie", "Eddie" };
        //foreach (string i in ignoreObjs)
        //    Physics2D.IgnoreCollision(GameObject.FindGameObjectWithTag(i).transform.GetComponent<Collider2D>(), 
        //        transform.GetComponent<Collider2D>());
    }
    // Update is called once per frame
    void Update() {
        if (Time.time > spawnTime + lifeSpan) {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log(other.gameObject.tag);
        // Ignore collision with Nubbie, Prottie, and Eddie
        if (other.gameObject.tag == "Nubbie" || other.gameObject.tag == "Prottie" 
            || other.gameObject.tag == "Eddie" || other.gameObject.tag == "Guide"
            || other.gameObject.tag == "Enemy")
        {
            Physics2D.IgnoreCollision(other.collider, this.GetComponent<Collider2D>());
        }
        else if (other.gameObject.tag != "Player")
        {

            // Device Landing
            m_rigidBody.isKinematic = true;
            normal = other.contacts[0].normal;
            transform.up = normal;
            this.GetComponent<Collider2D>().enabled = false;
            Field myField = this.GetComponentInChildren<Field>();

            // Play Particle System
            if (myField.isPosOrNeg() == 1)
            {
                m_particlesPos.GetComponent<ParticleSystem>().Play();
            }
            else
            {
                m_particlesNeg.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
