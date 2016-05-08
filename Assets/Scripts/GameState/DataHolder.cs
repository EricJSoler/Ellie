using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataHolder : MonoBehaviour
{
    public static DataHolder dataHolderInstance;
    
    [System.Serializable]
    struct DataToSave
    {
        public float level1Time;
        public float level2Time;
        public int lastScene;
    }

    DataToSave myData;
    // Use this for initialization
    void Start() {
        if (dataHolderInstance == null) {
            dataHolderInstance = this;
        }
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    public void save(float time) {
        int scene = SceneManager.GetActiveScene().buildIndex;
        switch (scene) {
            case 1:
                myData.level1Time = time;
                break;
            case 2:
                myData.level2Time = time;
                break;
        }

        myData.lastScene = (SceneManager.GetActiveScene().buildIndex + 1)
            % SceneManager.sceneCountInBuildSettings;
        
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, this.myData);
        file.Close();
        Debug.Log("Save Complete");
    }

    public int load() {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            myData = (DataToSave)bf.Deserialize(file);
            file.Close();
            return myData.lastScene;
        }
        return 0;
    }
}