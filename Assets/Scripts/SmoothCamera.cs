using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;
    private Vector3 facing;
    private float headingAngle;

    void Start() {
        player = GameObject.FindGameObjectWithTag("Player");
        facing = player.transform.forward;
        facing.z = 0;
        headingAngle = Quaternion.LookRotation(facing).eulerAngles.z;
        if (headingAngle > 180f) headingAngle -= 360f;

    }

    void FixedUpdate() {
        /*  float posX;
          float posY;
          if (headingAngle > 0)
          {
              posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x + 10, ref velocity.x, smoothTimeX);
              posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
          }
          else
          {
              posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x - 10, ref velocity.x, smoothTimeX);
              posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
          }*/

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
        flaot posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
}