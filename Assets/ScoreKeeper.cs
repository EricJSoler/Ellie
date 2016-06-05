using UnityEngine;
using System.Collections;
using System;

public class ScoreKeeper : Photon.MonoBehaviour {


    public Vector3[] boltSpawns;
    // Use this for initialization
    float timeSinceLastSpawn;

    int masterClientScore;
    int otherScore;
    public string otherName;
    System.Random rnd;
	void Start () {
        masterClientScore = 0;
        otherScore = 0;
        photonView.RPC("recieveOtherName", PhotonTargets.OthersBuffered, "ricky");
        timeSinceLastSpawn = Time.time - 30;
        rnd = new System.Random();   
    }
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine) {
            if (Time.time > timeSinceLastSpawn + 60) {
                spawnBolt();
                timeSinceLastSpawn = Time.time;
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
    public void recieveOtherName(string name) {
        otherName = name;
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
        otherScore--;
    }

    [PunRPC]
    public void removeOtherScore() {
        otherScore--;
    }

}
