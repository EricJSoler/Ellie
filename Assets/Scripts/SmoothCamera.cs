using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;
    private int headingAngle, prevHeadingAngle;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        headingAngle = 1;
        prevHeadingAngle = 0;
    }

    void FixedUpdate()
    {
        TiedCamera();
    }
    void MouseCamera()
    {
        float posX;
        float posY;



        Vector3 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVector.z = 0;
        Vector3 newCameraLocation = Vector3.Lerp(player.transform.position, mouseVector, .5f);
        float distance = Vector2.Distance(transform.position, newCameraLocation);
        posX = Mathf.SmoothDamp(transform.position.x, newCameraLocation.x, ref velocity.x, smoothTimeX);
        posY = Mathf.SmoothDamp(transform.position.y, newCameraLocation.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    void TiedCamera()
    {
        float posX;
        float posY;

        headingAngle = player.GetComponent<PlayerForces>().absHor;


        if (headingAngle > 0)
        {
            if (prevHeadingAngle == headingAngle)
            {
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 7, ref velocity.x, smoothTimeX);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
            }
            else
            {

                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 7, ref velocity.x, .6f);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, .6f);
            }
        }
        else
        {
            if (headingAngle == prevHeadingAngle)
            {
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 7, ref velocity.x, smoothTimeX);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
            }
            else
            {
                posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 7, ref velocity.x, .6f);
                posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, .6f);
            }
        }
        transform.position = new Vector3(posX, posY, transform.position.z);



        if (transform.position == player.transform.position)
        {
            prevHeadingAngle = headingAngle;
        }
    }
}