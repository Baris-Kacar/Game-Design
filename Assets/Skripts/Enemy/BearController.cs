using UnityEngine;

public class BearController : MonoBehaviour
{
    public float detectionRange = 40f;
    public float attackRange = 1f;
    public float attackTriggerDistance = 0.2f; 
    public float moveSpeed = 3f;
    public float attackCooldown = 3f;
    public GameObject bossPrefab;
    
    private Transform player;
    private Animator animator;
    private bool isCloseEnoughToAttack;
    private bool isAttacking;
    private float cooldownTimer;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }
    
    public void SpawnBoss(Transform playerTransform)
    {
        if (bossPrefab != null)
        {
            Vector3 distanz = new Vector3(10f, 0f, 0f);
            // Spawn the boss right in front of the player
            Vector3 spawnPosition = playerTransform.position + distanz + playerTransform.forward * 2f; // Adjust the distance as needed
            Instantiate(bossPrefab, spawnPosition, Quaternion.identity);
            Debug.Log("Boss spawned!");
        }
        else
        {
            Debug.LogError("BossPrefab is not assigned to BearController or is null.");
        }
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;

            transform.Translate(directionToPlayer * moveSpeed * Time.deltaTime);

            isCloseEnoughToAttack = distanceToPlayer < attackTriggerDistance;

            SetAnimationParameters(directionToPlayer.x > 0, directionToPlayer.x < 0, isCloseEnoughToAttack);

            // Check if the zombie can attack based on the cooldown timer
            if (isCloseEnoughToAttack && !isAttacking && cooldownTimer <= 0f)
            {
                Debug.Log(distanceToPlayer);
                animator.SetTrigger("Attack");
                isAttacking = true;
                cooldownTimer = attackCooldown; // Set the cooldown timer
            }

            // Update the cooldown timer
            if (cooldownTimer > 0f)
            {
                cooldownTimer -= Time.deltaTime;
            }
            else if (isAttacking) // Reset the attack state after the attack animation finishes
            {
                isAttacking = false;
                // Transition back to running animation immediately after attack animation
                animator.SetBool("isAttacking", false);
                animator.SetBool("isMovingRight", true);
            }
        }
        else
        {
            SetAnimationParameters(false, false, false);
            cooldownTimer = 0f; // Reset the cooldown timer when the player is out of range
           // Debug.Log("Not moving");
        }
    }

    private void SetAnimationParameters(bool isMovingRight, bool isMovingLeft, bool isCloseEnoughToAttack)
    {
        animator.SetBool("isMovingRight", isMovingRight);
        animator.SetBool("isMovingLeft", isMovingLeft);
        animator.SetBool("isCloseEnoughToAttack", isCloseEnoughToAttack);
       // Debug.Log(isMovingRight ? "Moving right" : isMovingLeft ? "Moving left" : "Not moving");
    }
}
    



