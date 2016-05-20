﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {
    public AudioClip spawnSound;
    public AudioClip finalSound;
    public AudioClip pushSound;
    public AudioClip pullSound;
    public AudioClip healthSound;
    public AudioClip hurtSound;
    public AudioClip throwSound;
    public AudioClip enemySound;

    public float sfxVolume= 7f;
    private AudioSource bkMusic;

    public Vector3 m_startSpawn;
    public string playerPrefab = "Ellie";

    private GameObject MainMenuButton;
    private GameObject ResumeButton;


    void Awake() {
        Instantiate((GameObject)Resources.Load(playerPrefab),
            m_startSpawn, Quaternion.identity);

    }
	void Start () {
        bkMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        bkMusic.PlayOneShot(spawnSound, sfxVolume); // SFX

        MainMenuButton = GameObject.Find("MainMenuButton");
        MainMenuButton.SetActive(false);
        ResumeButton = GameObject.Find("ResumeButton");
        ResumeButton.SetActive(false);
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            MainMenuButton.SetActive(true);
            ResumeButton.SetActive(true);
            FindObjectOfType<UITimer>().pause();
            FindObjectOfType<PlayerController>().disableControl();
        }
    }

    public void restartLevel() {
        bkMusic.PlayOneShot(spawnSound, 100f); // SFX
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }

    public void levelCompleted() {
        bkMusic.PlayOneShot(finalSound, sfxVolume);

        float timeCompleted = FindObjectOfType<UITimer>().getTime();
        FindObjectOfType<GlobalManager>().SaveTimeCompleted(timeCompleted);
        Debug.Log(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene(
            (SceneManager.GetActiveScene().buildIndex + 1)
            % (SceneManager.sceneCountInBuildSettings));
    }

    public void resumeLevel() {
        MainMenuButton.SetActive(false);
        ResumeButton.SetActive(false);
        FindObjectOfType<UITimer>().resume();
        FindObjectOfType<PlayerController>().enableControl();
    }

    public void playThrow() {
        bkMusic.PlayOneShot(throwSound, sfxVolume - 2);
    }

    public void playPush() {
        bkMusic.PlayOneShot(pushSound, sfxVolume - 2);
    }

    public void playPull() {
        bkMusic.PlayOneShot(pullSound, sfxVolume - 2);
    }

    public void playHurt() {
        bkMusic.PlayOneShot(hurtSound, sfxVolume - 2);
    }

    public void playExtraHealth() {
        bkMusic.PlayOneShot(healthSound, sfxVolume - 2);
    }

    public void playEnemySounds() {
        bkMusic.PlayOneShot(enemySound, sfxVolume - 2);
    }
}
