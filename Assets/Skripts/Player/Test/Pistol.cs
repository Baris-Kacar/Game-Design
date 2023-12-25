using UnityEngine;

public class Pistol : MonoBehaviour 
{
    public int damage = 30;             // Schaden pro Schuss
    public int magazineSize = 12;       // Gr��e des Magazins
    public float fireRate = 0.8f;       // Schussrate pro Sekunde
    private Animator animator;          // F�r die Animationen
    private float nextFireTime = 0.2f;   // Zeit bis zum n�chsten Schuss
    public Transform firePoint;         // Punkt, an dem die Kugel abgefeuert wird
    public GameObject bulletPrefab;     // Prefab der Kugel

    void Start() {
        // Weise der Variable animator einen Wert zu
        animator = GetComponent<Animator>(); 
        // �berpr�fe, ob der Animator gefunden wurde
        if (animator == null) {
            Debug.LogError("Animator not found! Make sure the Animator component is attached to the GameObject.");
        }
    }

    // Update wird einmal pro Frame aufgerufen
    void Update() {

        // Wenn der Spieler schie�t und die Schussrate eingehalten wird
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime) {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Setze die n�chste m�gliche Schusszeit
        }
    }

    void Shoot() {
        // �berpr�fe, ob noch Kugeln im Magazin sind
        if (magazineSize > 0) {
            if (bulletPrefab != null && firePoint != null) {
                // Setzt den Trigger f�r die Animation
                animator.SetTrigger("Shoot");
                // Reduziere die Anzahl der Kugeln im Magazin
                magazineSize--;
                // Erzeuge eine Kugel am Feuerpunkt
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                // Greife auf das Bullet-Skript zu und setze den Schaden
                B bulletScript = bullet.GetComponent<B>();
                if (bulletScript != null) {
                    bulletScript.SetDamage(damage);
                }
            }
        } else {
            //Sound f�r leeres Magazin abspielen
            Debug.Log("Leeres Magazin!");
        }
    }
    // Setze den Schaden der Pistol
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }

    
}
