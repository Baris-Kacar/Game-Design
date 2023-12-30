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
    public PlayerMovement playerMovement; // Verweise hier auf deine PlayerMovement-Klasse

    private Animator animator;
    private bool isDead = false;

    // Timer for delaying game over screen
    private float timerRemaining = 2.5f;

    void Start() {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth); 
        animator = GetComponent<Animator>();
        if (animator == null) {
            Debug.LogError("Animator not found! Make sure the Animator component is attached to the GameObject.");
        } 
        // Finde und speichere die PlayerMovement-Komponente
        playerMovement = GetComponent<PlayerMovement>(); 
        // Aktiviere den Collider2D des Spielers
        gameObject.GetComponent<Collider2D>().enabled = true; 
        // Aktiviere den Rigidbody2D des Spielers oder füge ihn hinzu, wenn er nicht vorhanden ist
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null) {
            rb = gameObject.AddComponent<Rigidbody2D>();
        }
        rb.constraints = RigidbodyConstraints2D.None; 
        // Sperre die Rotation um die Z-Achse
        rb.freezeRotation = true; 
        // Aktiviere alle anderen Skripte, die die Bewegung beeinflussen könnten
        if (playerMovement != null) {
            playerMovement.enabled = true;
        }
        // Setze die Zeit zurück, falls sie pausiert wurde
        Time.timeScale = 1f;
    }

    void Update() {
        if (isDead) {
            // Reduziere den timerRemaining um die vergangene Zeit
            timerRemaining -= Time.deltaTime;
            // Friere die Position des Spielers ein
            gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            // Deaktiviere den Collider2D des Spielers, um Bewegung durch Feinde zu verhindern
            gameObject.GetComponent<Collider2D>().enabled = false;
             // Entferne oder deaktiviere den Rigidbody2D, um weitere physikalische Interaktionen zu verhindern
            Destroy(GetComponent<Rigidbody2D>());
            // Deaktiviere alle anderen Skripte, die die Bewegung beeinflussen könnten
            if (playerMovement != null) {
                playerMovement.enabled = false;
            } 
            if (timerRemaining <= 0) {
                // Pausiere das Spiel
                Time.timeScale = 0f;

                // Triggere das Game Over
                gameManager.gameOver();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            TakeDamage(20); 
            // Hier frieren wir die Position des Spielers ein, um Kollisionen mit den Gegnern zu verhindern
            FreezePlayerPosition();
        }
    }

    void FreezePlayerPosition() {
        // Friere die Position des Spielers ein
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    public void TakeDamage(int damage) {
        animator.SetTrigger("Damage");
        currentHealth -= damage;
        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0 && !isDead)
        {
            animator.SetBool("Death", true);
            isDead = true;
        }
    }
}
