using UnityEngine;
using System.Collections;

public class ProttieController : MonoBehaviour {

    ProttieBase m_base;

    void Start() {
        m_base = this.GetComponent<ProttieBase>();
    }

    // Update is called once per frame
    void Update() {
    }
}
