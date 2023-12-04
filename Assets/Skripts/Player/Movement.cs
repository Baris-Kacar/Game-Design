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

        // Set the "isMovingRight" and "isMovingLeft" parameters in the Animator
        animator.SetBool("isMovingRight", horizontalInput > 0f);
        animator.SetBool("isMovingLeft", horizontalInput < 0f);

        // Move the character based on input
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
        
        
    }
}


