using UnityEngine;
using System.Collections;

public class EnemyBase : MonoBehaviour {

    #region PlayerComponents
    PlayerAnim m_anim;
    EnemyController m_controller;
    EnemyForces m_forces;
    PlayerDevToss m_dev;
    PlayerStats m_stats;

    public PlayerAnim playerAnim {
        get {
            return m_anim;
        }
    }

    public EnemyController playerController {
        get {
            return m_controller;
        }
    }

    public EnemyForces playerForces {
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

    //set this depending on the character
    public int m_PlayerPolarity = -1;
    
    
    void Start () {
        m_anim = GetComponent<PlayerAnim>();
        m_controller = GetComponent<EnemyController>();
        m_forces = GetComponent<EnemyForces>();
        m_dev = GetComponent<PlayerDevToss>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void killme() {
        GameObject.Find("LevelManager").GetComponent<LevelManager>().playEnemySounds(); // SFX
        Destroy(gameObject);
    }
}
