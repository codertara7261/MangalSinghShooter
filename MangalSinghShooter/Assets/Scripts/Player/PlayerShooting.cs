using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static event Action OnPlayerShoot;

    [SerializeField] private GameObject bulletPrefab;
    
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime = 0f;
    private float bulletSpeed = 50f;

    public float FireRate {
        get { return fireRate; }
        set { fireRate = Mathf.Max(0.1f, value); }
    }


    private void Update() {
        if(Input.GetMouseButtonDown(0) && Time.time >= nextFireTime) {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot() { 
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        OnPlayerShoot?.Invoke();

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        Destroy(bullet, 2f);
    }
}
