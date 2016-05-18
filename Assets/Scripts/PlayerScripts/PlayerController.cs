using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    PlayerBase m_base;
    //last recorded time a device was thrown
    float m_lastThrow;
    //time between throws
    public float m_reloadTime = 5f;
    public float m_guideWait = 0.1f;
    public float m_prevGuide;
    float m_timeSinceLastThrow;
    //set to r if the device to be thrown is positive
    //set to e if the device to be throw is negative
    //set to n if nothing is in the process of being thrown
    char m_devToBeTossed;

    bool ericSettings = true;

    bool m_lockedControls = false;
    bool m_disableControl = false;
    bool hideGuide = true;

    float m_timeControlsLocked;
    float m_TimeUntilUnlocked;




    void Start() {
        m_base = this.GetComponent<PlayerBase>();
        m_devToBeTossed = 'n';
        m_timeSinceLastThrow = m_prevGuide = Time.time;
    }

    void FixedUpdate() {
        if (!m_disableControl) {
            runningInput();
        }
    }

    void Update() {
        if (!m_disableControl) {
            //if (Input.GetKeyDown(KeyCode.Space) ) {
            //   // m_base.playerForces.jump();
            //}
            if (!m_lockedControls) {
                deviceThrowInput();
                runningInput();
                guideInput();
            } else {
                if (Time.time > m_TimeUntilUnlocked)
                    m_lockedControls = false;
            }
        }
    }

    void guideInput() {
        if (Input.GetKeyDown(KeyCode.G))
            hideGuide = !hideGuide;

        if ((Time.time >= m_prevGuide + m_guideWait) && !hideGuide)
        {
            m_base.playerGuide.throwGuide(10f);
            m_prevGuide = Time.time;
        }
    }

    void runningInput() {
        float run = Input.GetAxis("Horizontal");
        m_base.playerForces.run(run);
    }

    void deviceThrowInput() {

        if (Time.time > m_timeSinceLastThrow + m_reloadTime) {
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
                    m_timeSinceLastThrow = Time.time;
                }

                if (Input.GetKeyUp(KeyCode.E) && m_devToBeTossed == 'e') {
                    m_base.playerDevice.throwDevice(10f);
                    m_devToBeTossed = 'n';
                    m_timeSinceLastThrow = Time.time;
                }
            }
            else {
                if (m_devToBeTossed == 'n') {
                    if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Mouse0)) {
                        m_base.playerDevice.startTimer();
                        m_devToBeTossed = 'r';
                    }
                    else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Mouse1)) {
                        m_base.playerDevice.startTimer();
                        m_devToBeTossed = 'e';
                    }
                }

                if ((Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.Mouse0)) && m_devToBeTossed == 'r') {
                    m_base.playerDevice.throwDevice(-.25f);
                    m_devToBeTossed = 'n';
                    m_timeSinceLastThrow = Time.time;
                }

                if ((Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.Mouse1)) && m_devToBeTossed == 'e') {
                    m_base.playerDevice.throwDevice(.25f);
                    m_devToBeTossed = 'n';
                    m_timeSinceLastThrow = Time.time;
                }
            }
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

    public void disableControl() {
        m_disableControl = true;
    }

    public void enableControl() {
        m_disableControl = false;
    }
}
