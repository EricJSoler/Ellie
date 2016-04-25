using UnityEngine;
using System.Collections;

public class Device : MonoBehaviour {

    
    Rigidbody2D m_rigidBody;

    Vector2 normal;

    public float lifeSpan = 60f;
    private float spawnTime;
    void Start() {
        m_rigidBody = this.GetComponent<Rigidbody2D>();
        spawnTime = Time.time;
    }
    // Update is called once per frame
    void Update () {
        if (normal != null) {
         //   Debug.DrawRay(transform.position, normal, Color.blue);
        }

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
        }
    }
}
