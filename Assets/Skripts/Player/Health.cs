using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthbar;
    public GameManagerScript gameManager;
   
    private bool isDead;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);

        if (currentHealth <= 0 && !isDead)
        {
            Time.timeScale = 0f;

            isDead = true;
           // gameObject.SetActive(false);
            //gameObject.CompareTag("Enemy").SetActive(false);
           // GameObject.FindGameObjectWithTag("Enemy").SetActive(false);
            gameManager.gameOver();
        }
    }

    void Respawn()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
        healthbar.SetHealth(currentHealth);
        
    }
}