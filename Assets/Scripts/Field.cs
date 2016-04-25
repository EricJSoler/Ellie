using UnityEngine;
using System.Collections;

public class Field : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.attachedRigidbody) {
                other.attachedRigidbody.velocity =
                    new Vector2(this.transform.up.x + other.attachedRigidbody.velocity.x,
                    this.transform.up.y + other.attachedRigidbody.velocity.y);
            }
            Debug.Log("Trigger stay");
            Debug.DrawRay(this.transform.position, this.transform.up, Color.green);
        }
        
    }
}
