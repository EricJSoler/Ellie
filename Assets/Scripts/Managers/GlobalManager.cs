using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GlobalManager : MonoBehaviour {

    // Use this for initialization

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }
 
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save() {

    }

    public void SaveTimeCompleted(float time) {
        FindObjectOfType<DataHolder>().save(time);
    }

    public void Load() {
        SceneManager.LoadScene(FindObjectOfType<DataHolder>().load());
        
    }

}
