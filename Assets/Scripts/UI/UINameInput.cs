using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UINameInput : MonoBehaviour {

    public InputField input;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void storeName () {
        string name = input.text;
        FindObjectOfType<DataHolder>().initName(name);

    }
}
