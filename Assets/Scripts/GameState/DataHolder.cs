using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DataHolder : MonoBehaviour {
    public static DataHolder dataHolderInstance;

    [System.Serializable]
    struct DataToSave {
        public string name;
        public float level1Time;
        public float level2Time;
        public float level3Time;
        public int lastScene;
    }

    DataToSave myData;
    // Use this for initialization
    void Start() {
        myData.name = "";
        myData.level1Time = 9999999;
        myData.level2Time = 9999999;
        myData.level3Time = 9999999;

        if (dataHolderInstance == null) {
            dataHolderInstance = this;
        } else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update() {

    }

    public void initName (string name) {
        if (name.Equals("")) {
            name = "Anonymous";
        }
        myData.name = name;
    }

    public string getName () {
        return myData.name;
    }

    public void save(float time) {
        int scene = SceneManager.GetActiveScene().buildIndex;
        switch (scene) {
            case 1:
                if (myData.level1Time > time)
                    myData.level1Time = time;
                break;
            case 2:
                if (myData.level2Time > time)
                    myData.level2Time = time;
                break;
            case 3:
                if (myData.level3Time > time)
                    myData.level3Time = time;
                break;
        }

        myData.lastScene = (SceneManager.GetActiveScene().buildIndex + 1)
            % SceneManager.sceneCountInBuildSettings;

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/savedGames.gd");
        bf.Serialize(file, this.myData);
        file.Close();
        Debug.Log("Save Complete " + (Application.persistentDataPath));
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

    public float getBestTime(int level) {
        if (File.Exists(Application.persistentDataPath + "/savedGames.gd")) {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/savedGames.gd", FileMode.Open);
            DataToSave savedData = (DataToSave)bf.Deserialize(file);
            file.Close();

            switch (level) {
                case 1:
                    return savedData.level1Time;
                case 2:
                    return savedData.level2Time;
                case 3:
                    return savedData.level3Time;
            }

        }
        return 0;
    }
}