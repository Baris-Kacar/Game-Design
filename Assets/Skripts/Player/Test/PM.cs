using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PM : MonoBehaviour
{
    public CC controller; // Referenz zum Charakter-Controller (CC)
    public float runSpeed = 20f; // Standard-Laufgeschwindigkeit
    public float dashSpeed = 80f; // Geschwindigkeit beim Sprinten (Dash)

    private Animator animator;
    private TrailRenderer trailrenderer;
    private bool isDashing = false; // Zeigt an, ob gerade gesprintet wird

    float horizontalMove = 0f; // Horizontale Bewegung
    float verticalMove = 0f; // Vertikale Bewegung

    void Start() {
        // Weise der Variable animator einen Wert zu
        animator = GetComponent<Animator>();
        // Weise der Variable trailrenderer einen Wert zu
        trailrenderer = GetComponent<TrailRenderer>();
        // Überprüfe, ob der Animator gefunden wurde
        if (animator == null) {
            Debug.LogError("Animator not found! Make sure the Animator component is attached to the GameObject.");
        }
        // Überprüfe, ob der TrailRenderer gefunden wurde
        if (trailrenderer == null) { 
            Debug.LogError("TrailRenderer not found! Make sure the TrailRenderer component is attached to the GameObject.");
        }
    }


    // Update wird einmal pro Frame aufgerufen
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed; // Horizontale Bewegung basierend auf der Eingabe
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed; // Vertikale Bewegung basierend auf der Eingabe

        animator.SetFloat("SpeedH", Mathf.Abs(horizontalMove));
        animator.SetFloat("SpeedV", Mathf.Abs(verticalMove));

        // Dash-Eingabe
        if (Input.GetButtonDown("Jump") && !isDashing)
        {
            Dash();
        }
    }

    void FixedUpdate()
    {
        // Bewege unseren Charakter
        controller.Move(horizontalMove * Time.fixedDeltaTime, verticalMove * Time.fixedDeltaTime);

    }

    void Dash()
    {
        isDashing = true;

        // Speichere die ursprüngliche Laufgeschwindigkeit
        float originalRunSpeed = runSpeed;

        // Erhöhe die Laufgeschwindigkeit für den Sprint
        runSpeed = dashSpeed;

        // Trail aktivieren
        trailrenderer.emitting = true;
        // Setze die Lebensdauer des Trails
        trailrenderer.time = 0.2f; 
        // Setze die Laufgeschwindigkeit nach dem Sprint zurück
        StartCoroutine(ResetDash(originalRunSpeed));
    }

    IEnumerator ResetDash(float originalRunSpeed)
    {
        yield return new WaitForSeconds(0.5f); // Pausiere für die Dauer des Sprints (Anpassung nach Bedarf)

        runSpeed = originalRunSpeed;
        isDashing = false;
        // Deaktiviere den Trail
        trailrenderer.emitting = false;
    }
}
