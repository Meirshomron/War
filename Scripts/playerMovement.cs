using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour {

    private Transform target;
    private int wavePointIndex = 0;
    private bool goingRight = false;

    public GameObject Waypoint;
    private waypoints waypoints;
    [HideInInspector]
    public float speed;

    public float startSpeed = 30f;


    // Use this for initialization
    void Start()
    {
        speed = startSpeed;

        waypoints = Waypoint.GetComponent<waypoints>();
        //go left path or right path
        if (transform.position.z > 100)
        {
            target = waypoints.left[0];
        }
        else
        {
            target = waypoints.right[0];
            goingRight = true;
        } 
    }

    void Update()
    {
        slowDownIfHasTarget();
       
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.4f)
        {
            GetNextWaypoint();
        }
        speed = startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavePointIndex >= waypoints.left.Length - 1) //reached end of path
        {
            wavePointIndex--;
            if(goingRight)
                target = waypoints.right[wavePointIndex];
            else
                target = waypoints.left[wavePointIndex];          
        }
        else // go to next waypoint
        {
            wavePointIndex++;
            if (goingRight)
                target = waypoints.right[wavePointIndex];
            else
                target = waypoints.left[wavePointIndex];
        }
    }

    void slowDownIfHasTarget()
    {
        if (GetComponent<Turret>().getTarget() != null)
        {
            speed = speed / 4;
        }
        else
        {
            speed = startSpeed;
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

}
