using UnityEngine;

public class Shotgun : MonoBehaviour, IWeapon {
    [SerializeField] private float speed = 10f;           // geschwindigkeit der Kugel
    [SerializeField] private int damage = 10;             // Schaden pro Schuss
    [SerializeField] private static int magazineSize = 6;        // Größe des Magazins
    private int currentAmmo = magazineSize;               // Momentanes Magazin
    private int additionalAmmo;                           // Nachzuladene Kugeln
    [SerializeField] private int remainingAmmo;           // Übrige Muniton
    [SerializeField] private float fireRate = 1f;        // Schussrate pro Sekunde
    [SerializeField] private int pelletsPerShot = 5;      // Anzahl der Schrotkugeln pro Schuss
    private float nextFireTime = 0f;   // Zeit bis zum nächsten Schuss
    private Animator animator;          // Für die Animationen
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

    public void Shoot() {
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
                    bulletScript.InitializeBullet(damage, speed);
                }
            }
        }
        else
        {
            //Sound für leeres Magazin abspielen
            Debug.Log("Leeres Magazin!");
        }
    }
    public void Reload()
    {
        // Überprüfe, ob das Magazin bereits voll ist
        if (currentAmmo == magazineSize) {
            animator.SetTrigger("Reload");
            Debug.Log("Das Magazin ist bereits voll!");
            return;
        }
        // Berechne die Anzahl der Patronen, die nachgeladen werden können
        additionalAmmo = magazineSize - currentAmmo;
        // Überprüfe, ob der Spieler noch genügend zusätzliche Munition hat
        if (remainingAmmo > additionalAmmo)
        {
            animator.SetTrigger("Reload");
            // Der Spieler hat genug Munition, um das Magazin aufzufüllen
            currentAmmo = magazineSize;
            remainingAmmo -= additionalAmmo;
        }
        else
        {
            animator.SetTrigger("Reload");
            // Der Spieler hat nicht genug Munition, um das Magazin vollständig aufzufüllen
            currentAmmo += remainingAmmo;
            additionalAmmo = 0;
        }
        Debug.Log("Magazin nachgeladen!");
    }

    // Setze den Schaden der Shotgun
    public void SetDamage(int newDamage)
    {
        damage = newDamage;
    }
    // Setze den MagazineSize der Shotgun
    public void SetMagazineSize(int newMagazine)
    {
        magazineSize = newMagazine;
    }
    // Setze den FireRate der Shotgun
    public void SetFireRate(int newFireRate)
    {
        fireRate = newFireRate;
    }
}

