using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

    public GameObject toFollow;
    public float m_zoom;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = new Vector3(toFollow.transform.position.x,
            toFollow.transform.position.y, m_zoom);
	}
}
