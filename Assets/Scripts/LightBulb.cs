using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

    //private Behaviour halo;
    //private SpriteRenderer sprite;

    public GameObject lightbulbOff;
    public GameObject lightbulbOn;
    private bool lightOn;

	// Use this for initialization
	void Start () {
        lightOn = false;
        //halo = (Behaviour)GetComponent("Halo");
        //sprite = GetComponent<SpriteRenderer>();
        Off();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void On() {
        if (!lightOn)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().playCheckPoint();

        lightbulbOff.GetComponent<Renderer>().enabled = false;
        lightbulbOn.GetComponent<Renderer>().enabled = true;
        lightOn = true;
        //halo.enabled = true;
        //sprite.color = new Color(174, 159, 0);
        //sprite.color = Color.white;
    }

    public void Off() {
        lightbulbOff.GetComponent<Renderer>().enabled = true;
        lightbulbOn.GetComponent<Renderer>().enabled = false;
        lightOn = false;
        //halo.enabled = false;
        //sprite.color = new Color(255, 255, 255);

    }
}
