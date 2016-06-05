using UnityEngine;
using System.Collections;

public class MBolt : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "Player") {
            if (other.gameObject.GetPhotonView().isMine) {
                if (this.photonView.isMine) { //local im the master client
                    FindObjectOfType<ScoreKeeper>().addMasterClientScore();
                    PhotonNetwork.Destroy(this.gameObject);
                }
                else { //im player 2
                    photonView.RPC("otherHit", PhotonTargets.OthersBuffered);
                }
            }
        }
    }

    [PunRPC]
    void otherHit() {
        FindObjectOfType<ScoreKeeper>().addMasterClientScore();
        PhotonNetwork.Destroy(this.gameObject);
    }
}
