using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathEffect;

    private int health = 30;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject elem = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(elem, 1f);
        Destroy(gameObject);
    }

    public void SetHealth(int health)
    {
        this.health = health;
    }
}
