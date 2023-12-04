using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        // Assuming you have an Animator component attached to the same GameObject
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0)) // 0 represents the left mouse button
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Trigger the shooting animation
        animator.SetTrigger("Shoot");
    }
}