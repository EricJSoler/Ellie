using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {


    public Vector3 m_startSpawn;
    public string playerPrefab = "Ellie";
    
    void Awake() {
        Instantiate((GameObject)Resources.Load(playerPrefab),
            m_startSpawn, Quaternion.identity);
    }
	void Start () {
        
	}
	
	
	void Update () {
	
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
}
