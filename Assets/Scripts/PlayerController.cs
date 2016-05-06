using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    PlayerBase m_base;
    //last recorded time a device was thrown
    float m_lastThrow;
    //time between throws
    public float m_reloadTime = 2f;
    //set to r if the device to be thrown is positive
    //set to e if the device to be throw is negative
    //set to n if nothing is in the process of being thrown
    char m_devToBeTossed;

    bool ericSettings = true;

    bool m_lockedControls = false;

    float m_timeControlsLocked;
    float m_TimeUntilUnlocked;

    void Start () {
        m_base = this.GetComponent<PlayerBase>();
        m_devToBeTossed = 'n';
    }
	
	// Update is called once per frame
	void Update () {

        //if (Input.GetKeyDown(KeyCode.Space) ) {
        //   // m_base.playerForces.jump();
        //}
        if (!m_lockedControls) {
            if (!ericSettings) {
                if (m_devToBeTossed == 'n') {
                    if (Input.GetKeyDown(KeyCode.R)) {
                        m_base.playerDevice.startTimer();
                        m_devToBeTossed = 'r';
                    }
                    else if (Input.GetKeyDown(KeyCode.E)) {
                        m_base.playerDevice.startTimer();
                        m_devToBeTossed = 'e';
                    }
                }

                if (Input.GetKeyUp(KeyCode.R) && m_devToBeTossed == 'r') {
                    m_base.playerDevice.throwDevice(-10f);
                    m_devToBeTossed = 'n';
                }

                if (Input.GetKeyUp(KeyCode.E) && m_devToBeTossed == 'e') {
                    m_base.playerDevice.throwDevice(10f);
                    m_devToBeTossed = 'n';
                }
            }
            else {
                if (m_devToBeTossed == 'n') {
                    if (Input.GetKeyDown(KeyCode.UpArrow)) {
                        m_base.playerDevice.startTimer();
                        m_devToBeTossed = 'r';
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                        m_base.playerDevice.startTimer();
                        m_devToBeTossed = 'e';
                    }
                }

                if (Input.GetKeyUp(KeyCode.UpArrow) && m_devToBeTossed == 'r') {
                    m_base.playerDevice.throwDevice(-10f);
                    m_devToBeTossed = 'n';
                }

                if (Input.GetKeyUp(KeyCode.DownArrow) && m_devToBeTossed == 'e') {
                    m_base.playerDevice.throwDevice(10f);
                    m_devToBeTossed = 'n';
                }
            }


            float run = Input.GetAxis("Horizontal");
            if (run > 0) {
                m_base.playerForces.run(1);
            }
            else if (run < 0) {
                m_base.playerForces.run(-1);
            }
        }
        else {
            if(Time.time > m_TimeUntilUnlocked)
                m_lockedControls = false;
        }
    }

    //locks the controls for the time
    public void lockControls(float time) {
        m_lockedControls = true;
        m_timeControlsLocked = Time.time;
        m_TimeUntilUnlocked = m_timeControlsLocked + time;
    }

    public void unlockControls() {
        m_lockedControls = false;
    }
}
