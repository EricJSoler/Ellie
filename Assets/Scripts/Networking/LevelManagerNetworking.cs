using UnityEngine;
using System.Collections;

public class LevelManagerNetworking : MonoBehaviour {

    // Use this for initialization
    public Vector3 location;
    void Awake() {
            PhotonNetwork.Instantiate("Ellie", new Vector3(0, 0, 0), Quaternion.identity, 0);
        Debug.Log("Instantiated 1");
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
