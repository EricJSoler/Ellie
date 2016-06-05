using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

    #region Sound Effects
    public AudioClip spawnSound;
    public AudioClip finalSound;
    public AudioClip pushSound;
    public AudioClip pullSound;
    public AudioClip healthSound;
    public AudioClip hurtSound;
    public AudioClip throwSound;
    public AudioClip enemySound;
    public AudioClip checkptSound;
    #endregion

    public float sfxVolume= 7f;
    private AudioSource bkMusic;

    public Vector3 m_startSpawn;
    public string playerPrefab = "Ellie";

    private GameObject MainMenuButton;
    private GameObject ResumeButton;
    private GameObject EndPanel;
    private HighScores glb_highscore;


    void Awake() {
        if (!PhotonNetwork.connected) {
            Instantiate((GameObject)Resources.Load(playerPrefab),
                m_startSpawn, Quaternion.identity);
        }

    }
	void Start () {
        bkMusic = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        bkMusic.PlayOneShot(spawnSound, sfxVolume); // SFX

        MainMenuButton = GameObject.Find("MainMenuButton");
        MainMenuButton.SetActive(false);
        ResumeButton = GameObject.Find("ResumeButton");
        ResumeButton.SetActive(false);
        EndPanel = GameObject.Find("LevelComplete");
        EndPanel.SetActive(false);

        glb_highscore = GetComponent<HighScores>();
    }


    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            MainMenuButton.SetActive(true);
            ResumeButton.SetActive(true);
            pauseLevel();
        }
    }

    public void restartLevel() {
        bkMusic.PlayOneShot(spawnSound, 100f); // SFX
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
        Checkpoint[] checkpoints = GetComponents<Checkpoint>();
        foreach (Checkpoint checkpt in checkpoints) {
            checkpt.lightBulbOff();

        }
    }

    public void levelCompleted() {
        bkMusic.PlayOneShot(finalSound, sfxVolume); // SFX
        pauseLevel();

        float timeCompleted = FindObjectOfType<UITimer>().getTime();
        glb_highscore.insertHighScore("Guy", timeCompleted);

        FindObjectOfType<GlobalManager>().SaveTimeCompleted(timeCompleted);
        EndPanel.SetActive(true);
        Invoke("loadScene", 4f);
    }

    private void loadScene () {
        SceneManager.LoadScene(
            (SceneManager.GetActiveScene().buildIndex + 1)
            % (SceneManager.sceneCountInBuildSettings));
    }

    public void pauseLevel() {
        FindObjectOfType<UITimer>().pause();
        FindObjectOfType<PlayerController>().disableControl();
    }

    public void resumeLevel() {
        MainMenuButton.SetActive(false);
        ResumeButton.SetActive(false);
        FindObjectOfType<UITimer>().resume();
        FindObjectOfType<PlayerController>().enableControl();
    }

    #region Sound Effects
    public void playThrow() {
        bkMusic.PlayOneShot(throwSound, sfxVolume);
    }

    public void playPush() {
        bkMusic.PlayOneShot(pushSound, sfxVolume);
    }

    public void playPull() {
        bkMusic.PlayOneShot(pullSound, sfxVolume);
    }

    public void playHurt() {
        bkMusic.PlayOneShot(hurtSound, sfxVolume);
    }

    public void playExtraHealth() {
        bkMusic.PlayOneShot(healthSound, sfxVolume);
    }

    public void playEnemySounds() {
        bkMusic.PlayOneShot(enemySound, sfxVolume);
    }

    public void playCheckPoint() {
        bkMusic.PlayOneShot(checkptSound, sfxVolume);
    }
    #endregion
}
