/*using UnityEngine;

public class AssaultRifle : MonoBehaviour, IWeapon
{
    [SerializeField] private float speed = 10f;           // geschwindigkeit der Kugel
    [SerializeField] private int damage = 50;             // Schaden pro Schuss
    [SerializeField] private static int magazineSize = 30;       // Größe des Magazins
    private int currentAmmo = magazineSize;               // Momentanes Magazin
    private int additionalAmmo;                           // Nachzuladene Kugeln
    [SerializeField] private int remainingAmmo;           // Übrige Muniton
    [SerializeField] private float fireRate = 10f;        // Schussrate pro Sekunde
    [SerializeField] private float nextFireTime = 0.0f;   // Zeit bis zum nächsten Schuss
    
    //public Transform firePoint;         // Punkt, an dem die Kugel abgefeuert wird
    //public GameObject bulletPrefab;     // Prefab der Kugel

    void Update() {
        // Wenn der Spieler schießt und die Schussrate eingehalten wird
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime) {
            nextFireTime = Time.time + 1f / fireRate; // Setze die nächste mögliche Schusszeit
        }
    }

    public void Shoot(Animator animator, Transform firePoint, Gameobject bulletPrefab) {
        // Überprüfe, ob noch Kugeln im Magazin sind
        if (currentAmmo > 0) {
            if (bulletPrefab != null && firePoint != null) {
                // Setzt den Trigger für die Animation
                animator.SetTrigger("Shoot");
                // Reduziere die Anzahl der Kugeln im Magazin
                currentAmmo--;
                // Erzeuge eine Kugel am Feuerpunkt
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                // Greife auf das Bullet-Skript zu und setze den Schaden
                B bulletScript = bullet.GetComponent<B>();
                if (bulletScript != null) {
                    bulletScript.InitializeBullet(damage, speed);
                }
            }
        } else {
            //Sound für leeres Magazin abspielen
            Debug.Log("Leeres Magazin!");
        }
    }

    public void Reload(Animator animator) {
        // Überprüfe, ob das Magazin bereits voll ist
        if (currentAmmo == magazineSize) {
            Debug.Log("Das Magazin ist bereits voll!");
            return;
        }
        // Berechne die Anzahl der Patronen, die nachgeladen werden können
        additionalAmmo = magazineSize - currentAmmo;
        // Überprüfe, ob der Spieler noch genügend zusätzliche Munition hat
        if (remainingAmmo > additionalAmmo) {
            animator.SetTrigger("Reload");
            // Der Spieler hat genug Munition, um das Magazin aufzufüllen
            currentAmmo = magazineSize;
            remainingAmmo -= additionalAmmo;
        } else {
            animator.SetTrigger("Reload");
            // Der Spieler hat nicht genug Munition, um das Magazin vollständig aufzufüllen
            currentAmmo += remainingAmmo;
            additionalAmmo = 0;
        }
        Debug.Log("Magazin nachgeladen!");
    }

    public void SetDamage(int newDamage) {
        damage = newDamage;
    }
    public void SetMagazineSize(int newMagazine) {
        magazineSize = newMagazine;
    }
    public void SetFireRate(int newFireRate) {
        fireRate = newFireRate;
    }
}
*/