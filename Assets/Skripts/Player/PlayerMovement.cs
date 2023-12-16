using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Transform[] firePoints;
    public Transform firePoint;

    public GameObject bulletPrefeb;
    public Animator animator;

    private bool facingRight = true;

    void Update()
    {
        // Bewegung
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // Automatischer Firepoint-Wechsel
        if (horizontalInput < 0 && facingRight) {
            SwitchFirePoint();
            Flip();
        }
        else if (horizontalInput > 0 && !facingRight) {
            SwitchFirePoint();
            Flip();
        }

        // Animationssteuerung
        animator.SetBool("isMovingRight", horizontalInput > 0f);
        animator.SetBool("isMovingLeft", horizontalInput < 0f);

        // Debug-Log für die Überprüfung der Bewegungsrichtung
        //Debug.Log("Horizontal Input: " + horizontalInput);

        if (Input.GetButtonDown("Fire1")) {
            Shoot();
        }
    }
   

    void SwitchFirePoint() {
        // Firepoint wechseln
        foreach (Transform firePoint in firePoints)
        {
            firePoint.gameObject.SetActive(!firePoint.gameObject.activeSelf);
        }
    }

    void Flip() {
        // Drehung des Charakters umkehren
        facingRight = !facingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    void Shoot() {
        // shooting logic
        Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);
    }
}