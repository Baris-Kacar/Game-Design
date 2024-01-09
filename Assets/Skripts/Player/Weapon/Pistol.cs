using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pistol : MonoBehaviour, IWeapon {
    [SerializeField] private float speed = 10f;           // geschwindigkeit der Kugel
    [SerializeField] private int damage = 25;             // Schaden pro Schuss
    [SerializeField] private static int magazineSize = 12;// Gr��e des Magazins
    public int currentAmmo = 12;               // Momentanes Magazin
    private int additionalAmmo;                           // Nachzuladene Kugeln
    [SerializeField] private int remainingAmmo = magazineSize;           // �brige Muniton (Lager)
    [SerializeField] private float fireRate = 0.8f;       // Schussrate pro Sekunde
    [SerializeField] private float nextFireTime = 0.2f;   // Zeit bis zum n�chsten Schuss
    [SerializeField] private AudioManager audiomanager;
    [SerializeField] private AudioClip gunshotSound;
    [SerializeField] private AudioClip reloadSound;
    [SerializeField] private TextMeshProUGUI ammoText;
    

    
    void Start()
    {
        
        if (ammoText == null)
        {
            Debug.LogError("Ammo Text not assigned in the insepctor");
        }
        else
        {
            UpdateAmmoText();
        }
    }

    void Update() {
        // Wenn der Spieler schie�t und die Schussrate eingehalten wird
        if (Input.GetButtonDown("Fire1") && Time.time > nextFireTime) {
            nextFireTime = Time.time + 1f / fireRate; // Setze die n�chste m�gliche Schusszeit
        }
        Debug.Log(currentAmmo);
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0 || Input.GetMouseButtonDown(0) && currentAmmo > 0)
        {
            if (audiomanager != null && gunshotSound != null)
            {
                audiomanager.PlaySFX(gunshotSound);
                currentAmmo--;
                
            }
            
        }
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < 12 || currentAmmo == 0)
        {
            currentAmmo = 12;
           
            audiomanager.PlaySFX(reloadSound);
        }
    
        UpdateAmmoText();
    }

    public void Shoot(Animator animator, Transform firePoint, GameObject bulletPrefab) {
        // �berpr�fe, ob noch Kugeln im Magazin sind
        if (currentAmmo > 0) {
            if (bulletPrefab != null && firePoint != null) {
                Debug.Log("Prefab and firepoint");
                // Setzt den Trigger f�r die Animation
                animator.SetTrigger("Shoot");
                // Reduziere die Anzahl der Kugeln im Magazin
                currentAmmo--;
                // Erzeuge eine Kugel am Feuerpunkt
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                // Greife auf das Bullet-Skript zu und setze den Schaden
                Bullet bulletScript = bullet.GetComponent<Bullet>();
                
                if (bulletScript != null) {
                    bulletScript.InitializeBullet(damage, speed);
                    
                }
            }
        } else
        {
            currentAmmo = 12;
            animator.SetTrigger("Reload");
            //Sound f�r leeres Magazin abspielen
            Debug.Log("Leeres Magazin!");
        }
    }

    public void Reload(Animator animator) {
        // �berpr�fe, ob das Magazin bereits voll ist
        if (currentAmmo == magazineSize) {
            Debug.Log("Das Magazin ist bereits voll!");
            return;
        }
        // Berechne die Anzahl der Patronen, die nachgeladen werden k�nnen
        additionalAmmo = magazineSize - currentAmmo;
        // Debug-Ausgabe f�r �berpr�fung
        Debug.Log("Remaining Ammo: " + remainingAmmo);
        Debug.Log("Additional Ammo: " + additionalAmmo);
        // �berpr�fe, ob der Spieler noch gen�gend zus�tzliche Munition hat
        if (remainingAmmo >= additionalAmmo ) {
            animator.SetTrigger("Reload");
            // Der Spieler hat genug Munition, um das Magazin aufzuf�llen
            currentAmmo = magazineSize; 
            remainingAmmo -= additionalAmmo;
            Debug.Log("Magazin aufgef�llt!");
        } else {
            animator.SetTrigger("Reload");
            // Der Spieler hat nicht genug Munition, um das Magazin vollst�ndig aufzuf�llen
            currentAmmo += remainingAmmo;
            remainingAmmo = 0;
            Debug.Log("Magazin teilweise aufgef�llt!");
        }
        remainingAmmo = magazineSize; //Unendlich Muniton
        // Debug-Ausgabe am Ende der Reload-Funktion
        Debug.Log("Nachladen abgeschlossen. Current Ammo: " + currentAmmo + " Remaining Ammo: " + remainingAmmo);
        Debug.Log("Magazin nachgeladen!");
    }

    //magazineSize Gr��e des Magazins
    //currentAmmo Momentanes Magazin
    //additionalAmmo Nachzuladene Kugeln
    //remainingAmmo �brige Muniton

    public void SetDamage(int newDamage) {
        damage = newDamage;
    }
    public void SetMagazineSize(int newMagazine) {
        magazineSize = newMagazine;
    }
    public void SetFireRate(int newFireRate) {
        fireRate = newFireRate;
    }

    void UpdateAmmoText()
    {
        ammoText.text = $"{currentAmmo}/\u221e";
        
    }
   
}
