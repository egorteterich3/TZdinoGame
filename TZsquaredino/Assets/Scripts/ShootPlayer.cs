using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float fireRate = 1f;
    private float nextFireTime = 0f;

    [SerializeField] private WaypointMovement waypointMovement;

    private void Update()
    {
        if (!waypointMovement.IsWalking())
        {
            if (Time.time >= nextFireTime && Input.GetMouseButtonDown(0))
            {
                nextFireTime = Time.time + 1f / fireRate;

                Vector3 clickPosition = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(clickPosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 shootDirection = (hit.point - transform.position).normalized;

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                    bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
                }
            }
        }
    }
}

