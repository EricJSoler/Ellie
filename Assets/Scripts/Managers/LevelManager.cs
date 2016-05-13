using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {


    public Vector3 m_startSpawn;
    public string playerPrefab = "Ellie";

    private GameObject MainMenuButton;
    private GameObject ResumeButton;


    void Awake() {
        Instantiate((GameObject)Resources.Load(playerPrefab),
            m_startSpawn, Quaternion.identity);
    }
	void Start () {
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
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex));
    }

    public void levelCompleted() {
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
}
