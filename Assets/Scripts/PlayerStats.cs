using UnityEngine;
using System.Collections;

public class PlayerStats {

    int m_health;
    public int health {
        get {
            return m_health;
        }
    }

    public PlayerStats(int startHealth) {
        m_health = startHealth;
    }
    //returns true if player is out of health
    public bool takeHit() {
        m_health--;
        if (m_health == 0)
            return true;
        else
            return false;
    }

}
