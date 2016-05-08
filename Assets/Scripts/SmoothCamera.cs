using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;
    private int facingDirection, prevFacingDirection;
    private float prevPlayerX;
    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        facingDirection = player.GetComponent<PlayerForces>().absHor;
        prevFacingDirection = 0;
        prevPlayerX = player.transform.position.x;
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

        


        if (facingDirection > 0)    //facing right
        {
            if (transform.position.x > player.transform.position.x + 10)
            {
                facingDirection = -facingDirection;
            }
            else if(transform.position.x < player.transform.position.x + 7)
            {


                if (prevFacingDirection == facingDirection)
                {
                    posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 7, ref velocity.x, smoothTimeX);
                    posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
                    transform.position = new Vector3(posX, posY, transform.position.z);
                }
                else
                {

                    posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 7, ref velocity.x, .6f);
                    posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, .6f);
                    transform.position = new Vector3(posX, posY, transform.position.z);
                }
            }
            
        }
        else                        //facing left
        {
            if (transform.position.x < player.transform.position.x - 10)
            {
                facingDirection = -facingDirection;
            }
            else if (transform.position.x > player.transform.position.x - 7)
            {


                if (facingDirection == prevFacingDirection)
                {
                    posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 7, ref velocity.x, smoothTimeX);
                    posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
                    transform.position = new Vector3(posX, posY, transform.position.z);
                }
                else
                {
                    posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 7, ref velocity.x, .6f);
                    posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, .6f);
                    transform.position = new Vector3(posX, posY, transform.position.z);
                }
                if (transform.position.x == player.transform.position.x - 10)
                {
                    facingDirection = -facingDirection;
                }
            }
        }
       



        if (transform.position == player.transform.position)
        {
            prevFacingDirection = facingDirection;
        }
    }
}