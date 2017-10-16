using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour {

    private Transform target;
    private int wavePointIndex = 0;
    private bool goingRight = false;

    public GameObject Waypoint;
    private waypoints waypoints;
    [HideInInspector]
    public float speed;

    public float startSpeed = 10f;

    void Start()
    {
        speed = startSpeed;
        waypoints = Waypoint.GetComponent<waypoints>();
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
        if (GetComponent<Turret>().getTarget() == null) //if incounterd player - don't move
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.4f)
            {
                GetNextWaypoint();
            }
            speed = startSpeed;
        }
    }   

    void GetNextWaypoint()
    {
        if (wavePointIndex >= waypoints.left.Length - 1) //reached end of path
        {
            wavePointIndex--;
            if (goingRight)
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

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

}


