using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    private int headingAngle;
    private int prevHeadingAngle;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
  
    void FixedUpdate()
    {
        MouseCamera();
    }
    public Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
    {
        Vector3 P = x * Vector3.Normalize(B - A) + A;
        return P;
    }
    void MouseCamera()
    {
        float posX;
        float posY;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y,0);
        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + (Input.mousePosition.x / Screen.width), ref velocity.x, smoothTimeX);
        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + (Input.mousePosition.y / Screen.height), ref velocity.y, smoothTimeY);


        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    void TiedCamera()
    {
        float posX;
        float posY;
        prevHeadingAngle = headingAngle;
        headingAngle = player.GetComponent<PlayerForces>().absHor;
        if (headingAngle > 0)
        {
            if (prevHeadingAngle == headingAngle)
            {
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 10, ref velocity.x, smoothTimeX);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
            }
            else
            {
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 10, ref velocity.x, .6f);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, .6f);
            }
        }
        else
        {
            if (headingAngle == prevHeadingAngle)
            {
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 10, ref velocity.x, smoothTimeX);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
            }
            else
            {
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 10, ref velocity.x, .6f);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, .6f);
            }
        }

        // float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        //flaot posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}