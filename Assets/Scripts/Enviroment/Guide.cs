using UnityEngine;
using System.Collections;

public class Guide : MonoBehaviour {
    private Rigidbody2D rb;
    private GameObject player;
    private GameObject[] all;
    private bool allPerformed;
    private float life;
    private bool hideGuide;



	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        life = Time.time + 3f;
        all = new GameObject[0];
        allPerformed = false;
        hideGuide = true;

        transform.position = player.transform.position;
        gameObject.GetComponent<Renderer>().material.color = new Color(1f, 1f, 1f, 0.5f);
        //all = GameObject.FindObjectsOfType<GameObject>();
    }

    // Update is called once per frame
    void Update () {
        //if (allPerformed == false)
        //{
        //    all = GameObject.FindObjectsOfType<GameObject>();
        //    allPerformed = true;
        //}

        //for (int i = 0; i < all.Length; i++)
        //{

        //}
        //if (GetComponent<Collider2D>().IsTouching(player.gameObject.GetComponent<Collider2D>()))
        //    Destroy(gameObject);//Physics2D.IgnoreCollision(GetComponent<Collider2D>(), player.gameObject.GetComponent<Collider2D>());
        //if (Input.GetKeyDown(KeyCode.G))
        //    hideGuide = !hideGuide;

        //if (hideGuide)
        //    GetComponent<Renderer>().enabled = !GetComponent<Renderer>().enabled;

        if (Time.time >= life)
            Destroy(gameObject);
    }

    void OnCollision(Collision col)
    { }

    void OnCollisionEnter2D(Collision2D col)
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("Guiding"), true);

        if (col.gameObject.tag == "Nubbie" || col.gameObject.tag == "Prottie" || col.gameObject.tag == "Eddie"
            || col.gameObject.tag == "Guide" || col.gameObject.tag == "Device" || col.gameObject.tag == "Player")
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
        }
        else //if (col.gameObject.tag == "Untagged")
        {
            Physics2D.IgnoreCollision(col.collider, this.GetComponent<Collider2D>());
            Destroy(gameObject);
        }
    }
}
