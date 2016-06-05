using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

public class DisplayHighScores : MonoBehaviour {


    public int highScoresLevel = 1;
    public int maxNameLength;
    public Text[] highScoreText;
    private HighScores highScoreManager;

    void Awake()
    {
        highScoreManager = GetComponent<HighScores>();
    }

	// Use this for initialization
	void Start ()
    {
        for (int i = 0; i < highScoreText.Length; i++)
        {
            highScoreText[i].text = i + 1 + ". Retrieving...";
        }

        StartCoroutine("RefreshHighscores");
    }


    //--------------------------------- OnHighScoresDownloaded ---------------------------------------
    // Displays highscores to screen
    //------------------------------------------------------------------------------------------------
    public void OnHighscoresDownloaded (HighScoresNode[] highScoresList)
    {
        int rank = 0;
        bool lessHighScoreNodes = highScoresList.Length < highScoreText.Length;

        for (int i = 0; i < highScoreText.Length; i += 2)
        {
            highScoreText[i].text = ++rank + ". ";

            try
            {
                if (highScoresList[highScoresList.Length - rank].name.Length + 1 > maxNameLength)
                    highScoresList[highScoresList.Length - rank].name 
                        = highScoresList[highScoresList.Length - rank].name.Substring(0, maxNameLength);

                highScoreText[i].text += highScoresList[highScoresList.Length - rank].name;
                highScoreText[i + 1].text = formatTime(highScoresList[highScoresList.Length - rank].time);
                //+ addSpaceBuffer(highScoresList[highScoresList.Length - rank].name.Length)
                //+ formatTime(highScoresList[highScoresList.Length - rank].time);
            }
            catch (Exception e)
            {
                highScoreText[i].text += "null";
                highScoreText[i + 1].text = "";
                Debug.Log(e);
            }
        }
    }


    //--------------------------------- refreshHighscores --------------------------------------------
    // Refreshes global highscores
    //------------------------------------------------------------------------------------------------
    private IEnumerator RefreshHighscores()
    {
        while (true)
        {
            highScoreManager.getHighScores(highScoresLevel);
            yield return new WaitForSeconds(30);
        }
    }


    //--------------------------------- formatTime ---------------------------------------------------
    // Reformats the float value of time for proper display
    //------------------------------------------------------------------------------------------------
    private string formatTime(float time)
    {
        string minutes = Mathf.Floor(time / 60) + "";
        string seconds = time % 60 < 10 ? "0" + time % 60 : (time % 60) + "";

        return minutes + ":" + seconds;
    }


    //--------------------------------- addSpaceBuffer -----------------------------------------------
    // Adds spaces between name and time
    //------------------------------------------------------------------------------------------------
    private string addSpaceBuffer(int nameLength)
    {
        string s = "";

        for (int i = 0; i < 10 - nameLength; i++)
            s += " ";

        return s;
    }
}
