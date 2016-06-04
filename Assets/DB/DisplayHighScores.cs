using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour {


    public int highScoresLevel = 1;
    public int maxNameLength;
    public Text[] highScoreText;
    private HighScores highScoreManager;


	// Use this for initialization
	void Start ()
    {
        highScoreManager = GetComponent<HighScores>();

        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = i + 1 + ". Retrieving...";
        }

        StartCoroutine("RefreshHighscores");
    }


    //--------------------------------- OnHighScoresDownloaded ---------------------------------------
    // Connects to web URL and downloads highscores for specified level
    //------------------------------------------------------------------------------------------------
    public void OnHighscoresDownloaded (HighScoresNode[] highScoresList)
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = i + 1 + ". ";
            if (highScoresList.Length > i)
            {
                if (highScoresList[i].name.Length + 1 > maxNameLength)
                {
                    highScoresList[i].name = highScoresList[i].name.Substring(0, maxNameLength);
                }

                highScoreText[i].text += highScoresList[i].name + " - " + highScoresList[i].time;
            }
            else
                highScoreText[i].text += "null";
        }
    }


    //--------------------------------- refreshHighscores --------------------------------------------
    // Connects to web URL and downloads highscores for specified level
    //------------------------------------------------------------------------------------------------
    private IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highScoreManager.getHighScores(highScoresLevel);
            yield return new WaitForSeconds(30);
        }
    }
}
