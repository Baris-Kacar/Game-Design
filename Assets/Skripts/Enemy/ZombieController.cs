using UnityEngine;

public class ZombieController : MonoBehaviour
{
    public float detectionRange = 10f;
    public float moveSpeed = 3f;

    private Transform player;
    private Animator animator;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Check the distance between the zombie and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the detection range
        if (distanceToPlayer <= detectionRange)
        {
            // Calculate the direction towards the player
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            // Move the zombie towards the player
            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

            // Set the appropriate animation parameters based on the direction
            if (directionToPlayer.x > 0)
            {
                // Moving right
                animator.SetBool("isMovingRight", true);
                animator.SetBool("isMovingLeft", false);
                Debug.Log("Moving right");
            }
            else if (directionToPlayer.x < 0)
            {
                // Moving left
                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingLeft", true);
                Debug.Log("Moving left");
            }
            else
            {
                // Not moving horizontally, reset animations
                animator.SetBool("isMovingRight", false);
                animator.SetBool("isMovingLeft", false);
                Debug.Log("Not moving");
            }
        }
        else
        {
            // Player is out of detection range, stop moving and reset animations
            animator.SetBool("isMovingRight", false);
            animator.SetBool("isMovingLeft", false);
            Debug.Log("Not moving");
        }
    }
}
