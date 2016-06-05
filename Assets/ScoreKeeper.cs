using UnityEngine;
using System.Collections;
using System;

public class ScoreKeeper : Photon.MonoBehaviour {


    public Vector3[] boltSpawns;
    // Use this for initialization
    float timeSinceLastSpawn;

    int masterClientScore;
    int otherScore;
    string otherName;
    System.Random rnd;

    void Awake() {
       // PhotonNetwork.playerName = FindObjectOfType<GlobalManager>().getName();
    }

	void Start () {
        masterClientScore = 0;
        otherScore = 0;
        //object name = (object)FindObjectOfType<GlobalManager>().getName();
        //photonView.RPC("recieveOtherName", PhotonTargets.OthersBuffered, name);
        timeSinceLastSpawn = Time.time - 30;
        rnd = new System.Random();

        //setNames();

    }
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine) {
            if (Time.time > timeSinceLastSpawn + 20) {
                spawnBolt();
                timeSinceLastSpawn = Time.time;
            }
        }
        
	}


    void setNames() {
        PhotonPlayer[] a = PhotonNetwork.playerList;
        for(int i = 0; i < a.Length; i++) {
            if(!a[i].isLocal) {
                otherName = a[i].name;
            }
        }
    }

    void spawnBolt() {
        int randINt = rnd.Next() % boltSpawns.Length;
        PhotonNetwork.Instantiate("mBolt", boltSpawns[randINt], Quaternion.identity, 0);
    }

    void OnPhotonSerializeView(PhotonStream steam, PhotonMessageInfo info) {
        if (steam.isWriting) {
            steam.SendNext(otherScore);
            steam.SendNext(masterClientScore);
        }
        else if (steam.isReading){
            otherScore = (int)steam.ReceiveNext();
            masterClientScore = (int)steam.ReceiveNext();
        }
    }

    [PunRPC]
    public void recieveOtherName(object name) {
        otherName = (string)name;
        Debug.Log(name);
      
    }

    public string getOtherName() {
        //if (otherName == null) {
            PhotonPlayer[] a = PhotonNetwork.playerList;
            for (int i = 0; i < a.Length; i++) {
                if (!a[i].isLocal) {
                    otherName = a[i].name;
                }
            }
        //}
        return otherName;
    }


    public int getOtherScore() {
        if (PhotonNetwork.isMasterClient) {
            return otherScore;
        }
        else {
            return masterClientScore;
        }
    }

    public int getMyScore() {
        if (!PhotonNetwork.isMasterClient) {
            return otherScore;
        }
        else {
            return masterClientScore;
        }
    }

    public void addMasterClientScore() {
        masterClientScore++;
    }

    public void removeMasterClientScore() {
        masterClientScore--;
    }

    public void addOtherScore() {
        otherScore++;
    }

    [PunRPC]
    public void removeOtherScore() {
        otherScore--;
    }
    
    public void callRemoveOtherScore() {
        photonView.RPC("removeOtherScore", PhotonTargets.OthersBuffered);
    }

}
