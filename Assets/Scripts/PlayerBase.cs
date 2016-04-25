using UnityEngine;
using System.Collections;

public class PlayerBase : MonoBehaviour {


    #region PlayerComponents
    PlayerAnim m_anim;
    PlayerController m_controller;
    PlayerForces m_forces;
    PlayerDevToss m_dev;

    public PlayerAnim playerAnim {
        get {
            return m_anim;
        }
    }

    public PlayerController playerController {
        get {
            return m_controller;
        }
    }

    public PlayerForces playerForces {
        get {
            return m_forces;
        }
    }

    public PlayerDevToss playerDevice {
        get {
            return m_dev;
        }
    }
    #endregion

    void Awake() {
        m_anim = GetComponent<PlayerAnim>();
        m_controller = GetComponent<PlayerController>();
        m_forces = GetComponent<PlayerForces>();
        m_dev = GetComponent<PlayerDevToss>();

    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
