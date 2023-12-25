using UnityEngine;

public class AssaultRifle : MonoBehaviour
{
    [SerializeField] private int damage = 10;             // Schaden pro Schuss
    [SerializeField] private int magazineSize = 30;       // Größe des Magazins
    [SerializeField] private float fireRate = 10f;        // Schussrate pro Sekunde
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

            // Erzeuge die Kugel am Feuerpunkt
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

            // Greife auf das Bullet-Skript zu und setze den Schaden
            B bulletScript = bullet.GetComponent<B>();
            if (bulletScript != null)
            {
                bulletScript.SetDamage(damage);
            }
        }
        else
        {
            //Sound für leeres Magazin abspielen
            Debug.Log("Leeres Magazin!");
        }
    }

    // Setze den Schaden der Assault Rifle
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
}
