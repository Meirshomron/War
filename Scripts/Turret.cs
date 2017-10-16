using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Turret : MonoBehaviour {

    private Transform target;
   // private enemy targetEnemy;

    [Header("General")]
    public float range = 15f;
    public float startHealth = 100;
    private float health;

    [Header("Use bullets(defualt)")]
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Use laser")]
    public bool useLaser = false;
    public LineRenderer lineRenderer;
    public ParticleSystem impactEffect;
    public Light impactLight;
    public int damageOverTime = 100;
    public float slowAmount = .5f;

    [Header("Unity setup fields")]

    public string enemyTag = "Enemy";
    public string enemyCastleTag = "enemyCastle";
    public GameObject deathEffect;
    public Transform partToRotate;
    public float turnSpeed = 10f;
    public Transform firePoint;

    [Header("Unity Stuff")]
    public Image healthBar;   

    void Start () {
        if (transform.tag == "Enemy")
        {
            enemyTag = "Player";
            enemyCastleTag = "playerCastle";
        }

        health = startHealth;
        InvokeRepeating("UpdateTarget",0f,0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject[] enemyCastle = GameObject.FindGameObjectsWithTag(enemyCastleTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;
        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (shortestDistance > distanceToEnemy)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else if (enemyCastle != null && Vector3.Distance(transform.position, enemyCastle[0].transform.position)<range)
            target = enemyCastle[0].transform;
        else
        {
            target = null;
        }
    }
	
	void Update () {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LockOnTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                shoot();
                fireCountdown = 1f / fireRate;
            }
            fireCountdown -= Time.deltaTime;
        }
    }

    void Laser()
    {
        if(target.tag != enemyCastleTag)
            target.GetComponent<Turret>().TakeDamage(damageOverTime*Time.deltaTime);
        else
            target.GetComponent<Castle>().TakeDamage(damageOverTime * Time.deltaTime);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }
        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position +dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    void LockOnTarget()
    {
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    void shoot()
    {
        GameObject bulletGO= (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        if(bullet!= null)
        {
            bullet.seek(target);
        }
    }

    public void TakeDamage(float amount)
    {
        addMoney(amount);
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            //Die();
            Destroy(gameObject);
        }
    }

    private void addMoney(float amount)
    {
        int amountAdding = (int)(amount * 1.3f);
        if (amount < 1) //it's a laser attack
            amountAdding = (int)(amount * 2.014f);
        if (transform.tag == "Enemy")
        {
            PlayerStats.Money += amountAdding;
            Debug.Log(amountAdding);

        }
        else
        {
            EnemyController.EnemyMoney += amountAdding;
        }       
    }

    public Transform getTarget()
    {
        return target;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
