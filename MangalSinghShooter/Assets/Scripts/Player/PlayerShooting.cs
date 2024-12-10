using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float fireRate = 0.5f;

    private float nextFireTime = 0f;

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

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = firePoint.right * bulletSpeed;

        Destroy(bullet, 2f);
    }
}
