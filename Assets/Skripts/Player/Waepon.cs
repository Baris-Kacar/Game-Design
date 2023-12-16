using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waepon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefeb;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    
    
    
    
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
           animator.SetTrigger("Shoot");
            Shoot();
        }
        
    }

    void Shoot()
    {
        // shooting logic
        Instantiate(bulletPrefeb, firePoint.position, firePoint.rotation);

    }
}
