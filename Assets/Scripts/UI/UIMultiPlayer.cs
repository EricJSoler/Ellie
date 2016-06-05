using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMultiPlayer : MonoBehaviour {

    public Text myName;
    public Text theirName;
    public Text myScore;
    public Text theirScore;
    private ScoreKeeper scoreKeeper;

    // Use this for initialization
    void Start () {
       scoreKeeper =FindObjectOfType<ScoreKeeper>();
    }
	
	// Update is called once per frame
	void Update () {
        myName.text = FindObjectOfType<GlobalManager>().getName();
        theirScore.text = scoreKeeper.otherName;
        myScore.text = ""+ scoreKeeper.getMyScore();
        theirScore.text = "" + scoreKeeper.getOtherScore();

    }
}
