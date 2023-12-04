using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this to control movement speed

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check for movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Check if the character is moving
        bool isMoving = (horizontalInput != 0f || verticalInput != 0f);

        // Debug statement to check if isMoving is being set correctly
        Debug.Log("isRunning: " + isMoving);

        // Set the "isRunning" parameter in the Animator
        animator.SetBool("isRunning", isMoving);

        // Move the character based on input
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

    }
}
