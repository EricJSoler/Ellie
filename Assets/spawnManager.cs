using UnityEngine;
using System.Collections;

public class spawnManager : MonoBehaviour {

    public Vector3 player1Spawn;
    public Vector3 player2Spawn;
    public GameObject myEllie;
    // Use this for initialization
    bool spawnedScoreKeeper = false;
    void Awake() {
        if (PhotonNetwork.isMasterClient) {
            myEllie = PhotonNetwork.Instantiate("Ellie", player1Spawn, Quaternion.identity, 0);
        }
        else {
            myEllie = PhotonNetwork.Instantiate("Ellie", player2Spawn, Quaternion.identity, 0);
        }
    }

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!spawnedScoreKeeper && PhotonNetwork.isMasterClient && PhotonNetwork.room.playerCount == 2) {
            PhotonNetwork.Instantiate("scoreKeeper", new Vector3(0, 0, 0), Quaternion.identity, 0);
            spawnedScoreKeeper = true;
        }
	}

    public void respawn() {
        //PhotonNetwork.Destroy(myEllie);
        if (PhotonNetwork.isMasterClient) {
            myEllie.gameObject.transform.position = player1Spawn;
        }
        else {
            myEllie.gameObject.transform.position = player2Spawn;
        }
    }
}
