using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour {
    private Text healthText;
    PlayerBase m_playerStats;
    private int m_health;  

    void Start () {
        m_health = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_playerStats = player.GetComponent<PlayerBase>();
        healthText = GetComponent<Text>() as Text;
	}

    void Update() {
        m_health = m_playerStats.Health();
        healthText.text = m_health.ToString();
    }
}
