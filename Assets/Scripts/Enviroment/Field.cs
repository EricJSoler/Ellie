using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Field : MonoBehaviour
{

    //negative or positive scalar that will control
    //how hard player is pushed or pulled
    public float m_fieldStrength;

    struct GODeltaTuple
    {
        public GameObject obj;
        public Vector2 lastDelta;
    }

    List<GODeltaTuple> aplyingTo = new List<GODeltaTuple>();


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
            GODeltaTuple tup = new GODeltaTuple();
            tup.obj = other.gameObject;
            tup.lastDelta = addVel;
            aplyingTo.Add(tup);
        }
        if (other.gameObject.tag == "Enemy") {
            Vector2 addVel = this.transform.up * m_fieldStrength
                * other.GetComponent<EnemyBase>().m_PlayerPolarity;
            other.GetComponent<EnemyBase>().playerForces.addToDelta(addVel);
            GODeltaTuple tup = new GODeltaTuple();
            tup.obj = other.gameObject;
            tup.lastDelta = addVel;
            aplyingTo.Add(tup);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            Vector2 addVel = this.transform.up * m_fieldStrength
    * other.GetComponent<PlayerBase>().m_PlayerPolarity;
            other.GetComponent<PlayerBase>().playerForces.addToDelta(addVel *-1);
            int remove = -1;
            for(int i = 0; i < aplyingTo.Count; i++) {
                if(aplyingTo[i].obj.GetInstanceID() == other.gameObject.GetInstanceID()) {
                    remove = i;
                    break;
                }
            }
            if(remove != -1) {
                aplyingTo.RemoveAt(remove);
            }
        }
        else if (other.gameObject.tag == "Enemy") {
            Vector2 addVel = this.transform.up * m_fieldStrength
    * other.GetComponent<EnemyBase>().m_PlayerPolarity;
            other.GetComponent<EnemyBase>().playerForces.addToDelta(addVel * -1);
            int remove = -1;
            for (int i = 0; i < aplyingTo.Count; i++) {
                if (aplyingTo[i].obj.GetInstanceID() == other.gameObject.GetInstanceID()) {
                    remove = i;
                    break;
                }
            }
            if (remove != -1) {
                aplyingTo.RemoveAt(remove);
            }
        }
    }

    void OnDestroy() {
        foreach(GODeltaTuple ele in aplyingTo) {
            if (ele.obj.tag == "Player") {
                ele.obj.GetComponent<PlayerBase>().playerForces.addToDelta(ele.lastDelta * -1);
            }
            else if(ele.obj.tag == "Enemy") {
                ele.obj.GetComponent<EnemyBase>().playerForces.addToDelta(ele.lastDelta * -1);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) {
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
