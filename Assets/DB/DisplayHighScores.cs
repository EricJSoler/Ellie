using UnityEngine;
using System.Collections;
using System;
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
        int rank = 0;
        bool lessHighScoreNodes = highScoresList.Length < highScoreText.Length;

        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = ++rank + ". ";

            try
            {
                if (highScoresList[highScoresList.Length - rank].name.Length + 1 > maxNameLength)
                    highScoresList[highScoresList.Length - rank].name 
                        = highScoresList[highScoresList.Length - rank].name.Substring(0, maxNameLength);

                highScoreText[i].text += highScoresList[highScoresList.Length - rank].name
                    + " - " + highScoresList[highScoresList.Length - rank].time;
            }
            catch (Exception e)
            {
                highScoreText[i].text += "null";
                Debug.Log(e);
            }
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
