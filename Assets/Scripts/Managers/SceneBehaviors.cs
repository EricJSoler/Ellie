using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneBehaviors : MonoBehaviour {

	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

    public void ButtonClicked(string sceneName)
    { SceneManager.LoadScene(sceneName); }

    public void Exit()
    { Application.Quit(); }

    public void Load() {
        GameObject globalManager = GameObject.FindGameObjectWithTag("GlobalManager");
        GlobalManager gm = globalManager.GetComponent<GlobalManager>();
        gm.Load();
    }
}
