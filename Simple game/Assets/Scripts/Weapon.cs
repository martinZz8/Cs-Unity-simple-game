using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    private float fire_rate = 5f;
    private float next_time_to_fire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If left button is pressed - fire
        if (Input.GetButton("Fire1") && Time.time >= next_time_to_fire)
        {
            if (Math.Abs(GetComponent<Rigidbody>().velocity.x) >= 0.5f)
            {
                next_time_to_fire = Time.time + 1f / fire_rate;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
