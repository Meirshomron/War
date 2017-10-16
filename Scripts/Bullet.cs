
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Transform target;
    public int damage = 50;
    public float speed = 70f;
    public GameObject impactEffect;
    public float explosionRadius = 0f;

    public void seek(Transform _target)
    {
        target = _target;
    }

	void Update () {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        //if we hit and not passed it
        if(dir.magnitude<= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame,Space.World);
        transform.LookAt(target);
    }

    void HitTarget()
    {
        GameObject effectIns=(GameObject)Instantiate(impactEffect,transform.position,transform.rotation);
        Destroy(effectIns, 3f);
        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }
        Destroy(gameObject);
    }

    void Damage(Transform enemy)
    {
        Turret enemyTurrent =null;
        Castle enemyCastle = null;

        if (enemy.tag == "Enemy" || enemy.tag == "Player")
            enemyTurrent = enemy.GetComponent<Turret>();
        else
            enemyCastle = enemy.GetComponent<Castle>();

        if (enemyTurrent != null)
            enemyTurrent.TakeDamage(damage);
        else if(enemyCastle != null)
            enemyCastle.TakeDamage(damage);
    }

    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius);
        foreach(Collider collider in colliders)
        {
            //collider.tag == target.tag ||
            if ((target.tag == "Enemy" || target.tag == "enemyCastle") && (collider.tag == "Enemy" || collider.tag == "enemyCastle"))
            {
                Damage(collider.transform);
            }
            else if ((target.tag == "Player" || target.tag == "playerCastle") && (collider.tag == "Player" || collider.tag == "playerCastle"))
            {
                Damage(collider.transform);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRadius);
    }  
}
