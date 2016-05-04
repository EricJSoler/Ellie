using UnityEngine;
using System.Collections;

public class SmoothCamera : MonoBehaviour
{
    private Vector2 velocity;
    public float smoothTimeY;
    public float smoothTimeX;

    public GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
  
    void FixedUpdate()
    {
        LerpCamera();
    }
    void LerpCamera()
    {
        float posX;
        float posY;

      

        Vector3 mouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseVector.z = 0;
        Vector3 newCameraLocation = Vector3.Lerp(player.transform.position, mouseVector, .5f);
        
        posX = Mathf.SmoothDamp(transform.position.x, newCameraLocation.x, ref velocity.x, smoothTimeX);
        posY = Mathf.SmoothDamp(transform.position.y, newCameraLocation.y, ref velocity.y, smoothTimeY);
        transform.position = new Vector3(posX, posY, transform.position.z);
    }
    
}