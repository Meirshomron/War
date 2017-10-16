using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

    public TurrentBlueprint[] bluePrint= new TurrentBlueprint[3];

    public static int EnemyMoney;
    public int startMoney = 600;
    public Vector3[,] locations =new Vector3[3,16];

    private bool ShouldBuildEnemy = false;
    private bool alive = true;
    private float underAttack;

    public Castle myCastle;

    void Start()
    {
        initialLocations();
        EnemyMoney = startMoney;
        StartCoroutine(BuildEnemy());
        underAttack = myCastle.GetEnemyCastleHealth();
    }

    void initialLocations()
    {
        int l = -1;
        for (int i = 104; i < 121; i = i + 8)
        {
            l++;
            int r = -1;
            for (float j = 40; j < 152; j = j + 7f)
            {
                r++;
                float z = j;
                float x = i;

                if (x == 120 && z == 96)
                    x = 112;

                locations[l, r] = new Vector3(x, 1, z);
            }
        }
    }

    IEnumerator BuildEnemy()
    {
        while(alive)
        {
            if (myCastle.GetEnemyCastleHealth() < underAttack) //enemy is under attack
                defendCastle();
            else
                randomBuild();

            ShouldBuildEnemy = false;
            underAttack = myCastle.GetEnemyCastleHealth();
            yield return new WaitForSeconds(4f);
            //here to add code to change the wait time between every troop made.atm it's 4 secs between every troop/defense wave
            ShouldBuildEnemy = true;        
        }
    }

    public void defendCastle()
    {
        int amonutSpending = EnemyMoney / 2;
        while (EnemyMoney >= amonutSpending)
        {
            Vector3 selected = WhereBuilding(0,2,6,9);
            GameObject prefab = WhatBuilding().prefab;
            Instantiate(prefab, selected, Quaternion.identity);
        }
    }

    public void randomBuild()
    {
        if (ShouldBuildEnemy)
        {
            Vector3 selected = WhereBuilding(0,2,0,16);
            GameObject prefab = WhatBuilding().prefab;
            if (prefab)
                Instantiate(prefab, selected, Quaternion.identity);
            
        }
    }

    public Vector3 WhereBuilding(int minX, int maxX, int minY, int maxY)
    {
        Vector3 res = locations[Random.Range(minX, maxX), Random.Range(minY, maxY)];
        return res;
    }

    public TurrentBlueprint WhatBuilding()
    {
        int num = Random.Range(0, 100);//build randomly atm.
        if (num <= 33)
            num = 0;
        else if (num <= 67)
            num = 1;
        else
            num = 2;

        TurrentBlueprint ans = bluePrint[num];

        if (canBuild(ans))
            return ans;
        return null;
    }

    public bool canBuild(TurrentBlueprint enemy)
    {
        if (EnemyMoney - enemy.cost >= 0)
        {
            EnemyMoney -= enemy.cost;
            return true;
        }
        else
        {
            return false; 
        }
    }

    /*2 responsibilities:
     * 1.location of new enemy.
     * 2.what enemy to build.
     * 
     * measures:
     * 1.if being attacked: 1.build near attack location
     *                        1.1.randomly around the attacked location as long as in reach.
     *                      1.2.build the best enemy to deal with attack
     *                          1.2.1. if attacked by more than 2 troops-build missiles
     *                               if can't afford or else- if can afford, build laser else build standard.  
     * 2.up to AI!
     *  we can take into account the amount of troops player has on the floor.
     *  we can take into account the amount of money we have- a little- build many weak troops otherwise build expansive
     *  we can follow what troops player likes building.
     *  we can go random.
     *  
     * 
    */



}
