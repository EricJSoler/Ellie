using UnityEngine;
using System.Collections;

public class LightBulb : MonoBehaviour {

    private Behaviour halo;
    private SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        halo = (Behaviour)GetComponent("Halo");
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void On() {
        if (!halo.enabled)
            GameObject.Find("LevelManager").GetComponent<LevelManager>().playCheckPoint();
        halo.enabled = true;
        sprite.color = new Color(174, 159, 0);
    }

    public void Off() {
        halo.enabled = false;
        sprite.color = new Color(255, 255, 255);

    }
}
