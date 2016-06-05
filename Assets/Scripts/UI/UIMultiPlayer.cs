using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIMultiPlayer : MonoBehaviour {

    public Text myName;
    public Text theirName;
    public Text myScore;
    public Text theirScore;
    private ScoreKeeper scoreKeeper;
    bool foundScoreKeeper = false;

    // Use this for initialization
    void Start () {
       scoreKeeper =FindObjectOfType<ScoreKeeper>();
        if(scoreKeeper != null) {
            foundScoreKeeper = true;
        }
    }
	
	// Update is called once per frame
	void Update () {
        myName.text = FindObjectOfType<GlobalManager>().getName();
        if (scoreKeeper != null) {
            theirName.text = scoreKeeper.getOtherName();
            myScore.text = "" + scoreKeeper.getMyScore();
            theirScore.text = "" + scoreKeeper.getOtherScore();
        }
        if (!foundScoreKeeper) {
            scoreKeeper = FindObjectOfType<ScoreKeeper>();
            if (scoreKeeper != null) {
                foundScoreKeeper = true;
            }
        }

    }
}
