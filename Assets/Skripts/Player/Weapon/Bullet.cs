using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody2D rb;
    private float speed = 0; // Geschwindigkeit der Kugel
    private int damage = 0; // Standard-Schaden

    // Start wird vor dem ersten Frame-Update aufgerufen
    void Start() {
        rb.velocity = transform.right * speed; // Setze die Anfangsgeschwindigkeit der Kugel
    }

    // Wird aufgerufen, wenn der Renderer für jede Kamera unsichtbar wird
    void OnBecameInvisible() {
        // Zerstöre die Kugel, wenn sie unsichtbar wird
        Destroy(gameObject);
    }

    public void InitializeBullet(int damage, float speed) {
        this.damage = damage;
        this.speed = speed;
    }

    void OnTriggerEnter2D(Collider2D collision) {
        // Gehe davon aus, dass das Objekt eine Health-Komponente hat
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        // Zerstöre die Kugel bei der Kollision
        Destroy(gameObject);
    }
}
