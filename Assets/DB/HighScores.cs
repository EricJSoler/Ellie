using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScores : MonoBehaviour {


    #region Leaderboard Keys
    // When adding a new key, do the following:
    //    1) Follow the naming convention below
    //    2) Update MAX_PRIVATE_KEYS
    //    3) Update MAX_PUBLIC_KEYS
    //    4) Update case statement in getPrivateKey()
    //    5) Update case statement in getPublicKey()
    private const string PRIVATE_KEY_LVL1 = "LRxxlQV0vECmFOqIXZeiRAHn3Ssp-A2k-dyBd4sfpmTg";
    private const string PUBLIC_KEY_LVL1  = "574ba3f08af603062c13d36a";

    private const string PRIVATE_KEY_LVL2 = "vGVXU2qcykyGRp9HPaqVRw_csdbPY0vkyXawHvsRLaCw";
    private const string PUBLIC_KEY_LVL2  = "574bbc738af603062c13e637";

    private const string PRIVATE_KEY_LVL3 = "xEi9Ao5V7kik_iP21rK-Zgbc5txWD6U0qhwrIdiJ-EkA";
    private const string PUBLIC_KEY_LVL3  = "574bbca18af603062c13e671";

    private const string PRIVATE_KEY_LVL4 = "8Bg2O7tr1UG9v8XyxmLPrgGron-wM0GkCkmhpxINRNSw";
    private const string PUBLIC_KEY_LVL4  = "574bbcbc8af603062c13e688";

    private const string PRIVATE_KEY_LVL5 = "M1V57jX8u0OxhPwv5X7QzQ44SvSLKrekKn3cknpj1p4Q";
    private const string PUBLIC_KEY_LVL5  = "574d1a338af6030fd89d767f";

    private const string URL              = "http://dreamlo.com/lb/";
    private const int MAX_PRIVATE_KEYS    = 5;
    private const int MAX_PUBLIC_KEYS     = 5;
    #endregion


    //public Text[] highScoreText;
    public HighScoresNode[] highScoresList;
    public int level = 1;
    //public int maxRetrievals = 5;
    public int maxNameLengthForInsert = 20;
    private DisplayHighScores highScoresDisplay;


    void Awake()
    {
        highScoresDisplay = GetComponent<DisplayHighScores>();
        //StartCoroutine(uploadToDB("Cyrus", 503, "LRxxlQV0vECmFOqIXZeiRAHn3Ssp-A2k-dyBd4sfpmTg", "574ba3f08af603062c13d36a"));
        //StartCoroutine(uploadToDB("Is", 504, "LRxxlQV0vECmFOqIXZeiRAHn3Ssp-A2k-dyBd4sfpmTg", "574ba3f08af603062c13d36a"));
        //StartCoroutine(uploadToDB("Handsom", 505, "LRxxlQV0vECmFOqIXZeiRAHn3Ssp-A2k-dyBd4sfpmTg", "574ba3f08af603062c13d36a"));

        //StartCoroutine(uploadToDB("Cyrus", 703, "vGVXU2qcykyGRp9HPaqVRw_csdbPY0vkyXawHvsRLaCw", "574bbc738af603062c13e637"));
        //StartCoroutine(uploadToDB("Is", 704, "vGVXU2qcykyGRp9HPaqVRw_csdbPY0vkyXawHvsRLaCw", "574bbc738af603062c13e637"));
        //StartCoroutine(uploadToDB("Handsom", 705, "vGVXU2qcykyGRp9HPaqVRw_csdbPY0vkyXawHvsRLaCw", "574bbc738af603062c13e637"));

        //StartCoroutine(uploadToDB("Cyrus", 903, "xEi9Ao5V7kik_iP21rK-Zgbc5txWD6U0qhwrIdiJ-EkA", "574bbca18af603062c13e671"));
        //StartCoroutine(uploadToDB("Is", 904, "xEi9Ao5V7kik_iP21rK-Zgbc5txWD6U0qhwrIdiJ-EkA", "574bbca18af603062c13e671"));
        //StartCoroutine(uploadToDB("Handsom", 905, "xEi9Ao5V7kik_iP21rK-Zgbc5txWD6U0qhwrIdiJ-EkA", "574bbca18af603062c13e671"));

    }


    void Start ()
    {
    }
	

	void Update () { }


    //--------------------------------- insertHighScore ----------------------------------------------
    // Passes in player's name, the level played, and the time it took to complete that level to be
    // inserted into online leaderboard
    //------------------------------------------------------------------------------------------------
    public void insertHighScore(string name, float time)
    {
        Debug.Log("Entering INSERT w/ time @ " + time);

        string privateKey = getPrivateKey(level), 
               publicKey  = getPublicKey(level) ;

        #region Replace Malicious Words and Characters
        name = name.Replace(' ', '_');
        name = name.Replace("clear", "clr");
        name = name.Replace("DROP TABLE", "Scumbag");
        name = name.Replace("fuck", "fock");
        #endregion

        #region Concatanate Name by maxNameLength if necessary
        if (name.Length + 1 > maxNameLengthForInsert)
            name = name.Substring(0, maxNameLengthForInsert);
        #endregion

        StartCoroutine(uploadToDB(name, time, privateKey, publicKey));
    }


    //--------------------------------- getHighScores ------------------------------------------------
    // Retrieves the high scores from a level
    //------------------------------------------------------------------------------------------------
    public void getHighScores(int lvl)
    {
        StartCoroutine(downloadLevelDB(getPublicKey(lvl)));
    }


    //--------------------------------- getPrivateKey ------------------------------------------------
    // Returns the private key for a specified level
    //------------------------------------------------------------------------------------------------
    private static string getPrivateKey(int lvl)
    {
        string privateKey;

        switch (lvl)
        {
            case 1:
                privateKey = PRIVATE_KEY_LVL1;
                break;
            case 2:
                privateKey = PRIVATE_KEY_LVL2;
                break;
            case 3:
                privateKey = PRIVATE_KEY_LVL3;
                break;
            case 4:
                privateKey = PRIVATE_KEY_LVL4;
                break;
            case 5:
                privateKey = PRIVATE_KEY_LVL5;
                break;
            default:
                Debug.Log("Not a level");
                return "";
        }

        return privateKey;
    }


    //--------------------------------- getPublicKey -------------------------------------------------
    // Returns the public key for a specified level
    //------------------------------------------------------------------------------------------------
    private static string getPublicKey(int lvl)
    {
        string publicKey;

        switch (lvl)
        {
            case 1:
                publicKey = PRIVATE_KEY_LVL1;
                break;
            case 2:
                publicKey = PRIVATE_KEY_LVL2;
                break;
            case 3:
                publicKey = PRIVATE_KEY_LVL3;
                break;
            case 4:
                publicKey = PRIVATE_KEY_LVL4;
                break;
            case 5:
                publicKey = PRIVATE_KEY_LVL5;
                break;
            default:
                Debug.Log("Not a level");
                return "";
        }

        return publicKey;
    }
      

    //--------------------------------- uploadToDB ---------------------------------------------------
    // Connects to web URL and adds player name and time to leaderboard
    //------------------------------------------------------------------------------------------------
    private static IEnumerator uploadToDB(string name, float time, string privateKey, string publicKey)
    {
        WWW www = new WWW(URL + privateKey + "/add/" + WWW.EscapeURL(name) + "/" + (int)Mathf.Floor(time));
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
            Debug.Log("Error occured in high scores coroutine upload: " + www.error);
    }


    //--------------------------------- downloadLevelDB ----------------------------------------------
    // Connects to web URL and downloads highscores for specified level
    //------------------------------------------------------------------------------------------------
    IEnumerator downloadLevelDB(string publicKey)
    {
        WWW www = new WWW(URL + publicKey + "/pipe/");
        yield return www;

        if (!string.IsNullOrEmpty(www.error))
        {
            Debug.Log("Error occured in high scores coroutine download: " + www.error);
        }
        else
        {
            formatHighScores(www.text);
            highScoresDisplay.OnHighscoresDownloaded(highScoresList);
        }
    }


    //--------------------------------- formatHighScores ---------------------------------------------
    // Formats the high scores retrieved from the online database
    //------------------------------------------------------------------------------------------------
    private void formatHighScores(string textStream)
    {
        string[] entries = textStream.Split(new char[] { '\n' },
            System.StringSplitOptions.RemoveEmptyEntries);

        highScoresList = new HighScoresNode[entries.Length];

        for (int i = 0; i < entries.Length; i++)
        {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string userName = entryInfo[0];
            float time = (float) int.Parse(entryInfo[1]);
            highScoresList[i] = new HighScoresNode(userName, time, 1);
            //Debug.Log(highScoresList[i].name + ": " + highScoresList[i].time);
        }
    }


}



public struct HighScoresNode
{
    public string name;
    public float time;
    public int lvl;

    public HighScoresNode(string _name, float _time, int _lvl)
    {
        name = _name;
        time = _time;
        lvl  = _lvl;
    }
}