using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action OnCoinCollected;
    
    public int health = 100;
    public int damage = 20; // Adjust the damage as needed
    public GameObject deathEffect;
    private Animator animator;
    

    public void Start() {
        
    }

    public void Awake()
    {
        
    
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

            if (animator == null)
            {
                animator = GetComponent<Animator>();
                if (animator == null)
                {
                    Debug.LogError("Animator not found Line 57");
                }
            }
            Die();
           // ScoreScript.score += 10;
        }
    }

    void Die()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
       // Destroy(gameObject);
    }
    
}
