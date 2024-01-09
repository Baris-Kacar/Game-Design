using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement: MonoBehaviour {
    public Character_Controller controller; // Referenz zum Charakter-Controller (CC)

    public float runSpeed = 20f; // Standard-Laufgeschwindigkeit
    public float dashSpeed = 80f; // Geschwindigkeit beim Sprinten (Dash)

    public List<IWeapon> weapons; // Verwende das IWeapon-Interface in der Liste aller Waffen
    private int currentWeaponIndex = 0; // Index der aktuellen Waffe
    private IWeapon currentWeapon; // Referenz zur aktuellen Waffe

    private Pistol pistol; // Referenz zum Pistol-Skript

    private Shotgun shotgun; // Referenz zum Shotgun-Skript
    private bool shotgunUnlocked = false; // Zeigt an, ob die Schrotflinte freigeschaltet ist
    private int requiredPointsForShotgun = 100; // Beispiel: Der Spieler ben�tigt 100 Punkte, um die Schrotflinte freizuschalten

    private AssaultRifle assualtrifle; // Referenz zum AssaultRifle-Skript
    private bool assualtrifleUnlocked = false; // Zeigt an, ob die AssaultRifle freigeschaltet ist
    private int requiredPointsForAssualtRifle = 200; // Beispiel: Der Spieler ben�tigt 200 Punkte, um die AssaultRifle freizuschalten

    private Animator animator;
    private TrailRenderer trailrenderer;
    private bool isDashing = false; // Zeigt an, ob gerade gedashed wird

    float horizontalMove = 0f; // Horizontale Bewegung
    float verticalMove = 0f; // Vertikale Bewegung

    public Transform firePoint;         // Punkt, an dem die Kugel abgefeuert wird
    public GameObject bulletPrefab;     // Prefab der Kugel

    void Start() {
        // Weise der Variable animator einen Wert zu
        animator = GetComponent<Animator>();
        // Weise der Variable trailrenderer einen Wert zu
        trailrenderer = GetComponent<TrailRenderer>();
        // �berpr�fe, ob der Animator gefunden wurde
        if (animator == null) {
            Debug.LogError("Animator not found! Make sure the Animator component is attached to the GameObject.");
        }
        // �berpr�fe, ob der TrailRenderer gefunden wurde
        if (trailrenderer == null) { 
            Debug.LogError("TrailRenderer not found! Make sure the TrailRenderer component is attached to the GameObject.");
        }
        // Initialisiere die Waffen
        InitializeWeapons();
        // Setze die aktuelle Waffe beim Start
        SetCurrentWeapon();
    }
    void InitializeWeapons() {
        weapons = new List<IWeapon>();
        weapons.Add(new Pistol());
        weapons.Add(new Shotgun());
        weapons.Add(new AssaultRifle());
    }
    void Update() {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Horizontale Bewegung basierend auf der Eingabe
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed; // Vertikale Bewegung basierend auf der Eingabe

        animator.SetFloat("SpeedH", Mathf.Abs(horizontalMove));
        animator.SetFloat("SpeedV", Mathf.Abs(verticalMove));

        // Dash, wenn Left Shift gedr�ckt wird
        if (Input.GetButtonDown("Jump") && !isDashing) {
            Dash();
        }
        // Schie�t, wenn die Leertaste gedr�ckt wird
        if (Input.GetButtonDown("Fire1") || Input.GetMouseButtonDown(0)) {
            Shoot();
        }
        // Wechselt die Waffe, wenn die Taste f�r den Waffenwechsel gedr�ckt wird (z.B., Tasten "1,2,3")
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            animator.SetTrigger("Weapon_Change");
            SwitchWeapon(0); // Waffe 1 (Pistol)
            animator.SetInteger("Pull", 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) {
            if (!shotgunUnlocked) {
                Debug.Log("Noch nicht freigeschaltet.");
            } else {
                animator.SetTrigger("Weapon_Change");
                SwitchWeapon(1); // Waffe 2 (Shotgun)
                animator.SetInteger("Pull", 2);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) {
            if (!assualtrifleUnlocked) {
                Debug.Log("Noch nicht freigeschaltet.");
            } else {
                animator.SetTrigger("Weapon_Change");
                SwitchWeapon(2); // Waffe 3 (AssaultRifle)
                animator.SetInteger("Pull", 3);
            }
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            Reload();
        }
    }
    void FixedUpdate() {
        // Bewege unseren Charakter
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);

    }
    void Dash() {
        isDashing = true;
        // Speichere die urspr�ngliche Laufgeschwindigkeit
        float originalRunSpeed = runSpeed;
        // Erh�he die Laufgeschwindigkeit f�r den Sprint
        runSpeed = dashSpeed;
        // Deaktiviere die Kollision mit dem EnemyLayer w�hrend des Dashes
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, true);
        // Trail aktivieren
        trailrenderer.emitting = true;
        // Setze die Lebensdauer des Trails
        trailrenderer.time = 0.2f; 
        // Setze die Laufgeschwindigkeit nach dem Sprint zur�ck
        StartCoroutine(ResetDash(originalRunSpeed));
    }
    IEnumerator ResetDash(float originalRunSpeed) {
        yield return new WaitForSeconds(0.2f); // Pausiere f�r die Dauer des Sprints (Anpassung nach Bedarf)

        runSpeed = originalRunSpeed;
        // Deaktiviere den Trail
        trailrenderer.emitting = false;
        // Aktiviert die Kollision mit dem EnemyLayer w�hrend des Dashes
        Physics2D.IgnoreLayerCollision(gameObject.layer, gameObject.layer, false);
        // Wartezeit, bevor die Kollision mit dem EnemyLayer wieder aktiviert wird
        yield return new WaitForSeconds(1f);
        isDashing = false;
    }
    void SwitchWeapon(int index) {
        // Deaktiviert die aktuelle Waffe
        currentWeaponIndex = index;
        StartCoroutine(DelayedWeaponSwitch()); // Aktualisiert die aktuelle Waffe
        
    }
    IEnumerator DelayedWeaponSwitch() {
        yield return new WaitForSeconds(0.5f);
        SetCurrentWeapon(); // Aktualisiert die aktuelle Waffe
        Debug.Log($"Switched to {currentWeapon.GetType().Name}"); // Gibt den Namen der aktuellen Waffe aus (n�tzlich f�r Debugging)
    }
    void SetCurrentWeapon() {
        // Setzt die Referenz zur aktuellen Waffe
        currentWeapon = weapons[currentWeaponIndex];
    }
    public void Shoot() {
        if(currentWeapon != null) {
            currentWeapon.Shoot(animator, firePoint, bulletPrefab);
        }
    }
    public void Reload() {
        if (currentWeapon != null) {
            currentWeapon.Reload(animator);
        } else {
            Debug.Log(currentWeapon);
            Debug.Log("Nachladen nicht m�glich.");
        }
    }
}
