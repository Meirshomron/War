using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float scrollSpeed = 5f;
    public float panSpeed = 60f;
    public float panborderThickness = 10f;
    public float minY = 10f;
    public float maxY = 80f;
    private bool doMovement = true;
	
	void Update () {
        /*
        if (GameManager.GameIsOver)
        {
            this.enabled = false;
            return;
        }
        */

        if (Input.GetKeyDown(KeyCode.Escape))
            doMovement = !doMovement;

        if (!doMovement)
            return;

        if (Input.GetKey("a") || Input.mousePosition.x < panborderThickness)
        {
            transform.Translate(Vector3.forward*panSpeed*Time.deltaTime,Space.World);
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - panborderThickness)
        {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s")|| Input.mousePosition.y <= panborderThickness)
        {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("w")||Input.mousePosition.y>=Screen.height - panborderThickness)
        {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }

        /*board limits*/
        if(transform.position.x < -60) transform.position = new Vector3(-60, transform.position.y, transform.position.z);
        if (transform.position.x > 50) transform.position = new Vector3(50, transform.position.y, transform.position.z);
        if (transform.position.z < -30) transform.position = new Vector3(transform.position.x, transform.position.y,-30);
        if (transform.position.z > 200) transform.position = new Vector3(transform.position.x, transform.position.y,200);

        /*zoom in*/
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Vector3 pos = transform.position;
        pos.y -= scroll * 1000 * scrollSpeed * Time.deltaTime ;
        pos.y = Mathf.Clamp(pos.y,minY,maxY);
        transform.position = pos;
    }
}
