using UnityEngine;

public class Character_Controller : MonoBehaviour {
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f; // Glatte Bewegung des Charakters
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    private Vector3 velocity = Vector3.zero; // Geschwindigkeitsvektor für die Bewegung
    private bool m_FacingRight = true; // Hinzugefügte fehlende Variable für die Blickrichtung des Charakters

    private void Awake() {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void Move(float moveHorizontal, float moveVertical) {
        // Setze die Zielgeschwindigkeit für die horizontale Bewegung
        Vector3 targetVelocity = new Vector2(moveHorizontal * 10f, m_Rigidbody2D.velocity.y);
        // Wende die glatte Bewegung an
        m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

        // Überprüfe und kehre die Blickrichtung um, wenn sich die Bewegung in die entgegengesetzte Richtung bewegt
        if ((moveHorizontal > 0 && !m_FacingRight) || (moveHorizontal < 0 && m_FacingRight))
        {
            Flip();
        }

        // Erlaube vertikale Bewegung (du kannst zusätzliche Bedingungen hinzufügen, wenn benötigt)
        m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, moveVertical * 10f);
    }


    private void Flip() {
        // Ändere die Art und Weise, wie der Spieler als Blickrichtung markiert ist.
        m_FacingRight = !m_FacingRight;

        // Drehe den Charakter um 180 Grad
        transform.Rotate(0f, 180f, 0f);
    }

}
