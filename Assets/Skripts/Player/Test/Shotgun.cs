using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private int damage = 10;             // Schaden pro Schuss
    [SerializeField] private int magazineSize = 6;        // Größe des Magazins
    [SerializeField] private float fireRate = 1f;        // Schussrate pro Sekunde
    [SerializeField] private int pelletsPerShot = 5;      // Anzahl der Schrotkugeln pro Schuss
    private float nextFireTime = 0f;   // Zeit bis zum nächsten Schuss
    public Transform firePoint;         // Punkt, an dem die Kugel abgefeuert wird
    public GameObject bulletPrefab;     // Prefab der Kugel

    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        // Wenn der Spieler schießt und die Schussrate eingehalten wird
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Setze die nächste mögliche Schusszeit
        }
    }

    void Shoot()
    {
        // Überprüfe, ob noch Kugeln im Magazin sind
        if (magazineSize > 0)
        {
            // Reduziere die Anzahl der Kugeln im Magazin
            magazineSize--;

            // Schieße mehrere Schrotkugeln
            for (int i = 0; i < pelletsPerShot; i++)
            {
                // Erzeuge die Kugel am Feuerpunkt mit leicht zufälliger Rotation
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.Euler(0, 0, Random.Range(-5f, 5f)));

                // Greife auf das Bullet-Skript zu und setze den Schaden
                B bulletScript = bullet.GetComponent<B>();
                if (bulletScript != null)
                {
                    bulletScript.SetDamage(damage);
                }
            }
        }
        else
        {
            //Sound für leeres Magazin abspielen
            Debug.Log("Leeres Magazin!");
        }
    }

    // Setze den Schaden der Shotgun
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
