using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;
    private int facingDirection, prevFacingDirection, upright;
    private float prevPlayerX;
    public GameObject player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        facingDirection = player.GetComponent<PlayerForces>().absHor;
        upright = player.GetComponent<PlayerForces>().absUp;
        prevFacingDirection = 0;
        prevPlayerX = player.transform.position.x;
    }

    void FixedUpdate() {
        if (player.GetComponent<PlayerController>().freeLook) {
            MouseCamera();
        }
        else {
            ForwardCamera();
        }
        
    }
    void MouseCamera() {
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
    void checkpoint() {
        transform.position = new Vector3(player.transform.position.x + 7, player.transform.position.y, transform.position.z);
    }
    void ForwardCamera() {
        float posX;
        float posY;
        Vector3 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVector.z = 0;
        Vector3 newCameraLocation = Vector3.Lerp(player.transform.position, mouseVector, .5f);
        float velY = player.GetComponent<Rigidbody2D>().velocity.y;
        if (velY > 3) {
            velY = 3;
        }
        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + player.GetComponent<Rigidbody2D>().velocity.x, ref velocity.x, smoothTimeX);
        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + velY, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    void TiedCamera() {
        float posX;
        float posY;

        //if (upright != player.GetComponent<PlayerForces>().absUp && player.GetComponent<PlayerForces>().grounded) {
        //    upright = player.GetComponent<PlayerForces>().absUp;
        //}

        float yVel = player.GetComponent<Rigidbody2D>().velocity.y;
        if(yVel > 0 && !player.GetComponent<PlayerForces>().grounded) {
            upright = 1;
        }
        else if(yVel < 0 && !player.GetComponent<PlayerForces>().grounded) {
            upright = -1;
        }
        else {
            upright = player.GetComponent<PlayerForces>().absUp;
        }

        if (facingDirection > 0)    //facing right
        {
            if (transform.position.x > player.transform.position.x + 7) {
                facingDirection = -facingDirection;
            }
            else if (transform.position.x < player.transform.position.x + 6) {
                if (prevFacingDirection == facingDirection) {
                    if (upright < 1) {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 6, ref velocity.x, smoothTimeX);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y - 4, ref velocity.y, smoothTimeY);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                    else {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 6, ref velocity.x, smoothTimeX);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + 4, ref velocity.y, smoothTimeY);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                }
                else {

                    if (upright < 1) {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 6, ref velocity.x, .6f);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y - 4, ref velocity.y, .6f);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                    else {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 6, ref velocity.x, .6f);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + 4, ref velocity.y, .6f);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                }
            }

        }
        else                        //facing left
        {
            if (transform.position.x < player.transform.position.x - 7) {
                facingDirection = -facingDirection;
            }
            else if (transform.position.x > player.transform.position.x - 6) {


                if (facingDirection == prevFacingDirection) {
                    if (upright < 1) {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 6, ref velocity.x, smoothTimeX);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y - 4, ref velocity.y, smoothTimeY);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                    else {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 6, ref velocity.x, smoothTimeX);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + 4, ref velocity.y, smoothTimeY);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                }
                else {
                    if (upright < 1) {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 6, ref velocity.x, .6f);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y - 4, ref velocity.y, .6f);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                    else {
                        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 6, ref velocity.x, .6f);
                        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y + 4, ref velocity.y, .6f);
                        transform.position = new Vector3(posX, posY, transform.position.z);
                    }
                }
            }
        }




        if (transform.position == player.transform.position) {
            prevFacingDirection = facingDirection;
        }
    }
}