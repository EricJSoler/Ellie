using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHealthBolts : MonoBehaviour {
    PlayerBase m_playerStats;
    private int m_health;

    // Use this for initialization
    void Start () {
        m_health = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_playerStats = player.GetComponent<PlayerBase>();

    }
	
	// Update is called once per frame
	void Update () {
        m_health = m_health = m_playerStats.Health();
        for (int i = 0; i < m_health; i++) {
            //create bolts and render
        }
	
	}
}
