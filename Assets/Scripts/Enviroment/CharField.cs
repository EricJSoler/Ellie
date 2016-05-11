using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharField : MonoBehaviour {

    public float m_fieldStrength;

    struct GODeltaTuple
    {
        public GameObject obj;
        public Vector2 lastDelta;
    }

    List<GODeltaTuple> aplyingTo = new List<GODeltaTuple>();


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        applyDeltas();
    }

    private void applyDeltas() {
        List<GODeltaTuple> temp = new List<GODeltaTuple>();

        for(int i = 0; i < aplyingTo.Count; i++) { 
            Vector2 newDir = aplyingTo[i].obj.transform.position
                - this.transform.position;
            newDir.Normalize();
            if (aplyingTo[i].obj.tag == "Player") {
                PlayerBase player = aplyingTo[i].obj.GetComponent<PlayerBase>();
                player.playerForces.addToDelta(aplyingTo[i].lastDelta * -1);
                player.playerForces.addToDelta(newDir * m_fieldStrength * player.m_PlayerPolarity);
                GODeltaTuple updatedDelta = aplyingTo[i];
                updatedDelta.lastDelta = newDir * m_fieldStrength * player.m_PlayerPolarity;
                temp.Add(updatedDelta);
            }
        }
        aplyingTo = temp;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Vector2 newDir = other.transform.position
                - this.transform.position;
            newDir.Normalize();
            PlayerBase player = other.GetComponent<PlayerBase>();
            player.playerForces.addToDelta(newDir * m_fieldStrength * player.m_PlayerPolarity);
            GODeltaTuple tuple = new GODeltaTuple();
            tuple.obj = other.gameObject;
            tuple.lastDelta = newDir * m_fieldStrength * player.m_PlayerPolarity;
            aplyingTo.Add(tuple);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            int remove = -1;
            for (int i = 0; i < aplyingTo.Count; i++) {
                if (aplyingTo[i].obj.GetInstanceID() == other.gameObject.GetInstanceID()) {
                    remove = i;
                    break;
                }
            }
            if(remove != -1) {
                Vector2 lastDelta = aplyingTo[remove].lastDelta;
                aplyingTo.RemoveAt(remove);
                other.GetComponent<PlayerBase>().playerForces.addToDelta(lastDelta * -1);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
    }
}
