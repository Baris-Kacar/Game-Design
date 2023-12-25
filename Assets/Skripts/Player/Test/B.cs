using UnityEngine;

public class B : MonoBehaviour
{
    public float speed = 20f; // Geschwindigkeit der Kugel
    public Rigidbody2D rb;
    private int damage = 0; // Standard-Schaden

    // Start wird vor dem ersten Frame-Update aufgerufen
    void Start()
    {
        rb.velocity = transform.right * speed; // Setze die Anfangsgeschwindigkeit der Kugel
    }

    // Wird aufgerufen, wenn der Renderer für jede Kamera unsichtbar wird
    void OnBecameInvisible()
    {
        // Zerstöre die Kugel, wenn sie unsichtbar wird
        Destroy(gameObject);
    }

    // Setze den Schaden der Kugel
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    // Setze die Geschwindigkeit der Kugel
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
        if (rb != null)
        {
            rb.velocity = transform.right * speed;
        }
    }

    // Wird aufgerufen, wenn die Kugel mit einem anderen Collider kollidiert
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Gehe davon aus, dass das Objekt eine Health-Komponente hat
        Health health = collision.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        // Zerstöre die Kugel bei der Kollision
        Destroy(gameObject);
    }
}
