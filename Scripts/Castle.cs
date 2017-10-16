using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour {

    public float startHealth = 750f;
    public float health;
    public Image healthBar;
    public GameObject gameOverUI;

    void Start()
    {
        health = startHealth;
    }

    public void TakeDamage(float amount)
    {
        addMoney(amount);
        health -= amount;
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            gameOverUI.SetActive(true);

            Destroy(gameObject);
        }
    }

    private void addMoney(float amount)
    {
        int amountAdding = (int)(amount * 1.3f);
        if (amount < 1) //it's a laser attack
            amountAdding = (int)(amount * 2.014f);

        if (transform.tag == "Enemy" || transform.tag == "enemyCastle")
        {
            PlayerStats.Money += amountAdding;
        }
        else
        {
            EnemyController.EnemyMoney += amountAdding;
        }
    }
    public float GetEnemyCastleHealth()
    {
        return health;
    }
}
