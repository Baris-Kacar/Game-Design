using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 100;
    public int damage = 20; // Adjust the damage as needed
    public GameObject deathEffect;
    private Animator animator;

    public void Start() {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator not found! Make sure the Animator component is attached to the GameObject.");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null) {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0) {
            
            Die();
            animator.SetTrigger("Death");
            ScoreScript.scoreValue += 10;
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    
}
