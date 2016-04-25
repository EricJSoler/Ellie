using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    PlayerBase m_base;

    void Start () {
        m_base = this.GetComponent<PlayerBase>();
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.Space) ) {
            m_base.playerForces.jump();
        }

        if (Input.GetKeyDown(KeyCode.R)) {
            m_base.playerDevice.throwDevice(1);
        }

        if (Input.GetKey(KeyCode.D)) {
            m_base.playerForces.run(1);
        }
        if (Input.GetKey(KeyCode.A)) {
            m_base.playerForces.run(-1);
        }
    }
}
