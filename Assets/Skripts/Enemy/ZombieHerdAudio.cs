using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHerdAudio : MonoBehaviour
{
    private Transform player;
    private Transform hitbox; // The hitbox in front of the zombie herd
    private AudioSource audioSource;
    public float maxDistance = 10f; // Maximum distance for full volume
    public float minDistance = 2f;  // Minimum distance for minimum volume
    public float volumeFactor = 2.0f; // Adjust this factor to control the volume

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

        
        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource not found!");
        }
    }

    void Update()
    {
        if (hitbox != null && audioSource != null)
        {
            // Calculate the distance between the hitbox and the player
            float distanceToPlayer = Vector2.Distance(hitbox.position, player.position);

            // Set the volume based on the distance with a volume factor
            float volume = volumeFactor * (1f - Mathf.Clamp01((distanceToPlayer - minDistance) / (maxDistance - minDistance)));
            audioSource.volume = volume;

           
        }
    }
}


