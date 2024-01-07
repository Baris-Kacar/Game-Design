using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHerd : MonoBehaviour
{
    public float baseSpeed = 0.4f; // The base speed of the ZombieHerd
    public float distanceMultiplier = 1.5f; // Multiplier for distance-based speed adjustment
    private Transform player;
    private Transform hitbox; // The hitbox in front of the zombie herd

    void Start()
    {
       
        player = GameObject.FindGameObjectWithTag("Player").transform;

        
        GameObject hitboxObject = GameObject.FindGameObjectWithTag("Hitbox");

        if (player == null)
        {
            Debug.LogError("Player not found!");
        }

        if (hitboxObject == null)
        {
            Debug.LogError("Hitbox not found!");
        }
        else
        {
            hitbox = hitboxObject.transform;
        }
    }

    void Update()
    {
        if (hitbox != null)
        {
            // Calculate the distance between the hitbox and the player
            float distanceToPlayer = Vector2.Distance(hitbox.position, player.position);

            // Calculate the adjusted speed based on the distance
            float adjustedSpeed = baseSpeed + (distanceToPlayer * distanceMultiplier);

            // Ensure the adjusted speed doesn't go below the base speed
            adjustedSpeed = Mathf.Max(adjustedSpeed, baseSpeed);

            // Move the entire herd with the adjusted speed
            transform.Translate(Vector2.right * adjustedSpeed * Time.deltaTime);

            
        }
    }
}










