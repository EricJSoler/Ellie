using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHealthBolts : MonoBehaviour {
    PlayerBase m_playerStats;
    public static int MaxBolts = 5;

    private int m_health;
    public GameObject[] bolts = new GameObject[MaxBolts];

    // Use this for initialization
    void Start () {
        m_health = 0;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        m_playerStats = player.GetComponent<PlayerBase>();

        for (int i = 0; i < MaxBolts; i++) {
            bolts[i].SetActive(false);
        }

    }
	
	// Update is called once per frame
	void Update () {
        m_health = m_playerStats.Health();
        turnOnBolts(m_health);
	}

    private void turnOnBolts(int health) {
        for (int i = 0; i < health; i++) {
            bolts[i].SetActive(true);    
        }

        for (int i = health; i < MaxBolts; i++) {
            bolts[i].SetActive(false);
        }
             
    }

    
}
