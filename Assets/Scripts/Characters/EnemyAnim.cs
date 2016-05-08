using UnityEngine;
using System.Collections;

public class EnemyAnim : MonoBehaviour {

    public GameObject body;
    // Use this for initialization
    PlayerBase m_base;
    void Start() {
        m_base = GetComponent<PlayerBase>();
    }

    // Update is called once per frame
    void Update() {
    }

    public void rotatePlayer() {
        transform.Rotate(transform.forward, 180f); //make this apretty lerp 
    }
}
