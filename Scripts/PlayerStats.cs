using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public static int Money;
    public static Vector3 Location;
    public int startMoney = 400;
    public static float startTime;

    private void Start()
    {
        Money = startMoney;
        Location = transform.position;
        startTime = Time.realtimeSinceStartup;
        Debug.Log("startTime is "+ startTime);
    }

}
